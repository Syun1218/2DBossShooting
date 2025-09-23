using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
	#region 変数
	private Actions _actions;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    private void Awake()
    {
        //インプットアクションを有効化する
        _actions = new Actions();
        _actions.Enable();

        _actions.Director.SceneChange.performed += OnAnyPress;
    }

    private void OnAnyPress(InputAction.CallbackContext context)
    {
        //インプットアクションを無効化する
        _actions.Disable();

        if(SceneManager.GetActiveScene().name == "Title")
        {
            //現在がタイトルシーンならメインシーンへ移行
            SceneManager.LoadScene("MainGame");
        }
        else if(SceneManager.GetActiveScene().name == "GameOver")
        {
            //現在がオーバーシーンならタイトルシーンへ移行
            SceneManager.LoadScene("Title");
        }
    }
    #endregion
}