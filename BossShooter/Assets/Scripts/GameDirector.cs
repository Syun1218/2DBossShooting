using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームシーンの進行を管理するクラス
/// </summary>
public class GameDirector : MonoBehaviour
{
    #region 変数
    //進行変数
    private static GameDirector _instance;

    //プレイヤー変数
    private AsyncOperationHandle<PlayerData> _loadPlayerData;
    private AsyncOperationHandle<BulletData> _loadBulletData;
    private PlayerController _playerController;
    private PlayerData _playerData;
    private BulletData _playerBulletData;
    private GameObject _player;

    //エネミー変数
    private AsyncOperationHandle<EnemyData> _loadEnemyData;
    private EnemyData _enemyData;
    private GameObject _enemyParent;
    private GameObject _enemy;
    private GameObject[] _subEnemies;
    private EnemyController _enemyController;
    #endregion

    #region プロパティ
    public static GameDirector Instance
    {
        get { return _instance; }
    }
    #endregion

    #region メソッド
    private void Awake()
    {
        //シングルトンを設定
        if(_instance is null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //スクリプタブルオブジェクトをロードする
        _loadPlayerData = Addressables.LoadAssetAsync<PlayerData>("PlayerData");
        _playerData = _loadPlayerData.WaitForCompletion();
        _loadBulletData = Addressables.LoadAssetAsync<BulletData>(_playerData.BulletAddress);
        _playerBulletData = _loadBulletData.WaitForCompletion();
        _loadEnemyData = Addressables.LoadAssetAsync<EnemyData>("BossData");
        _enemyData = _loadEnemyData.WaitForCompletion();

        //各オブジェクトを生成する
        _player = Instantiate(_playerData.Player, _playerData.PlayerInstancePosition, Quaternion.identity);
        _enemyParent = new GameObject("EnemyParent");
        _enemy = Instantiate(_enemyData.Enemy, _enemyData.EnemyInstancePosition, Quaternion.identity, _enemyParent.transform);
        if(_enemyData.SubEnemies.Length != 0)
        {
            //エネミーの部位がある場合、すべて生成
            _subEnemies = new GameObject[_enemyData.SubEnemies.Length];
            for(int i = 0;i < _enemyData.SubEnemies.Length; i++)
            {
                _subEnemies[i] = Instantiate(_enemyData.SubEnemies[i], _enemyData.SubEnemyInstancePosition[i], Quaternion.identity, _enemyParent.transform);
            }
        }

        //各管理クラスのインスタンスを生成する
        _playerController = new PlayerController(_playerData, _player,_playerBulletData);
        _enemyController = new EnemyController(_enemy,_subEnemies,_enemyParent,_enemyData);
    }

    private void Update()
    {
        _playerController.OnUpdate();
    }

    private void FixedUpdate()
    {
        _playerController.OnFixedUpdata();
        _enemyController.OnFixedUpdate();
    }

    /// <summary>
    /// ボスが倒された場合に呼ばれ、終了処理を行う
    /// </summary>
    public void KilledBoss()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}