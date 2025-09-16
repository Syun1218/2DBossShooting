using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー操作を管理し、各アクションスクリプトへの中継を行う
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region 変数
	private Actions _actions;
    private Vector2 _moveValue;
    private bool _isRawSpeed;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    private void Awake()
    {
        //スクリプタブルオブジェクトからデータを取得


        //インプットアクションの初期化
        _actions = new Actions();

        _actions.Enable();

        //入力イベントの定義
        _actions.Player.Move.performed += OnMove;
        _actions.Player.Move.canceled += OnMove;
        _actions.Player.LowSpeed.performed += OnLawSpeed;
        _actions.Player.LowSpeed.canceled += OnLawSpeed;

        //アクションクラスのインスタンスを生成

    }

    private void OnDisable()
    {
        //インプットアクションの終了
        _actions.Disable();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// 移動入力の取得
    /// </summary>
    /// <param name="context">入力ベクトル</param>
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 低速移動入力の取得
    /// </summary>
    private void OnLawSpeed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isRawSpeed = true;
        }
        else
        {
            _isRawSpeed = false;
        }
    }
    #endregion
}