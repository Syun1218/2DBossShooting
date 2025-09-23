using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class EnemyHPUI
{
    #region 変数
    private AsyncOperationHandle<GameObject> _loadCanvas;
	private Canvas _uiCanvas;
    private Slider _hpSlider;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public EnemyHPUI(float maxHP)
	{
        //カンバスをロードする
        _loadCanvas = Addressables.LoadAssetAsync<GameObject>("HPCanvas");
		_uiCanvas = _loadCanvas.WaitForCompletion().GetComponent<Canvas>();

		//スライダーを取得する
		_uiCanvas = GameObject.Instantiate(_uiCanvas);
		_hpSlider = _uiCanvas.transform.GetChild(0).GetComponent<Slider>();
		_hpSlider.maxValue = maxHP;
    }

	/// <summary>
	/// スライダーに値を反映する
	/// </summary>
	/// <param name="hp">エネミーの体力</param>
	public void ChangeUI(int hp)
	{
		_hpSlider.value = hp;
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