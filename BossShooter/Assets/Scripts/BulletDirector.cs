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
	private SelfCircleCollider[] _bulletColliderArray;
	private int _maxCount;
	private float _bulletSpeed;
	private Vector2 _bulletNowPosition;
	private ObjectPool _bulletPool;
	private BulletData.BulletType _bulletType;
	private int _nowBullet = 0;
	private int _bulletsScore;

	//ホーミング弾移動処理変数
	private float _targetAngle;
	private float _clampAngle;
	private float _rotateStep;
	private float _nowHomingTime = 0;
	private bool _isHoming = true;
	private float _totalAngle = 0;
	private float _frameRotate;
	private float _rotateDiff;
	private float _desideTortal;

	//定数
	private const float MAX_BULLET_POSITION_X = 9f;
	private const float ENEMY_MAX_BULLET_POSITION_X = -10f;
	private const float MAX_ANGLE = 30;
	private const float ROTATE_SPEED = 25;
	private const float TARGET_HOMING_TIME = 30;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public BulletDirector(int bulletCount,float bulletSpeed,ObjectPool pool,BulletData.BulletType type,int score = 0)
    {
		//弾の生成数分の配列を確保する
		_bulletArray = new GameObject[bulletCount];
		_bulletColliderArray = new SelfCircleCollider[bulletCount];
		_maxCount = bulletCount;
		_bulletSpeed = bulletSpeed;
		_bulletPool = pool;
		_bulletType = type;
		_bulletsScore = score;
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
				_bulletColliderArray[i] = bullet.GetComponent<SelfCircleCollider>();
				_nowBullet++;
				break;
			}
		}
    }

	/// <summary>
	/// 弾の衝突状況を管理する
	/// </summary>
	public void OnUpdate()
    {
		//弾が存在しない場合処理を行わない
		if(_nowBullet == 0)
        {
			return;
        }

		for (int i = 0; i < _maxCount; i++)
		{
			if(_bulletArray[i] is null)
            {
				continue;
            }

			//衝突した弾の返却処理
			if (_bulletColliderArray[i].IsCollsion)
            {
				_bulletColliderArray[i].IsCollsion = false;
				_bulletPool.EnqueueObject(_bulletArray[i]);
				_bulletArray[i] = null;
				_bulletColliderArray[i] = null;
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
					_bulletColliderArray[i] = null;
					_nowBullet--;
				}
			}
		}
	}

	/// <summary>
	/// ホーミングタイプの弾の移動処理
	/// </summary>
	private void HomingBulletMove()
    {
		for (int i = 0; i < _maxCount; i++)
		{
			if (_bulletArray[i] is not null)
			{
				//弾オブジェクトの左方向に向かって移動させる
				_bulletNowPosition = _bulletArray[i].transform.position;
				_bulletNowPosition -= (Vector2)_bulletArray[i].transform.right * _bulletSpeed;
				_bulletArray[i].transform.position = _bulletNowPosition;

                if (_isHoming)
                {
					//プレイヤーとの位置関係によって±30度までの緩やかな回転をさせる
					_targetAngle = Vector2.SignedAngle(-_bulletArray[i].transform.right, (GameDirector.Instance.CurrentData.PlayerPosition - (Vector2)_bulletArray[i].transform.position).normalized);
					_rotateStep = ROTATE_SPEED * Time.deltaTime;
					_frameRotate = Mathf.Clamp(_targetAngle, -_rotateStep, _rotateStep);
					_totalAngle = _desideTortal + _frameRotate;

					//制限超過分を補正する
					if(_totalAngle > MAX_ANGLE)
					{
						_frameRotate = MAX_ANGLE - _desideTortal;
					}
					else if(-MAX_ANGLE > _totalAngle)
					{
                        _frameRotate = -MAX_ANGLE - _desideTortal;
                    }

                    //回転を適用する
                    _bulletArray[i].transform.Rotate(0, 0, _frameRotate);
					_desideTortal += _frameRotate;

                    //一定時間後、誘導をやめる
                    _nowHomingTime += Time.deltaTime;
					if(_nowHomingTime >= TARGET_HOMING_TIME)
                    {
						_nowHomingTime = 0;
						_isHoming = false;
                    }
                }

				//一定のX座標を超えたら、返却処理を行う
				if (ENEMY_MAX_BULLET_POSITION_X >= _bulletArray[i].transform.position.x)
				{
					_bulletArray[i].transform.Rotate(Vector3.zero);
					_bulletPool.EnqueueObject(_bulletArray[i]);
					_bulletArray[i] = null;
					_bulletColliderArray[i] = null;
					_nowBullet--;
					_nowHomingTime = 0;
					_isHoming = true;
					_targetAngle = 0;
					_desideTortal = 0;
					GameDirector.Instance.CurrentData.IsExistenceHomingBullet = false;
				}
			}
		}
	}

	/// <summary>
	/// 拡散タイプの弾の移動処理
	/// </summary>
	private void DiffusionBulletMove()
    {
		for (int i = 0; i < _maxCount; i++)
		{
			if (_bulletArray[i] is not null)
			{
				//弾オブジェクトの左方向に向かって直進させる
				_bulletNowPosition = _bulletArray[i].transform.position;
				_bulletNowPosition += (Vector2)_bulletArray[i].transform.right * _bulletSpeed;
				_bulletArray[i].transform.position = _bulletNowPosition;

				//一定のX座標を超えたら、返却処理を行う
				if (ENEMY_MAX_BULLET_POSITION_X >= _bulletArray[i].transform.position.x)
				{
					_bulletPool.EnqueueObject(_bulletArray[i]);
					_bulletArray[i] = null;
					_bulletColliderArray[i] = null;
					_nowBullet--;
				}
			}
		}
	}

	/// <summary>
	/// 目標直線タイプの弾の移動処理
	/// </summary>
	private void TargetBulletMove()
    {
		for (int i = 0; i < _maxCount; i++)
		{
			if (_bulletArray[i] is not null)
			{
				//弾オブジェクトの左方向に向かって直進させる
				_bulletNowPosition = _bulletArray[i].transform.position;
				_bulletNowPosition += (Vector2)_bulletArray[i].transform.right * _bulletSpeed;
				_bulletArray[i].transform.position = _bulletNowPosition;

                //一定のX座標を超えたら、返却処理を行う
                if (ENEMY_MAX_BULLET_POSITION_X >= _bulletArray[i].transform.position.x)
                {
                    _bulletPool.EnqueueObject(_bulletArray[i]);
                    _bulletArray[i] = null;
                    _bulletColliderArray[i] = null;
                    _nowBullet--;
                }
            }
		}
	}

	/// <summary>
	/// アクティブな弾をすべて消す
	/// </summary>
	public void ClearBullets()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            if (_bulletArray[i] is not null)
            {
				GameDirector.Instance.ScoreDirector.AddScore(_bulletsScore);
                _bulletPool.EnqueueObject(_bulletArray[i]);
                _bulletArray[i] = null;
                _bulletColliderArray[i] = null;
                _nowBullet--;
            }
        }
    }
	#endregion
}