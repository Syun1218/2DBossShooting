using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnHomingShot : ActionBace
{
    #region 変数
    private ObjectPool _myPool;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public override void Initialized()
    {
        if(_myPool is null)
        {
            _myPool = _pools.HomingPool;
        }
    }

    public override NodeBace.NodeState OnAction()
    {
        //すでにホーミング弾が存在する場合、発射しない
        if (GameDirector.Instance.CurrentData.IsExistenceHomingBullet)
        {
            return NodeBace.NodeState.Success;
        }

        //まだ存在しない場合、弾を発射する
        GetShotPosition();
        GameDirector.Instance.CurrentData.HomingDirector.SetActiveBullet(_pools.HomingPool.DequeueObject(_shotInstancePosition));

        //データを更新する
        GameDirector.Instance.CurrentData.IsExistenceHomingBullet = true;

        return NodeBace.NodeState.Success;
    }

    public override void End()
    {
        
    }
    #endregion
}