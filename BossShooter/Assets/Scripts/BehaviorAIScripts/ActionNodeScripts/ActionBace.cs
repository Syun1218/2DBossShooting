using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// アクションノードの行動スクリプトのベース
/// </summary>
public class ActionBace
{
	#region 変数
	protected GameObject _owner;
	protected EnemyData _enemyData;
	#endregion

	#region プロパティ
	public GameObject Owner
    {
        set { _owner = value; }
    }

	public EnemyData EnemyData
	{
		set { _enemyData = value; }
	}
	#endregion

	#region メソッド
	/// <summary>
	/// 初期化処理
	/// </summary>
	public virtual void Initialized()
    {
		//処理は継承先で記述
    }

	/// <summary>
	/// 行動処理
	/// </summary>
	public virtual NodeBace.NodeState OnAction()
    {
        //処理は継承先で記述
        return NodeBace.NodeState.Running;
    }

	/// <summary>
	/// 終了処理
	/// </summary>
	public virtual void End()
    {
        //処理は継承先で記述
    }
    #endregion
}