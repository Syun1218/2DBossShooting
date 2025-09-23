using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 判断ノードのベース
/// </summary>
public class ConditionBace
{
    #region 変数
    protected GameDirector _gameDirector;
    #endregion

    #region プロパティ
    public GameDirector GameDirector
    {
        set { _gameDirector = value; }
    }
    #endregion

    #region メソッド  
    public virtual NodeBace.NodeState IsJudge()
    {
		return NodeBace.NodeState.Running;
    }
	#endregion
}