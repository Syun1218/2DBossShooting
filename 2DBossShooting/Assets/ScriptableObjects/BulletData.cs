using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
	#region 変数
	[SerializeField] private GameObject _bullet;
    [SerializeField] private BulletType _myType;
	[SerializeField] private int _instanceCount;
	[SerializeField] private float _bulletSpeed;
	[SerializeField] private float _bulletColliderRadius;
	private SelfCircleCollider.ObjectType _bulletObjectType = SelfCircleCollider.ObjectType.PlayerBullet;
	#endregion

	#region プロパティ
    /// <summary>
    /// 弾オブジェクト
    /// </summary>
	public GameObject Bullet
    {
        get { return _bullet; }
    }

    /// <summary>
    /// 弾の種類
    /// </summary>
    public BulletType MyType
    {
        get { return _myType; }
    }

    /// <summary>
    /// 弾の生成数
    /// </summary>
	public int InstanceCount
    {
        get { return _instanceCount; }
    }

    /// <summary>
    /// 弾の移動速度
    /// </summary>
    public float BulletSpeed
    {
        get { return _bulletSpeed; }
    }

    /// <summary>
    /// 弾のコライダー半径
    /// </summary>
    public float BulletColliderRadius
    {
        get { return _bulletColliderRadius; }
    }

    /// <summary>
    /// 弾のオブジェクト役割
    /// </summary>
    public SelfCircleCollider.ObjectType BulletObjectType
    {
        get { return _bulletObjectType; }
    }
    #endregion

    #region メソッド
    /// <summary>
    /// 弾の種類
    /// </summary>
    public enum BulletType
    {
        Straight,  //発射位置から横にまっすぐ飛ぶ
        Homing,    //敵に向かって一定時間ホーミングする
        Diffusion, //発射位置から拡散する
        Target,    //発射位置から敵に向かって直線で飛ぶ
    }
    #endregion
}