using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 現在のゲームでAIに必要な情報を格納する
/// </summary>
public class GameCurrentData
{
	#region 変数
    //座標データ変数
	private Vector2 _playerPosisiton;
	private Vector2 _enemyCorePosition;
    private Vector2[] _subEnemiesPosition;
    
    //弾管理クラス変数
    private BulletDirector _homingDirector;
    private BulletDirector _diffusionDirector;
    private BulletDirector _targetDirector;
    private bool _isExistenceHomingBullet = false;
	#endregion

	#region プロパティ
	public Vector2 PlayerPosition
    {
        get { return _playerPosisiton; }
        set { _playerPosisiton = value; }
    }

	public Vector2 EnemyCorePosition
    {
		get { return _enemyCorePosition; }
        set { _enemyCorePosition = value; }
    }

	public Vector2[] SubEnemiesPosition
    {
        get { return _subEnemiesPosition; }
        set { _subEnemiesPosition = value; }
    }

    public BulletDirector HomingDirector
    {
        get { return _homingDirector; }
        set { _homingDirector = value; }
    }

    public BulletDirector DiffusionDirector
    {
        get { return _diffusionDirector; }
        set { _diffusionDirector = value; }
    }

    public BulletDirector TargetDirector
    {
        get { return _targetDirector; }
        set { _targetDirector = value; }
    }

    public bool IsExistenceHomingBullet
    {
        get { return _isExistenceHomingBullet; }
        set { _isExistenceHomingBullet = value; }
    }
	#endregion

	#region メソッド

	#endregion
}