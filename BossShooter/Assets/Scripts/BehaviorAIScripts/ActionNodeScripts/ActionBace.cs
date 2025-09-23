using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// アクションノードの行動スクリプトのベース
/// </summary>
public class ActionBace
{
	#region 変数
	protected GameDirector _gameDirector;
	protected GameObject _owner;
	protected EnemyData _enemyData;
	protected EnemyBulletPools _pools;
	protected int _index;
	protected Vector2 _shotInstancePosition;

	//定数
	protected const int DEFAULT_INDEX = -1;
	#endregion

	#region プロパティ
	public GameDirector GameDirector
	{
		set { _gameDirector = value; }
	}

	public GameObject Owner
    {
        set { _owner = value; }
    }

	public EnemyData EnemyData
	{
		set { _enemyData = value; }
	}

	public EnemyBulletPools Pools
    {
        set { _pools = value; }
    }

	public int Index
    {
        set { _index = value; }
    }
	#endregion

	#region メソッド
	/// <summary>
	/// 初期化処理
	/// </summary>
	public virtual void Initialized()
    {
		//処理は継承先で記述
    }

	/// <summary>
	/// 行動処理
	/// </summary>
	public virtual NodeBace.NodeState OnAction()
    {
        //処理は継承先で記述
        return NodeBace.NodeState.Running;
    }

	/// <summary>
	/// 終了処理
	/// </summary>
	public virtual void End()
    {
        //処理は継承先で記述
    }

	/// <summary>
	/// 弾の発射座標を返す
	/// </summary>
	protected void GetShotPosition()
    {
		//インデックスが-1なら本体座標を、違うなら配列にインデックスを入れた際の値を発射座標とする
		if (_index == DEFAULT_INDEX)
		{
			_shotInstancePosition = _gameDirector.CurrentData.EnemyCorePosition;
		}
		else
		{
			_shotInstancePosition = _gameDirector.CurrentData.SubEnemiesPosition[_index];
		}
	}
    #endregion
}