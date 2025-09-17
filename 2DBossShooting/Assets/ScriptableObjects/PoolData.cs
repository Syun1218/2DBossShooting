using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="Data/PoolData")]
public class PoolData : ScriptableObject
{
	#region 変数
	[SerializeField] private GameObject _instanceObject;
	[SerializeField] private int _instanceCount;
	#endregion

	#region プロパティ
	public GameObject InstanceObject
    {
        get { return _instanceObject; }
    }

	public int InstanceCount
    {
        get { return _instanceCount; }
    }
	#endregion
}