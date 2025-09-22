using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// 敵キャラのデータクラス
/// </summary>
[CreateAssetMenu(menuName ="Data/EnemyData")]
public class EnemyData : ScriptableObject
{
	#region 変数
	[SerializeField] [Header("エネミーオブジェクト")] private GameObject _enemy;
	[SerializeField] [Header("エネミーの部位オブジェクト")] private GameObject[] _subEnemies;

	[SerializeField] [Header("エネミーの生成座標")] private Vector2 _enemyInstancePosition;
	[SerializeField] [Header("エネミーの部位の生成座標")] private Vector2[] _subEnemiesInstancePosition;

	[SerializeField] [Header("エネミーのコライダーの半径")] private float _enemyColliderRadius;
	[SerializeField] [Header("エネミーの部位のコライダーの半径")] private float[] _subEnemiesColliderRadius;

	[SerializeField] [Header("エネミーの最大体力")] private int _maxHP;
	[SerializeField] [Header("エネミーの部位の最大体力")] private int[] _subEnemiesMaxHP;

    [SerializeField] [Header("エネミーが使用するホーミング弾のデータ")] private BulletData _homingBulletData;
    [SerializeField] [Header("エネミーが使用する拡散弾のデータ")] private BulletData _diffusionBulletData;
    [SerializeField] [Header("エネミーが使用するプレイヤー狙いの弾のデータ")] private BulletData _targetBulletData;

    [SerializeField] [Header("エネミーのAI")] private NodeTreeDesigner _treeDesigner;
    [SerializeField][Header("エネミーの部位のAI")] private NodeTreeDesigner[] _subEnemyTreeDesigners;

	[SerializeField] [Header("エネミーの上下移動速度")] private float _speed;
    [SerializeField] [Header("エネミーの攻撃間隔")] private float _idolTime;
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
    /// ホーミング弾のデータ
    /// </summary>
    public BulletData HomingBulletData
    {
        get { return _homingBulletData; }
    }

    /// <summary>
    /// 拡散弾のデータ
    /// </summary>
    public BulletData DiffusionBulletData
    {
        get { return _diffusionBulletData; }
    }

    /// <summary>
    /// プレイヤー狙いの弾のデータ
    /// </summary>
    public BulletData TargetBulletData
    {
        get { return _targetBulletData; }
    }

    /// <summary>
    /// エネミーのAI
    /// </summary>
    public NodeTreeDesigner TreeDesigner
    {
        get { return _treeDesigner; }
    }

    /// <summary>
    /// エネミーの部位のAI
    /// </summary>
    public NodeTreeDesigner[] SubEnemyTreeDesigners
    {
        get { return _subEnemyTreeDesigners; }
    }

    /// <summary>
    /// エネミーの移動速度
    /// </summary>
    public float Speed
    {
        get { return _speed; }
    }

    /// <summary>
    /// 攻撃間隔
    /// </summary>
    public float IdolTime
    {
        get { return _idolTime; }
    }
	#endregion
}