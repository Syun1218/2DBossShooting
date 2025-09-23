using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーの部位の管理クラス
/// </summary>
public class SubEnemyController:CollisionInterface
{
	#region 変数
	private GameDirector _gameDirector;
    private GameObject _subEnemy;
	private SelfCircleCollider _myCollider;
	private float _myHP;
	private bool _isLive = true;
	private RootNode _root;
	private int _myIndex;
	private bool _isStart = true;	
	private NodeBace.NodeState _state;
	#endregion

	#region プロパティ
	public bool IsLive
    {
        set { _isLive = value; }
    }
	#endregion

	#region メソッド
	public SubEnemyController(GameDirector director,GameObject subEnemy,SelfCircleCollider collider,NodeTreeDesigner designer,EnemyData data,EnemyBulletPools pools,int index)
    {
		_gameDirector = director;
		_subEnemy = subEnemy;
		_myCollider = collider;
		_myIndex = index;
		_myHP = data.SubEnemiesMaxHP[_myIndex];
		_isLive = true;

		//コライダーの設定を行う
		_myCollider.MyCollisionInterface = this;

		//AIを構築する
		_root = new RootNode(_gameDirector,designer, subEnemy, data, pools,_myIndex);
		_isStart = true;
	}

	public void OnFixedUpdate()
	{
		//破壊されている場合、AIを起動しない
		if (!_isLive)
		{
			return;
		}

        //AIのスタートメソッドをまだ起動していない場合、起動する
        if (_isStart)
        {
            _root.OnStart();
            _isStart = false;
        }

        //AIのアクションを実行させる
        _state = _root.OnUpdate();

        //アクションの成否を判定したら、終了処理を呼ぶ
        if (_state != NodeBace.NodeState.Running)
        {
            _root.OnEnd();
            _isStart = true;
        }
    }

	public void OnCollision(SelfCircleCollider.ObjectType otherType)
	{
		//衝突相手がプレイヤーの弾オブジェクトの場合、ダメージを受ける
		if(otherType == SelfCircleCollider.ObjectType.PlayerBullet)
        {
			_myHP--;

			//HPが0になった場合、破壊状態にする
			if(0 >= _myHP)
			{
				_isLive = false;
				_gameDirector.CurrentData.IsDieSubEnemies[_myIndex] = true;
				_subEnemy.GetComponent<SpriteRenderer>().material.color = Color.black;
			}
        }
	}
	#endregion
}