using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnDiffusionShot : ActionBace
{
    #region 変数
    private ObjectPool _myPool;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public override void Initialized()
    {
        if (_myPool is null)
        {
            _myPool = _pools.DiffusionPool;
        }
    }

    public override NodeBace.NodeState OnAction()
    {
        //弾オブジェクトをプレイヤーに向かう座標を0として、これを基準に回転を加えて生成する

        return NodeBace.NodeState.Success;
    }

    public override void End()
    {
        
    }
    #endregion
}