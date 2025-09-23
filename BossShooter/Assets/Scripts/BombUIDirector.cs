using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

/// <summary>
/// ボムUIの管理クラス
/// </summary>
public class BombUIDirector
{
    #region 変数
    private AsyncOperationHandle<GameObject> _loadCanvas;
    private Canvas _bombCanvas;
    private Image[] _bombImages;
    private int _bombCount;

    //定数
    private const int ARRAY_OFFSET = 1;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public BombUIDirector(int bomb)
    {
        //カンバスをロードする
        _loadCanvas = Addressables.LoadAssetAsync<GameObject>("BombCanvas");
        _bombCanvas = _loadCanvas.WaitForCompletion().GetComponent<Canvas>();
        _bombCount = bomb - ARRAY_OFFSET;

        //ボムイメージを取得
        _bombCanvas = GameObject.Instantiate(_bombCanvas);
        _bombImages = new Image[bomb];
        for (int i = 0; i < bomb; i++)
        {
            _bombImages[i] = _bombCanvas.transform.GetChild(i).GetComponent<Image>();
        }
    }

    /// <summary>
    /// ボムのイメージを一つ減らす
    /// </summary>
    public void RemoveBombImage()
    {
        _bombImages[_bombCount].enabled = false;
        _bombCount--;
    }

    /// <summary>
    /// データをリリースする
    /// </summary>
    public void ReleaseLoadData()
    {
        Addressables.Release(_loadCanvas);
    }
    #endregion
}