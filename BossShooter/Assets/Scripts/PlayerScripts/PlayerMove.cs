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
	private Vector2 _nowPosition;

	//移動制限定数
	private const float MAX_X_POSITION = 8.4f;
	private const float MIN_X_POSITION = -8.4f;
	private const float MAX_Y_POSITION = 4.7f;
	private const float MIN_Y_POSITION = -4.7f;
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
		_nowPosition = _playerTransform.position;

		//低速移動の場合と通常の場合で処理を分ける
        if (isLowSpeed)
        {
			_nowPosition.x += moveValue.x * _lowSpeed;
			_nowPosition.y += moveValue.y * _lowSpeed;
        }
        else
        {
			_nowPosition.x += moveValue.x * _normalSpeed;
			_nowPosition.y += moveValue.y * _normalSpeed;
		}

		//プレイヤーが制限範囲外に出る場合、制限範囲内になるように値を補正する
		if(_nowPosition.x > MAX_X_POSITION)
        {
			_nowPosition.x = MAX_X_POSITION;
        }

		if(MIN_X_POSITION > _nowPosition.x)
        {
			_nowPosition.x = MIN_X_POSITION;
        }

		if(_nowPosition.y > MAX_Y_POSITION)
        {
			_nowPosition.y = MAX_Y_POSITION;
        }

		if(MIN_Y_POSITION > _nowPosition.y)
        {
			_nowPosition.y = MIN_Y_POSITION;
        }

		//座標を反映する
		_playerTransform.position = _nowPosition;
    }
	#endregion
}