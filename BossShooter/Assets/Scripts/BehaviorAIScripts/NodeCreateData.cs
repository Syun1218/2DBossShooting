using UnityEngine;

/// <summary>
/// ノードツリーを構成するときに使用するノードのデータ保持クラス
/// </summary>
[System.Serializable]
public class NodeCreateData
{
	#region 変数  
	[SerializeField] private string _nodeName;
	[SerializeField] private NodeType _myType;
	[SerializeField] private int _nodeID;
	[SerializeField] private int _parentID;
    [SerializeField] private ActionNode.ActionType _actionType;
    [SerializeField] private ConditionNode.ConditionType _conditionType;
    #endregion

    #region プロパティ
    public string NodeName
    {
        get { return _nodeName; }
    }

	public NodeType MyType
    {
        get { return _myType; }
    }

    public int NodeID
    {
        get { return _nodeID; }
    }

    public int ParentID
    {
        get { return _parentID; }
    }

    public ActionNode.ActionType ActionNode
    {
        get { return _actionType; }
    }

    public ConditionNode.ConditionType ConditionNode
    {
        get { return _conditionType; }
    }
    #endregion

    public enum NodeType
    {
		Action,
		Condition,
		Decorator,
		Selector,
		Sequence,
    }
}