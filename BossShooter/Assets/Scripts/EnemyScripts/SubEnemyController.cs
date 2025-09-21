using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーの部位の管理クラス
/// </summary>
public class SubEnemyController:CollisionInterface
{
	#region 変数
	private GameObject _subEnemy;
	private SelfCircleCollider _myCollider;
	private bool _isLive = true;
	private RootNode _root;
	#endregion

	#region プロパティ
	public bool IsLive
    {
        set { _isLive = value; }
    }
	#endregion

	#region メソッド
	public SubEnemyController(GameObject subEnemy,SelfCircleCollider collider,NodeTreeDesigner designer,EnemyData data)
    {
		_subEnemy = subEnemy;
		_myCollider = collider;

		//コライダーの設定を行う
		_myCollider.MyCollisionInterface = this;

		//AIを構築する
		//_root = new RootNode(designer, subEnemy,data);

		//この部位が生きている場合のみAIに処理を行わせる
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