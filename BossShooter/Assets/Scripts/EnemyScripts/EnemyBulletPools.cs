using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーが使用する弾のプールを生成する
/// </summary>
public class EnemyBulletPools
{
	#region 変数
	private ObjectPool _homingPool;
	private ObjectPool _diffusionPool;
	private ObjectPool _targetPool;
	#endregion

	#region プロパティ
	/// <summary>
	/// ホーミング弾のプール
	/// </summary>
	public ObjectPool HomingPool
    {
        get { return _homingPool; }
    }

	/// <summary>
	/// 拡散弾のプール
	/// </summary>
	public ObjectPool DiffusionPool
    {
        get { return _diffusionPool; }
    }

	/// <summary>
	/// プレイヤー狙いの弾のプール
	/// </summary>
	public ObjectPool TargetPool
    {
        get { return _targetPool; }
    }
	#endregion

	#region メソッド
	public EnemyBulletPools(EnemyData data)
    {
		_homingPool = new ObjectPool(data.HomingBulletData.Bullet, data.HomingBulletData.InstanceCount, data.HomingBulletData.BulletColliderRadius, SelfCircleCollider.ObjectType.EnemyBullet,BulletData.BulletType.Homing,data.HomingBulletData.BulletScore);
		_diffusionPool = new ObjectPool(data.DiffusionBulletData.Bullet, data.DiffusionBulletData.InstanceCount, data.DiffusionBulletData.BulletColliderRadius, SelfCircleCollider.ObjectType.EnemyBullet,BulletData.BulletType.Diffusion,data.DiffusionBulletData.BulletScore);
		_targetPool = new ObjectPool(data.TargetBulletData.Bullet, data.TargetBulletData.InstanceCount, data.TargetBulletData.BulletColliderRadius, SelfCircleCollider.ObjectType.EnemyBullet,BulletData.BulletType.Target,data.TargetBulletData.BulletScore);
    }
	#endregion
}