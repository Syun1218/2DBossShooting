using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// スコアを保存し、UIに反映する
/// </summary>
public class ScoreDirector
{
	#region 変数
	private AsyncOperationHandle<GameObject> _loadCanvas;
	private int _score = 0;
	private Canvas _scoreCanvas;
	private TMP_Text _scoreText;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public ScoreDirector()
    {
		//カンバスをロードする
		_loadCanvas = Addressables.LoadAssetAsync<GameObject>("ScoreCanvas");
		_scoreCanvas = _loadCanvas.WaitForCompletion().GetComponent<Canvas>();

		//カンバスを生成し、子オブジェクトをテキストとして記録
		_scoreCanvas = GameObject.Instantiate(_scoreCanvas);
		_scoreText = _scoreCanvas.transform.GetChild(0).GetComponent<TMP_Text>();
		_scoreText.SetText("0");
    }

	public void AddScore(int add)
    {
		_score += add;
		_scoreText.SetText(_score.ToString());
    }
	#endregion
}