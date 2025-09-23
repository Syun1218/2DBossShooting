using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 円形のコライダーとしての判定用データを持たせる
/// </summary>
public class SelfCircleCollider : MonoBehaviour,CollisionInterface
{
    #region 変数
    private GameDirector _gameDirector;
	private Vector2 _centerPoint;
	private float _radius;
	private ObjectType _myObjectType;
    private BulletData.BulletType _bulletType;
    private CollisionInterface _myCollisionInterface;
    private int _bulletScore;

    //弾の衝突後処理用変数
    private bool _isCollision = false;
    private ObjectType _otherObjectType;
	#endregion

	#region プロパティ
    public GameDirector GameDirector
    {
        set { _gameDirector = value; }
    }

	/// <summary>
	/// サークルコライダーの半径
	/// </summary>
	public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

	public ObjectType MyObjectType
    {
        get { return _myObjectType; }
        set { _myObjectType = value; }
    }

    public Vector2 CenterPoint
    {
        get { return _centerPoint; }
    }

    public CollisionInterface MyCollisionInterface
    {
        get { return _myCollisionInterface; }
        set { _myCollisionInterface = value; }
    }

    public bool IsCollsion
    {
        get { return _isCollision; }
        set { _isCollision = value; }
    }

    public int BulletScore
    {
        set { _bulletScore = value; }
    }

    public BulletData.BulletType BulletType
    {
        set { _bulletType = value; }
    }

    public ObjectType OtherObjectType
    {
        set { _otherObjectType = value; }
    }
	#endregion

	#region メソッド
	/// <summary>
	/// このオブジェクトの役割
	/// </summary>
	public enum ObjectType
    {
		Player,        //プレイヤーオブジェクト
		PlayerBullet,  //プレイヤーの弾オブジェクト
		Enemy,         //エネミーオブジェクト
		EnemyBullet,   //エネミーの弾オブジェクト
    }

    private void Awake()
    {
        _myCollisionInterface = this;

        _gameDirector = GameObject.FindGameObjectWithTag("Director").GetComponent<GameDirector>();
    }

    public void FixedUpdate()
    {
        _centerPoint = gameObject.transform.position;
    }

    /// <summary>
    /// 衝突した弾をプールに返却する処理
    /// </summary>
    public void OnCollision(SelfCircleCollider.ObjectType otherType)
    {
        _isCollision = true;

        //これが敵の弾オブジェクトであれば追加処理を行う
        if(_myObjectType == ObjectType.EnemyBullet)
        {
            //衝突相手がプレイヤーの弾であればスコアを加算
            if (_otherObjectType == ObjectType.PlayerBullet)
            {
                _gameDirector.ScoreDirector.AddScore(_bulletScore);
            }
            
            
            //ホーミング弾であればデータ更新を行う
            if (_bulletType == BulletData.BulletType.Homing)
            {
                _gameDirector.CurrentData.IsExistenceHomingBullet = false;
            }
        }
    }
    #endregion


}