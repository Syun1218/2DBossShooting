using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ノードデータを格納する
/// </summary>
[CreateAssetMenu(menuName = "NodeTree")]
public class NodeTreeDesigner : ScriptableObject
{
	#region 変数  
	public List<NodeCreateData> _nodeDatas = new List<NodeCreateData>();
	#endregion
}