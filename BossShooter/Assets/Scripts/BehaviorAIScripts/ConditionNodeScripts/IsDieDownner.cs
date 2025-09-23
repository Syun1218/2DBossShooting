using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミー下部位が破壊されている場合、成功を返す
/// </summary>
public class IsDieDownner : ConditionBace
{
    #region 変数
    //定数
    private const int DOWNER_INDEX = 1;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public override NodeBace.NodeState IsJudge()
    {
        if (GameDirector.Instance.CurrentData.IsDieSubEnemies[DOWNER_INDEX])
        {
            return NodeBace.NodeState.Success;
        }
        else
        {
            return NodeBace.NodeState.Fail;
        }
    }
    #endregion
}