using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ノードのベースクラス
/// </summary>
public class NodeBace
{
	#region 変数
	protected List<NodeBace> _childNodes = new List<NodeBace>();
	protected NodeBace _childNode;
	protected GameObject _aiOwner;
    #endregion

    #region プロパティ
	public List<NodeBace> ChildNodes
    {
        get { return _childNodes; }
        set { _childNodes = value; }
    }

	public NodeBace ChildNode
    {
        set { _childNode = value; }
    }

	public GameObject AIOwner
    {
        set { _aiOwner = value; }
    }
    #endregion

    #region メソッド
    /// <summary>
    /// ノードの実行状況判断ステート
    /// </summary>
    public enum NodeState
    {
		Success, //実行に成功した場合
		Fail,    //実行に失敗した場合
		Running  //実行中の場合
    }

	/// <summary>
	/// ノードが呼ばれた際の処理を行う
	/// </summary>
	public virtual void OnStart()
    {
		//処理は継承先で記入
    }

	/// <summary>
	/// ノード処理中に呼ばれ続け、処理の結果を返す
	/// </summary>
	public virtual NodeState OnUpdate()
    {
		//処理は継承先で記入

		return NodeState.Running;
    }

	/// <summary>
	/// ノード終了時の処理を行う
	/// </summary>
	public virtual void OnEnd()
    {
		//処理は継承先で記入
	}
	#endregion
}