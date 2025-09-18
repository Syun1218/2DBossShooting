using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 弾の移動や消滅管理を行うクラス
/// </summary>
public class BulletDirector
{
	#region 変数
	private GameObject[] _bulletArray;
	private int _maxCount;
	private float _bulletSpeed;
	private Vector2 _bulletNowPosition;
	private ObjectPool _bulletPool;
	private BulletData.BulletType _bulletType;

	//定数
	private const float MAX_BULLET_POSITION_X = 9f;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public BulletDirector(int bulletCount,float bulletSpeed,ObjectPool pool,BulletData.BulletType type)
    {
		//弾の生成数分の配列を確保する
		_bulletArray = new GameObject[bulletCount];
		_maxCount = bulletCount;
		_bulletSpeed = bulletSpeed;
		_bulletPool = pool;
		_bulletType = type;
    }

	/// <summary>
	/// 生成された弾を格納する処理
	/// </summary>
	/// <param name="bullet">生成された弾</param>
	public void SetActiveBullet(GameObject bullet)
    {
		//配列の空いている場所に弾を格納する
		for(int i = 0;i < _maxCount; i++)
        {
			if (_bulletArray[i] is null)
			{
				_bulletArray[i] = bullet;
				break;
			}
		}
    }

	/// <summary>
	/// 定期処理を行う
	/// </summary>
	public void OnFixedUpdate()
    {
        //弾のタイプによって移動処理を変える
        switch (_bulletType)
        {
			case BulletData.BulletType.Straight:
				StraightBulletMove();
			break;

			case BulletData.BulletType.Homing:
				HomingBulletMove();
			break;

			case BulletData.BulletType.Diffusion:
				DiffusionBulletMove();
			break;

			case BulletData.BulletType.Target:
				TargetBulletMove();
			break;
        }
    }

	/// <summary>
	/// 直線移動タイプの弾の移動処理
	/// </summary>
	private void StraightBulletMove()
    {
		for (int i = 0; i < _maxCount; i++)
		{
			if (_bulletArray[i] is not null)
			{
				//弾を直進移動させる
				_bulletNowPosition = _bulletArray[i].transform.position;
				_bulletNowPosition += Vector2.right * _bulletSpeed;
				_bulletArray[i].transform.position = _bulletNowPosition;

				//特定のX座標を超えた場合、画面外で出たとみなしてプールに返却する
				if (_bulletArray[i].transform.position.x >= MAX_BULLET_POSITION_X)
				{
					_bulletPool.EnqueueObject(_bulletArray[i]);
					_bulletArray[i] = null;
				}
			}
		}
	}

	/// <summary>
	/// ホーミングタイプの弾の移動処理
	/// </summary>
	private void HomingBulletMove()
    {

    }

	/// <summary>
	/// 拡散タイプの弾の移動処理
	/// </summary>
	private void DiffusionBulletMove()
    {

    }

	/// <summary>
	/// 目標直線タイプの弾の移動処理
	/// </summary>
	private void TargetBulletMove()
    {

    }
	#endregion
}