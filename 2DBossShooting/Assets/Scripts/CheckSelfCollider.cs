using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 自作の円形コライダー同士の衝突判定を行う
/// </summary>
public class CheckSelfCollider : MonoBehaviour
{
	#region 変数
	private static CheckSelfCollider _instance;
	private List<SelfCircleCollider> _colliders = new List<SelfCircleCollider>();
	#endregion

	#region プロパティ
	public static CheckSelfCollider Instance
    {
        get { return _instance; }
    }
    #endregion

    #region メソッド
    private void Awake()
    {
        //シングルトンの設定を行う
        if(_instance is null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// コライダーを持つオブジェクトを受け取り、衝突判定対象に含める
    /// </summary>
    /// <param name="obj">コライダーを持つオブジェクト</param>
    public void SetColliderObject(GameObject obj)
    {
		_colliders.Add(obj.GetComponent<SelfCircleCollider>());
    }

	/// <summary>
	/// 渡されたオブジェクトを衝突判定の対象から外す
	/// </summary>
	public void RemoveColliderObject(GameObject obj)
    {
        _colliders.Remove(obj.GetComponent<SelfCircleCollider>());
    }

    private void Update()
    {
        //衝突判定を行う
        
    }
    #endregion
}