using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー操作を管理し、各アクションスクリプトへの中継を行う
/// </summary>
public class PlayerController:CollisionInterface
{
    #region 変数
    private PlayerData _playerData;
    private BulletData _playerBulletData;
    private GameObject _player;
    private SelfCircleCollider _playerCollider;
    private bool _inputShot = false;
    private bool _canShot = true;
	private Actions _actions;
    private Vector2 _moveValue;
    private bool _isRawSpeed;
    private PlayerMove _playerMove;
    private ObjectPool _objectPool;
    private float _nowCoolTime;
    private BulletDirector _bulletDirector;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public PlayerController(PlayerData data,GameObject player,BulletData bulletData)
    {
        //プレイヤーとプレイヤーデータを取得
        _playerData = data;
        _playerBulletData = bulletData; 
        _player = player;

        //プレイヤーコライダーの設定を行い、衝突判定の対象にする
        _playerCollider = _player.GetComponent<SelfCircleCollider>();
        _playerCollider.Radius = _playerData.PlayerColliderRadius;
        _playerCollider.MyObjectType = SelfCircleCollider.ObjectType.Player;
        _playerCollider.MyCollisionInterface = this;
        CheckSelfCollider.Instance.SetColliderObject(_player);

        //インプットアクションの初期化
        _actions = new Actions();

        _actions.Enable();

        //入力イベントの定義
        _actions.Player.Move.performed += OnMove;
        _actions.Player.Move.canceled += OnMove;
        _actions.Player.LowSpeed.performed += OnLawSpeed;
        _actions.Player.LowSpeed.canceled += OnLawSpeed;
        _actions.Player.Shot.performed += OnShot;
        _actions.Player.Shot.canceled += OnShot;

        //弾のプールを生成
        _objectPool = new ObjectPool(_playerBulletData.Bullet, _playerBulletData.InstanceCount,_playerBulletData.BulletColliderRadius,_playerBulletData.BulletObjectType);
        _bulletDirector = new BulletDirector(_playerBulletData.InstanceCount,_playerBulletData.BulletSpeed,_objectPool,_playerBulletData.MyType);

        //アクションクラスのインスタンスを生成
        _playerMove = new PlayerMove( _playerData.NormalSpeed, _playerData.LowSpeed, _player.transform);
    }

    private void OnDisable()
    {
        //インプットアクションの終了
        _actions.Disable();
    }

    public void OnUpdate()
    {
        //ショット操作がされているかつクールタイムが明けている場合、弾を発射する
        if (_canShot && _inputShot)
        {
            _bulletDirector.SetActiveBullet(_objectPool.DequeueObject(_player.transform.position));
            _canShot = false;
        }
        //ショットのクールタイムを計測する
        else if(!_canShot)
        {
            _nowCoolTime += Time.deltaTime;
            if(_nowCoolTime >= _playerData.ShotCoolTime)
            {
                _nowCoolTime = 0;
                _canShot = true;
            }
        }

        _bulletDirector.OnUpdate();
    }

    public void OnFixedUpdata()
    {
        _playerMove.MovePlayer(_isRawSpeed, _moveValue);
        _bulletDirector.OnFixedUpdate();
    }

    /// <summary>
    /// 移動入力の取得
    /// </summary>
    /// <param name="context">入力ベクトル</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 低速移動入力の取得
    /// </summary>
    private void OnLawSpeed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isRawSpeed = true;
        }
        else
        {
            _isRawSpeed = false;
        }
    }

    /// <summary>
    /// 弾発射入力の取得
    /// </summary>
    private void OnShot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputShot = true;
        }
        else
        {
            _inputShot = false;
        }
    }

    public void OnCollision(SelfCircleCollider.ObjectType otherType)
    {
        //残機を減らす
    }
    #endregion
}