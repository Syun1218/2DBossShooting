using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
	#region 変数
	[SerializeField] [Header("弾オブジェクト")] private GameObject _bullet;
    [SerializeField] [Header("弾の挙動のタイプ")] private BulletType _myType;
	[SerializeField] [Header("弾の生成個数")] private int _instanceCount;
	[SerializeField] [Header("弾の速度")] private float _bulletSpeed;
	[SerializeField] [Header("弾のコライダーの半径")] private float _bulletColliderRadius;
	[SerializeField] [Header("弾の判定の種類")] private SelfCircleCollider.ObjectType _bulletObjectType;
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