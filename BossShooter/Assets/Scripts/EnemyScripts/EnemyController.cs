using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーの管理クラス
/// </summary>
public class EnemyController:CollisionInterface
{
	#region 変数
	private GameObject _enemy;
	private GameObject[] _subEnemies;
	private GameObject _enemyParent;
	private EnemyData _enemyData;
	private SubEnemyController[] _subEnemyControllers;
	private SelfCircleCollider _enemyCollider;
	private SelfCircleCollider _subEnemyCollider;
	private Vector2 _parentPosition;
	private bool _isUp = true;
	private int _hp;
	private NodeBace.NodeState _state;

	//弾のプール管理変数
	private EnemyBulletPools _pools;

	//AI用変数
	private RootNode _root;
	private bool _isStart = true;

	//定数
	private const float MAX_Y_POSITION = 1.1f;
	private const float MIN_Y_POSITION = -1.1f;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public EnemyController(GameObject enemy,GameObject[] subEnemies,GameObject parent,EnemyData data)
    {
		//エネミーデータを受け取る
		_enemy = enemy;
		_subEnemies = subEnemies;
		_enemyParent = parent;
		_enemyData = data;
		_hp = _enemyData.MaxHP;

		//弾のプールを生成する
		_pools = new EnemyBulletPools(_enemyData);

		//弾の管理用クラスをデータに格納
		GameDirector.Instance.CurrentData.HomingDirector = new BulletDirector(_enemyData.HomingBulletData.InstanceCount, _enemyData.HomingBulletData.BulletSpeed,_pools.HomingPool ,_enemyData.HomingBulletData.MyType);
		GameDirector.Instance.CurrentData.DiffusionDirector = new BulletDirector(_enemyData.DiffusionBulletData.InstanceCount, _enemyData.DiffusionBulletData.BulletSpeed,_pools.DiffusionPool ,_enemyData.DiffusionBulletData.MyType);
		GameDirector.Instance.CurrentData.TargetDirector = new BulletDirector(_enemyData.TargetBulletData.InstanceCount, _enemyData.TargetBulletData.BulletSpeed,_pools.TargetPool ,_enemyData.TargetBulletData.MyType);

		//エネミーを衝突判定の対象に加える
		_enemyCollider = _enemy.GetComponent<SelfCircleCollider>();
		_enemyCollider.Radius = _enemyData.EnemyColliderRadius;
		_enemyCollider.MyObjectType = SelfCircleCollider.ObjectType.Enemy;
		_enemyCollider.MyCollisionInterface = this;
		CheckSelfCollider.Instance.SetColliderObject(_enemy);

		//ゲームデータを更新
		GameDirector.Instance.CurrentData.SubEnemiesPosition = new Vector2[_subEnemies.Length];

		//エネミーの各部位ごとに衝突判定の対象に加え、コントローラーを設定
		_subEnemyControllers = new SubEnemyController[_subEnemies.Length];
		for (int i = 0;i < _subEnemies.Length; i++)
        {
			_subEnemyCollider = _subEnemies[i].GetComponent<SelfCircleCollider>();
			_subEnemyCollider.Radius = _enemyData.SubEnemyColliderRadius[i];
			_subEnemyCollider.MyObjectType = SelfCircleCollider.ObjectType.Enemy;
			CheckSelfCollider.Instance.SetColliderObject(_subEnemies[i]);
			_subEnemyControllers[i] = new SubEnemyController(_subEnemies[i], _subEnemyCollider, _enemyData.SubEnemyTreeDesigners[i],_enemyData,_pools,i);
        }

		//AIを構築する
		_root = new RootNode(_enemyData.TreeDesigner, _enemy,_enemyData,_pools);
    }

	public void OnUpdate()
    {
		//ゲームデータを更新
		GameDirector.Instance.CurrentData.EnemyCorePosition = _enemy.transform.position;

		for(int i = 0;i < _subEnemies.Length; i++)
        {
			GameDirector.Instance.CurrentData.SubEnemiesPosition[i] = _subEnemies[i].transform.position;
        }

		//弾管理オブジェクトを作動させる
		GameDirector.Instance.CurrentData.HomingDirector.OnUpdate();
		GameDirector.Instance.CurrentData.DiffusionDirector.OnUpdate();
		GameDirector.Instance.CurrentData.TargetDirector.OnUpdate();
	}

	public void OnFixedUpdate()
    {
		//AIのスタートメソッドをまだ起動していない場合、起動する
		if (_isStart)
		{
			_root.OnStart();
			_isStart = false;
		}

		//弾管理オブジェクトを作動させる
		GameDirector.Instance.CurrentData.HomingDirector.OnFixedUpdate();
		GameDirector.Instance.CurrentData.DiffusionDirector.OnFixedUpdate();
		GameDirector.Instance.CurrentData.TargetDirector.OnFixedUpdate();

        //AIのアクションを実行させる
        _state = _root.OnUpdate();

		//アクションの成否を判定したら、終了処理を呼ぶ
		if(_state != NodeBace.NodeState.Running)
		{
			_root.OnEnd();
			_isStart = true;
		}

        //エネミーを上下に移動させる
        _parentPosition = _enemyParent.transform.position;

        if (_isUp)
        {
			_parentPosition += Vector2.up * _enemyData.Speed;
        }
        else
        {
			_parentPosition += Vector2.down * _enemyData.Speed;
        }

		_enemyParent.transform.position = _parentPosition;
		//画面端になったら移動方向を逆転させる
		if (_enemyParent.transform.position.y >= MAX_Y_POSITION)
		{
			_isUp = false;
		}
		else if (MIN_Y_POSITION >= _enemyParent.transform.position.y)
        {
			_isUp = true;
        }
    }

	public void OnCollision(SelfCircleCollider.ObjectType otherType)
	{
		//衝突相手がプレイヤーの弾オブジェクトの場合、ダメージを受ける
		if (otherType == SelfCircleCollider.ObjectType.PlayerBullet)
		{
			_hp--;

			//体力がゼロなった場合、進行スクリプトに通知する
			if(0 >= _hp)
            {
				GameDirector.Instance.OverGame();
            }
		}
	}
	#endregion
}