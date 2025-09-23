using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMDirector : MonoBehaviour
{
	#region 変数
	private static BGMDirector _instance;

	//BGM変数
	private AudioSource _audioSource;
	[SerializeField] private AudioClip _titleBGM;
	[SerializeField] private AudioClip _MainBGM;
	[SerializeField] private AudioClip _GameOverBGM;
	#endregion

	#region プロパティ
	public static BGMDirector Instance
	{
		get { return _instance; }
	}
    #endregion

    #region メソッド
    private void Awake()
    {
		//シングルトンの設定
        if(_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		_audioSource = gameObject.GetComponent<AudioSource>();
    }

	/// <summary>
	/// タイトルBGM再生
	/// </summary>
	public void PlayTitleBGM()
	{
		_audioSource.clip = _titleBGM;
		_audioSource.Play();
	}

	/// <summary>
	/// メインBGM再生
	/// </summary>
	public void PlayMainBGM()
	{
		_audioSource.clip = _MainBGM;
		_audioSource.Play();
	}

	/// <summary>
	/// ゲームオーバーBGM再生
	/// </summary>
	public void PlayGameOverBGM()
	{
		_audioSource.clip = _GameOverBGM;
		_audioSource.Play();
	}
    #endregion
}