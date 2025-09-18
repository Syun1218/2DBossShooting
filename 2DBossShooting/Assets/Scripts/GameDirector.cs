using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Collections;
using System.Collections.Generic;

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
    private AsyncOperationHandle<PoolData> _poolData;
    private PlayerController _playerController;
    private PlayerData _playerData;
    private PoolData _playerPoolData;
    private GameObject _player;

    //定数
    private readonly Vector2 _playerInstancePosition = new Vector2(-5, 0);
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
        _poolData = Addressables.LoadAssetAsync<PoolData>("PlayerBullet");
        _playerPoolData = _poolData.WaitForCompletion();

        //各オブジェクトを生成する
        _player = Instantiate(_playerData.Player, _playerInstancePosition, Quaternion.identity);

        //各管理クラスのインスタンスを生成する
        _playerController = new PlayerController(_playerData, _player,_playerPoolData);
    }

    private void Update()
    {
        _playerController.OnUpdate();
    }

    private void FixedUpdate()
    {
        _playerController.OnFixedUpdata();
    }
    #endregion
}