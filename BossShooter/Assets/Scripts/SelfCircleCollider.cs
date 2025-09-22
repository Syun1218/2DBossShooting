using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 円形のコライダーとしての判定用データを持たせる
/// </summary>
public class SelfCircleCollider : MonoBehaviour,CollisionInterface
{
	#region 変数
	private Vector2 _centerPoint;
	private float _radius;
	private ObjectType _myObjectType;
    private BulletData.BulletType _bulletType;
    private CollisionInterface _myCollisionInterface;
    private int _bulletScore;

    //弾の衝突後処理用変数
    private bool _isCollision = false;
	#endregion

	#region プロパティ
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

        //これが敵の弾オブジェクトであれば、スコアを加算
        if(_myObjectType == ObjectType.EnemyBullet)
        {
            GameDirector.Instance.ScoreDirector.AddScore(_bulletScore);

            //ホーミング弾であればデータ更新を行う
            if (_bulletType == BulletData.BulletType.Homing)
            {
                GameDirector.Instance.CurrentData.IsExistenceHomingBullet = false;
            }
        }
    }
    #endregion


}