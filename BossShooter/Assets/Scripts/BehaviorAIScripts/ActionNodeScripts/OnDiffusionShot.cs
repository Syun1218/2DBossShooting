using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnDiffusionShot : ActionBace
{
    #region 変数
    private ObjectPool _myPool;
    private Vector2 _rotate;
    private float _rotateAngle;
    private GameObject _bullet;
    private float[] _offsets;
    private int _bulletCount;
    #endregion

    #region メソッド
    public override void Initialized()
    {
        if (_myPool is null)
        {
            _myPool = _pools.DiffusionPool;
        }

        //エネミーの状態によって連射回数を変化させる
        if (GameDirector.Instance.IsEnemyHPMin())
        {
            _offsets = _enemyData.MinOffsets;
            _bulletCount = _enemyData.MinBulletCount;
        }
        else if (GameDirector.Instance.IsEnemyHPMid())
        {
            _offsets = _enemyData.MidOffsets;
            _bulletCount = _enemyData.MidBulletCount;
        }
        else
        {
            _offsets = _enemyData.NormalOffsets;
            _bulletCount = _enemyData.NormalBulletCount;
        }
        _rotateAngle = 0;
    }

    public override NodeBace.NodeState OnAction()
    {
        GetShotPosition();

        //弾オブジェクトをプレイヤーに向かう座標を0とする
        _rotate = (GameDirector.Instance.CurrentData.PlayerPosition - _shotInstancePosition).normalized;
        _rotateAngle = Mathf.Atan2(_rotate.y, _rotate.x) * Mathf.Rad2Deg;
        
        //角度をつけて一定数生成する
        for(int i = 0;i < _bulletCount; i++)
        {
            _bullet = _pools.TargetPool.DequeueObject(_shotInstancePosition);
            _bullet.transform.rotation = Quaternion.Euler(0, 0, _rotateAngle + _offsets[i]);
            GameDirector.Instance.CurrentData.TargetDirector.SetActiveBullet(_bullet);
        }

        return NodeBace.NodeState.Success;
    }

    public override void End()
    {
        
    }
    #endregion
}