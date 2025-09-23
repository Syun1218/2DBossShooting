using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーに向かって直線状に弾を発射する
/// </summary>
public class OnTargetShot : ActionBace
{
    #region 変数
    private ObjectPool _myPool;
    private Vector2 _rotate;
    private float _rotateAngle;
    private GameObject _bullet;
    private int _nowCount;
    private float _nowTime;
    private bool _canShot = true;
    private int _nowShotCount;

    //定数
    private const float TARGET_TIME = 0.05f;
    #endregion

    #region メソッド
    public override void Initialized()
    {
        if (_myPool is null)
        {
            _myPool = _pools.TargetPool;
        }

        _rotateAngle = 0;
        _nowCount = 0;
        _canShot = true;

        //エネミーの状態によって連射回数を変化させる
        if (_gameDirector.IsEnemyHPMin())
        {
            _nowShotCount = _enemyData.MinRapidFireCount;
        }
        else if (_gameDirector.IsEnemyHPMid())
        {
            _nowShotCount = _enemyData.MidRapidFireCount;
        }
        else
        {
            _nowShotCount = _enemyData.NormalRapidFireCount;
        }
    }

    public override NodeBace.NodeState OnAction()
    {
        //連射の間隔をあける
        if (!_canShot)
        {
            _nowTime += Time.deltaTime;
            if(_nowTime >= TARGET_TIME)
            {
                _canShot = true;
                _nowTime = 0;
            }
            else
            {
                return NodeBace.NodeState.Running;
            }
        }

        _canShot = false;

        //弾オブジェクトをプレイヤーの方を向いた状態で一定数生成する
        //生成時の角度が決まっていない場合、発射角度を計算してから発射する
        if (_rotateAngle == 0)
        {
            GetShotPosition();

            //弾オブジェクトの回転数を計算し、プールからの取り出し時に適用する
            _rotate = (_gameDirector.CurrentData.PlayerPosition - _shotInstancePosition).normalized;
            _rotateAngle = Mathf.Atan2(_rotate.y, _rotate.x) * Mathf.Rad2Deg;
        }

        //弾を発射する
        _bullet = _pools.TargetPool.DequeueObject(_shotInstancePosition);
        _bullet.transform.rotation = Quaternion.Euler(0, 0, _rotateAngle);
        _gameDirector.CurrentData.TargetDirector.SetActiveBullet(_bullet);

        _nowCount++;

        //指定数の発射が終了した場合は成功を返し、終了していない場合は実行中を返す
        if (_nowCount >= _nowShotCount)
        {
            return NodeBace.NodeState.Success;
        }
        return NodeBace.NodeState.Running;
    }

    public override void End()
    {
        _rotateAngle = 0;
    }
    #endregion
}