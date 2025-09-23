using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SEDirector : MonoBehaviour
{
	#region 変数
	private static SEDirector _instance;

	//SE変数
    private AudioSource _audioSource;
	[SerializeField] private AudioClip _buttonClip;
	[SerializeField] private AudioClip _shotClip;
	[SerializeField] private AudioClip _bombClip;
	[SerializeField] private AudioClip _destroyClip;
	#endregion

	#region プロパティ
	public static SEDirector Instance
	{
		get { return _instance; }
	}
    #endregion

    #region メソッド
    private void Awake()
    {
        //シングルトンの設定
        if (_instance == null)
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
    /// ゲーム開始時の音
    /// </summary>
    public void PlayButtonSE()
    {
        _audioSource.PlayOneShot(_buttonClip);
    }

    /// <summary>
    /// プレイヤーの弾発射時の音
    /// </summary>
    public void PlayShotSE()
    {
        _audioSource.PlayOneShot(_shotClip);
    }

    /// <summary>
    /// ボム使用時の音
    /// </summary>
    public void PlayBombSE()
    {
        _audioSource.PlayOneShot(_bombClip);
    }

    /// <summary>
    /// 部位破壊時の音
    /// </summary>
    public void PlayDestroySE()
    {
        _audioSource.PlayOneShot(_destroyClip);
    }
    #endregion
}