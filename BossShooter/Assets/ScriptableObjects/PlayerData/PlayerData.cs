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
    [SerializeField] [Header("プレイヤーオブジェクト")] private GameObject _player;
    [SerializeField] [Header("プレイヤーの生成座標")] private Vector2 _playerInstancePosition;
    [SerializeField] [Header("プレイヤーのコライダーの半径")] private float _playerColliderRadius;

    [SerializeField] [Header("プレイヤーの最大残機")] private int _maxLife;
    [SerializeField] [Header("プレイヤーの最大ボム所持数")] private int _maxBomb;

    [SerializeField] [Header("プレイヤーの通常移動速度")] private float _normalSpeed;
    [SerializeField] [Header("プレイヤーの低速移動速度")] private float _lowSpeed;

    [SerializeField] [Header("プレイヤーの弾発射間隔")] private float _shotCoolTime;

    [SerializeField] [Header("プレイヤーの弾のアドレス")] private string _bulletAddress;
	#endregion

	#region プロパティ
    /// <summary>
    /// プレイヤーのプレハブ
    /// </summary>
    public GameObject Player
    {
        get { return _player; }
    }

    /// <summary>
    /// プレイヤーの生成される座標
    /// </summary>
    public Vector2 PlayerInstancePosition
    {
        get { return _playerInstancePosition; }
    }

    /// <summary>
    /// プレイヤーのコライダーの半径
    /// </summary>
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

    /// <summary>
    /// 使用する弾のアドレス
    /// </summary>
    public string BulletAddress
    {
        get { return _bulletAddress; }
    }
	#endregion
}