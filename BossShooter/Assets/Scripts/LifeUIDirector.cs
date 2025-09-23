using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

/// <summary>
/// 残機UIの管理クラス
/// </summary>
public class LifeUIDirector
{
    #region 変数
    private AsyncOperationHandle<GameObject> _loadCanvas;
    private Canvas _lifeCanvas;
    private Image[] _lifeImages;
    private int _lifeCount;

    //定数
    private const int ARRAY_OFFSET = 1;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public LifeUIDirector(int life)
    {
        //カンバスをロードする
        _loadCanvas = Addressables.LoadAssetAsync<GameObject>("LifeCanvas");
        _lifeCanvas = _loadCanvas.WaitForCompletion().GetComponent<Canvas>();
        _lifeCount = life - ARRAY_OFFSET;

        //残機イメージを取得
        _lifeCanvas = GameObject.Instantiate(_lifeCanvas);
        _lifeImages = new Image[life];
        for(int i = 0;i < life; i++)
        {
            _lifeImages[i] = _lifeCanvas.transform.GetChild(i).GetComponent<Image>();
        }
    }

    /// <summary>
    /// 残機のイメージを一つ減らす
    /// </summary>
    public void RemoveLifeImage()
    {
        _lifeImages[_lifeCount].enabled = false;
        _lifeCount--;
    }
    #endregion
}