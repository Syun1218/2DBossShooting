using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーのデータクラス
/// </summary>
[CreateAssetMenu(menuName ="Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region 変数
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector2 _playerInstancePosition;
    [SerializeField] private float _playerColliderRadius;

    [SerializeField] private int _maxLife;
    [SerializeField] private int _maxBomb;

    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _lowSpeed;

    [SerializeField] private float _shotCoolTime;
	#endregion

	#region プロパティ
    /// <summary>
    /// プレイヤーのプレハブ
    /// </summary>
    public GameObject Player
    {
        get { return _player; }
    }

    public Vector2 PlayerInstancePosition
    {
        get { return _playerInstancePosition; }
    }

    public float PlayerColliderRadius
    {
        get { return _playerColliderRadius; }
    }

    /// <summary>
    /// プレイヤーの最大残機数
    /// </summary>
	public int MaxLife
    {
        get { return _maxLife; }
    }

    /// <summary>
    /// プレイヤーの最大ボム所持数
    /// </summary>
	public int MaxBomb
    {
        get { return _maxBomb; }
    }

    /// <summary>
    /// プレイヤーの通常移動速度
    /// </summary>
	public float NormalSpeed
    {
        get { return _normalSpeed; }
    }

    /// <summary>
    /// プレイヤーの低速移動速度
    /// </summary>
    public float LowSpeed
    {
        get { return _lowSpeed; }
    }

    /// <summary>
    /// プレイヤーのショットのクールタイム
    /// </summary>
    public float ShotCoolTime
    {
        get { return _shotCoolTime; }
    }
	#endregion
}