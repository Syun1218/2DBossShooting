using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubEnemyController:CollisionInterface
{
	#region 変数
	private GameObject _subEnemy;
	private SelfCircleCollider _myCollider;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public SubEnemyController(GameObject subEnemy,SelfCircleCollider collider)
    {
		_subEnemy = subEnemy;
		_myCollider = collider;

		//コライダーの設定を行う
		_myCollider.MyCollisionInterface = this;
    }

	public void OnCollision(SelfCircleCollider.ObjectType otherType)
	{
		//衝突相手がプレイヤーの弾オブジェクトの場合、ダメージを受ける
		if(otherType == SelfCircleCollider.ObjectType.PlayerBullet)
        {
			
        }
	}
	#endregion
}