using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// スコアを保存し、UIに反映する
/// </summary>
public class ScoreDirector
{
	#region 変数
	private int _score = 0;
	private Canvas _scoreCanvas;
	private TMP_Text _scoreText;
	#endregion

	#region プロパティ
	public int Score
	{
		get { return _score; }
	}
	#endregion

	#region メソッド
	public ScoreDirector(Canvas canvas)
    {
		//カンバスをロードする
		_scoreCanvas = canvas;

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