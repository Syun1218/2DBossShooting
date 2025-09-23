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

    //エネミー状態用変数
    private bool[] _isDieSubEnemie;
	#endregion

	#region プロパティ
    /// <summary>
    /// プレイヤーの現在座標
    /// </summary>
	public Vector2 PlayerPosition
    {
        get { return _playerPosisiton; }
        set { _playerPosisiton = value; }
    }

    /// <summary>
    /// エネミー本体の現在座標
    /// </summary>
	public Vector2 EnemyCorePosition
    {
		get { return _enemyCorePosition; }
        set { _enemyCorePosition = value; }
    }

    /// <summary>
    /// エネミー部位の現在座標
    /// </summary>
	public Vector2[] SubEnemiesPosition
    {
        get { return _subEnemiesPosition; }
        set { _subEnemiesPosition = value; }
    }

    /// <summary>
    /// ホーミング弾の管理クラス
    /// </summary>
    public BulletDirector HomingDirector
    {
        get { return _homingDirector; }
        set { _homingDirector = value; }
    }

    /// <summary>
    /// 拡散弾の管理クラス
    /// </summary>
    public BulletDirector DiffusionDirector
    {
        get { return _diffusionDirector; }
        set { _diffusionDirector = value; }
    }

    /// <summary>
    /// 直進弾の管理クラス
    /// </summary>
    public BulletDirector TargetDirector
    {
        get { return _targetDirector; }
        set { _targetDirector = value; }
    }

    /// <summary>
    /// ホーミング弾が現在存在するか
    /// </summary>
    public bool IsExistenceHomingBullet
    {
        get { return _isExistenceHomingBullet; }
        set { _isExistenceHomingBullet = value; }
    }
    
    /// <summary>
    /// エネミー部位の死亡状況
    /// </summary>
    public bool[] IsDieSubEnemies
    {
        get { return _isDieSubEnemie; }
        set { _isDieSubEnemie = value; }
    }
    #endregion
}