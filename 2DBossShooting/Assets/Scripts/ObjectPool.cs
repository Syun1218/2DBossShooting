using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 渡されたオブジェクトを生成し、プール管理する
/// </summary>
public class ObjectPool
{
	#region 変数
	private GameObject _bulletParent;
	private Queue<GameObject> _pool = new Queue<GameObject>();
	private GameObject _poolObject;

	//定数
	private readonly Vector2 _poolInstancePosition = new Vector2(-50, 0);
	#endregion

	#region プロパティ

	#endregion

	#region メソッド
	public ObjectPool(GameObject poolObject,int count)
    {
		//ヒエラルキーの整理用に弾を子としてもつ空オブジェクトを生成
		_bulletParent = new GameObject("PlayerBulletParent");

		//渡されたオブジェクトをプレイヤーの子として生成し、キューに記録
		for(int i = 0;i < count; i++)
        {
			_poolObject = GameObject.Instantiate(poolObject, _poolInstancePosition,poolObject.transform.rotation,_bulletParent.transform);
			_pool.Enqueue(_poolObject);
        }
    }

	/// <summary>
	/// 渡されたオブジェクトをプールにしまう
	/// </summary>
	/// <param name="obj">返却するオブジェクト</param>
	public void EnqueueObject(GameObject obj)
    {
        obj.transform.position = _poolInstancePosition;
		_pool.Enqueue(obj);
    }

	/// <summary>
	/// オブジェクトを貸し出す
	/// </summary>
	/// <param name="pos">オブジェクトのポジション</param>
	/// <returns>貸し出されたオブジェクト</returns>
	public GameObject DequeueObject(Vector2 pos)
    {
		if(_pool.Count == 0)
        {
			return null;
        }

		_poolObject = _pool.Dequeue();
        _poolObject.transform.position = pos;
		return _poolObject;
    }
	#endregion
}