using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface CollisionInterface
{
	#region メソッド
	/// <summary>
	/// エネミーとプレイヤーが使用する衝突時処理
	/// </summary>
	public void OnCollision(SelfCircleCollider.ObjectType otherType);
	#endregion
}