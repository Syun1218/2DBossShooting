using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region 変数
    [SerializeField] private GameObject _player;

    [SerializeField] private int _maxLife;
    [SerializeField] private int _maxBomb;

    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _lowSpeed;

    [SerializeField] private float _shotCoolTime;
	#endregion

	#region プロパティ
    public GameObject Player
    {
        get { return _player; }
    }

	public int MaxLife
    {
        get { return _maxLife; }
    }

	public int MaxBomb
    {
        get { return _maxBomb; }
    }

	public float NormalSpeed
    {
        get { return _normalSpeed; }
    }

    public float LowSpeed
    {
        get { return _lowSpeed; }
    }

    public float ShotCoolTime
    {
        get { return _shotCoolTime; }
    }
	#endregion
}