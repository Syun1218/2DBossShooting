using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーの移動処理クラス
/// </summary>
public class PlayerMove
{
	#region 変数
	private float _normalSpeed;
	private float _lowSpeed;
	private Transform _playerTransform;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public PlayerMove(float normalSpeed,float lowSpeed,Transform playerTransform)
    {
		_normalSpeed = normalSpeed;
		_lowSpeed = lowSpeed;
		_playerTransform = playerTransform;
    }

	/// <summary>
	/// プレイヤーの移動処理
	/// </summary>
	/// <param name="isLowSpeed">低速移動をするか</param>
	/// <param name="moveValue">移動方向と量</param>
	public void MovePlayer(bool isLowSpeed,Vector2 moveValue)
    {
        if (isLowSpeed)
        {

        }
        else
        {
			
        }
    }
	#endregion
}