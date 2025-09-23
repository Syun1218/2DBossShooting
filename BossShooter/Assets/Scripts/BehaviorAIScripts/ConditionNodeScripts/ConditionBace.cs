using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 判断ノードのベース
/// </summary>
public class ConditionBace
{
	#region メソッド  
	public virtual NodeBace.NodeState IsJudge()
    {
		return NodeBace.NodeState.Running;
    }
	#endregion
}