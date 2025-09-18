using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 敵キャラのデータクラス
/// </summary>
[CreateAssetMenu(menuName ="Data/EnemyData")]
public class EnemyData : ScriptableObject
{
	#region 変数
	[SerializeField] private GameObject _enemy;
	[SerializeField] private GameObject[] _subEnemies;
	[SerializeField] private Vector2 _enemyInstancePosition;
	[SerializeField] private Vector2[] _subEnemiesInstancePosition;
	[SerializeField] private float _enemyColliderRadius;
	[SerializeField] private float[] _subEnemiesColliderRadius;
	[SerializeField] private int _maxHP;
	[SerializeField] private int[] _subEnemiesMaxHP;

	[SerializeField] private float _speed;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
    /// <summary>
    /// エネミーオブジェクト
    /// </summary>
	public GameObject Enemy
    {
        get { return _enemy; }
    }

    /// <summary>
    /// エネミーの部位オブジェクト
    /// </summary>
	public GameObject[] SubEnemies
    {
        get { return _subEnemies; }
    }

    /// <summary>
    /// エネミーの生成位置
    /// </summary>
	public Vector2 EnemyInstancePosition
    {
        get { return _enemyInstancePosition; }
    }

    /// <summary>
    /// エネミーの部位の生成位置
    /// </summary>
	public Vector2[] SubEnemyInstancePosition
    {
        get { return _subEnemiesInstancePosition; }
    }

    /// <summary>
    /// エネミーのコライダーの半径
    /// </summary>
	public float EnemyColliderRadius
    {
        get { return _enemyColliderRadius; }
    }

    /// <summary>
    /// エネミーの部位のコライダーの半径
    /// </summary>
	public float[] SubEnemyColliderRadius
    {
        get { return _subEnemiesColliderRadius; }
    }

    /// <summary>
    /// エネミーの最大体力
    /// </summary>
	public int MaxHP
    {
        get { return _maxHP; }
    }

    /// <summary>
    /// エネミーの部位の最大体力
    /// </summary>
	public int[] SubEnemiesMaxHP
    {
        get { return _subEnemiesMaxHP; }
    }

    /// <summary>
    /// エネミーの移動速度
    /// </summary>
    public float Speed
    {
        get { return _speed; }
    }
	#endregion
}