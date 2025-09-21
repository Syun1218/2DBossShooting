using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ビヘイビアツリーの頂点ノード、ScriptablObjectからノードデータを受け取りツリーを構成する
/// </summary>
public class RootNode : NodeBace
{
	#region 変数  
	private NodeTreeDesigner _designer;
	private List<NodeBace> _allNodes = new List<NodeBace>();

	private NodeBace _runningNode;
	private List<NodeBace> _wayConditionNode;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド  
	public RootNode(NodeTreeDesigner designer,GameObject aiOwner,EnemyData data,int index = -1)
    {
		//ツリー構造とAIを保持するオブジェクトを取得
		_designer = designer;
		_aiOwner = aiOwner;

		int parentID = 0;

		//渡されたデータリストをもとにノードツリーを構築する
		for (int i = 0; i < _designer._nodeDatas.Count; i++)
		{
			//ノードを実体化させる
			_allNodes.Add(InstanceNode(i));
			_allNodes[i].AIOwner = _aiOwner;
			_allNodes[i].EnemyData = data;
			_allNodes[i].Index = index;
			parentID = _designer._nodeDatas[i].ParentID;

			//親ノードがルートノードの場合、親設定の処理をスキップする
			if (i == 0)
			{
				continue;
			}

			//親ノードがルートノードではない場合、親に渡す
			if (_designer._nodeDatas[parentID].MyType == NodeCreateData.NodeType.Decorator)
			{
				_allNodes[parentID].ChildNode = _allNodes[i];
			}
			else
			{
				_allNodes[parentID].ChildNodes.Add(_allNodes[i]);
			}
		}

		//index0の位置にあるノードをルートノードの子ノードにする
		_childNode = _allNodes[0];
	}

    public override void OnStart()
    {
		_childNode.OnStart();
    }

    public override NodeState OnUpdate()
    {
		return _childNode.OnUpdate();
	}

    public override void OnEnd()
    {
		_childNode.OnEnd();
    }

    /// <summary>
    /// 渡されたインデックスからリストのノードをインスタンスして返す処理
    /// </summary>
    private NodeBace InstanceNode(int index)
    {
		NodeBace returnNode = null;

        switch (_designer._nodeDatas[index].MyType)
        {
            case NodeCreateData.NodeType.Selector:
                returnNode = new SelectorNode();
            break;

            case NodeCreateData.NodeType.Sequence:
                returnNode = new SequenceNode();
            break;

            case NodeCreateData.NodeType.Action:
				returnNode = new ActionNode(_designer._nodeDatas[index].ActionNode);
				break;

            case NodeCreateData.NodeType.Condition:
                //returnNode = new ConditionNode(_designer._nodeDatas[index].ConditionNode);
            break;
        }

        return returnNode;
    }
    #endregion
}