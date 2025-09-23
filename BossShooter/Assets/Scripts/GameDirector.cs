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
    private GameDirector _instance;
    private GameCurrentData _currentData;
    private CheckSelfCollider _checkSelfCollider;
    private bool _isPouse = false;
    private bool _isLoad = false;

    //UI変数
    private ScoreDirector _scoreDirector;
    private EnemyHPUI _enemyHPUI;
    private LifeUIDirector _lifeUIDirector;
    private BombUIDirector _bombUIDirector;

    //プレイヤー変数
    private AsyncOperationHandle<PlayerData> _loadPlayerData;
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

    //定数
    private const int NORMAL_TIMESCALE = 1;
    #endregion

    #region プロパティ
    public GameCurrentData CurrentData
    {
        get { return _currentData; }
        set { _currentData = value; }
    }

    public ScoreDirector ScoreDirector
    {
        get { return _scoreDirector; }
    }

    public bool IsPouse
    {
        get { return _isPouse; }
        set { _isPouse = value; }
    }
    #endregion

    #region メソッド
    private void Awake()
    {
        //データ保存用クラスを生成する
        _currentData = new GameCurrentData();
        _checkSelfCollider = new CheckSelfCollider(this);
    }

    private IEnumerator Start()
    {
        //ロードを行う
        _isLoad = false;
        yield return StartCoroutine(LoadAnyObject());

        //ロード後の処理を行う
        InstanceLoadObject();
    }

    /// <summary>
    /// ロードを行う
    /// </summary>
    private IEnumerator LoadAnyObject()
    {
        //スクリプタブルオブジェクトをロードする
        _loadPlayerData = Addressables.LoadAssetAsync<PlayerData>("PlayerData");
        _loadEnemyData = Addressables.LoadAssetAsync<EnemyData>("BossData");

        while(!_loadEnemyData.IsDone || !_loadPlayerData.IsDone)
        {
            yield return null;
        }

        //ロード失敗した場合の処理を行う
        if(_loadPlayerData.Status != AsyncOperationStatus.Succeeded)
        {
            yield break;
        }

        if(_loadEnemyData.Status != AsyncOperationStatus.Succeeded)
        {
            yield break;
        }

        //ロードしたデータを記録
        _playerData = _loadPlayerData.Result;
        _enemyData = _loadEnemyData.Result;

        _isLoad = true;
    }

    /// <summary>
    /// ロード終了後の処理を行う
    /// </summary>
    private void InstanceLoadObject()
    {
        //UIクラスを生成する
        _scoreDirector = new ScoreDirector();
        _enemyHPUI = new EnemyHPUI(_enemyData.MaxHP);
        _lifeUIDirector = new LifeUIDirector(_playerData.MaxLife);
        _bombUIDirector = new BombUIDirector(_playerData.MaxBomb);

        //各オブジェクトを生成する
        _playerBulletData = _playerData.BulletData;
        _player = Instantiate(_playerData.Player, _playerData.PlayerInstancePosition, Quaternion.identity);
        _enemyParent = new GameObject("EnemyParent");
        _enemy = Instantiate(_enemyData.Enemy, _enemyData.EnemyInstancePosition, Quaternion.identity, _enemyParent.transform);
        if (_enemyData.SubEnemies.Length != 0)
        {
            //エネミーの部位がある場合、すべて生成
            _subEnemies = new GameObject[_enemyData.SubEnemies.Length];
            for (int i = 0; i < _enemyData.SubEnemies.Length; i++)
            {
                _subEnemies[i] = Instantiate(_enemyData.SubEnemies[i], _enemyData.SubEnemyInstancePosition[i], Quaternion.identity, _enemyParent.transform);
            }
        }

        //各管理クラスのインスタンスを生成する
        _playerController = new PlayerController(this,_checkSelfCollider,_playerData, _player, _playerBulletData);
        _enemyController = new EnemyController(this,_checkSelfCollider,_enemy, _subEnemies, _enemyParent, _enemyData);
    }

    private void Update()
    {
        //ロード前は動かさない
        if (!_isLoad)
        {
            return;
        }

        if (_isPouse)
        {
            return;
        }

        _checkSelfCollider.OnUpdate();
        _playerController.OnUpdate();
        _enemyController.OnUpdate();
        _enemyHPUI.ChangeUI(_enemyController.HP);
    }

    private void FixedUpdate()
    {
        //ロード前は動かさない
        if (!_isLoad)
        {
            return;
        }

        _playerController.OnFixedUpdata();
        _enemyController.OnFixedUpdate();
    }

    /// <summary>
    /// ボスが倒されるか残機がゼロ未満になったら呼ばれ、ゲームオーバー画面に遷移する
    /// </summary>
    public void OverGame()
    {
        //プレイヤーのインプットアクションを無効化する
        _playerController.OnDisable();

        //ロードしたデータをリリースする
        Addressables.Release(_loadPlayerData);
        Addressables.Release(_loadEnemyData);
        _scoreDirector.ReleaseLoadData();
        _bombUIDirector.ReleaseLoadData();
        _lifeUIDirector.ReleaseLoadData();
        _enemyHPUI.ReleaseLoadData();

        //コライダー判定シングルトンのデータをリセットする
        _checkSelfCollider.ClearCollisionData();

        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
	/// アクティブなすべての弾を消す
	/// </summary>
	public void ClearAllBullet()
    {
        _currentData.HomingDirector.ClearBullets();
        _currentData.DiffusionDirector.ClearBullets();
        _currentData.TargetDirector.ClearBullets();
    }

    /// <summary>
    /// エネミーが2段階目になっているか
    /// </summary>
    public bool IsEnemyHPMin()
    {
        return _enemyData.MinHP >= _enemyController.HP;
    }

    /// <summary>
    /// エネミーが1段階目になっているか
    /// </summary>
    public bool IsEnemyHPMid()
    {
        return _enemyData.MidHP >= _enemyController.HP;
    }

    /// <summary>
    /// プレイヤーの弾の連射数を返す
    /// </summary>
    /// <returns>弾の連射数</returns>
    public int GetPlayerBulletCount()
    {
        if(_scoreDirector.Score >= _playerData.MaxScore)
        {
            return _playerData.MaxBulletCount;
        }
        else if(_scoreDirector.Score >= _playerData.MidScore)
        {
            return _playerData.MidBulletCount;
        }
        else
        {
            return _playerData.NormalBulletCount;
        }
    }

    /// <summary>
    /// 残機UIの非表示処理を呼ぶ中継
    /// </summary>
    public void RemoveLife()
    {
        _lifeUIDirector.RemoveLifeImage();
    }

    /// <summary>
    /// ボムUIの非表示処理を呼ぶ中継
    /// </summary>
    public void RemoveBomb()
    {
        _bombUIDirector.RemoveBombImage();
    }

    /// <summary>
    /// ポーズ、ポーズ解除を行う
    /// </summary>
    public void ChangePouse()
    {
        if (_isPouse)
        {
            _isPouse = false;
            Time.timeScale = NORMAL_TIMESCALE;
        }
        else
        {
            _isPouse= true;
            Time.timeScale = 0;
        }
    }
    #endregion
}