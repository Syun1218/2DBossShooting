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
    private bool _isInvicible = false;
    private int _myLife = 0;
    private int _myBomb = 0;
    private bool _isDeath = false;
    private float _nowRespawnTime = 0;
    private float _nowInvicibleTime = 0;

    //定数
    private readonly Vector2 _deathPoint = new Vector2(80, 0);
    private const float RESPAWN_TARGET_TIME = 0.75f;
    private const float INVICIBLE_TARGET_TIME = 3f;
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

        //残機とボムをセットする
        _myLife = _playerData.MaxLife;
        _myBomb = _playerData.MaxBomb;

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
        _objectPool = new ObjectPool(_playerBulletData.Bullet, _playerBulletData.InstanceCount,_playerBulletData.BulletColliderRadius,_playerBulletData.BulletObjectType,BulletData.BulletType.Straight);
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
        //死亡している場合、リスポーンまで待機し、リスポーン後に無敵状態となる
        if (_isDeath)
        {
            _nowRespawnTime += Time.deltaTime;
            if(_nowRespawnTime >= RESPAWN_TARGET_TIME)
            {
                //無敵状態でリスポーンさせる
                _isDeath = false;
                _isInvicible = true;
                _nowRespawnTime = 0;
                _player.transform.position = _playerData.PlayerInstancePosition;
            }
        }

        //無敵状態が切れるまでをカウントする
        if (_isInvicible)
        {
            _nowInvicibleTime += Time.deltaTime;
            if(_nowInvicibleTime >= INVICIBLE_TARGET_TIME)
            {
                _isInvicible = false;
                _nowInvicibleTime = 0;
            }
        }

        //死亡していない場合現在のプレイヤーの座標をデータに渡し、死亡中の場合、リスポーンポイントを渡す
        if (_isDeath)
        {
            GameDirector.Instance.CurrentData.PlayerPosition = _playerData.PlayerInstancePosition;
            return;
        }
        else
        {
            GameDirector.Instance.CurrentData.PlayerPosition = _player.transform.position;
        }

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
        //死亡時には処理をしない
        if (_isDeath)
        {
            return;
        }

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
        //無敵状態でないかつ生きているなら残機を減らす
        if (!_isInvicible && !_isDeath)
        {
            _myLife--;
            _player.transform.position = _deathPoint;
            _isDeath = true;

            //残機がゼロ未満になったら、ゲームオーバーになる
            if(0 > _myLife)
            {
                GameDirector.Instance.OverGame();
            }
        }
    }
    #endregion
}