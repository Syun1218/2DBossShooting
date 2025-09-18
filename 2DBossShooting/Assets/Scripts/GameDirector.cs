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
    private AsyncOperationHandle<BulletData> _bulletData;
    private PlayerController _playerController;
    private PlayerData _playerData;
    private BulletData _playerBulletData;
    private GameObject _player;
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
        _bulletData = Addressables.LoadAssetAsync<BulletData>("PlayerBullet");
        _playerBulletData = _bulletData.WaitForCompletion();

        //各オブジェクトを生成する
        _player = Instantiate(_playerData.Player, _playerData.PlayerInstancePosition, Quaternion.identity);

        //各管理クラスのインスタンスを生成する
        _playerController = new PlayerController(_playerData, _player,_playerBulletData);
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