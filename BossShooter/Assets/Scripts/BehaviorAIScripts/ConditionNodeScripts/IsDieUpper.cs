using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミー上部位が破壊されている場合、成功を返す
/// </summary>
public class IsDieUpper : ConditionBace
{
    #region 変数
    //定数
    private const int UPPER_INDEX = 0;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public override NodeBace.NodeState IsJudge()
    {
        if (_gameDirector.CurrentData.IsDieSubEnemies[UPPER_INDEX])
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