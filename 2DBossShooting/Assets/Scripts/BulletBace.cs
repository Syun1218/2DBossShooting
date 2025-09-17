using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 弾の移動管理クラスのベース
/// </summary>
public class BulletBace : MonoBehaviour
{
	#region 変数
	protected bool _isEnable = false;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	/// <summary>
	/// 弾を有効化する
	/// </summary>
	public void BulletEnabled()
    {
		_isEnable = true;
    }

	/// <summary>
	/// 弾を無効化する
	/// </summary>
	public void BulletDisabled()
    {
		_isEnable = false;
    }
	#endregion
}