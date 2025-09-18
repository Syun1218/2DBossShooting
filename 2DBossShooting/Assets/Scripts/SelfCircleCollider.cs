using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 円形のコライダーとしての判定用データを持たせる
/// </summary>
public class SelfCircleCollider : MonoBehaviour
{
	#region 変数
	private Vector2 _centerPoint;
	private float _radius;
	private ObjectType _myObjectType;
	#endregion

	#region プロパティ
	/// <summary>
	/// サークルコライダーの半径
	/// </summary>
	public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

	public ObjectType MyObjectType
    {
        get { return _myObjectType; }
        set { _myObjectType = value; }
    }
	#endregion

	#region メソッド
	/// <summary>
	/// このオブジェクトの役割
	/// </summary>
	public enum ObjectType
    {
		Player,        //プレイヤーオブジェクト
		PlayerBullet,  //プレイヤーの弾オブジェクト
		Enemy,         //エネミーオブジェクト
		EnemyBullet,   //エネミーの弾オブジェクト
    }

    public void FixedUpdate()
    {
        _centerPoint = gameObject.transform.position;
    }
    #endregion

    private void OnDrawGizmos()
    {
        // ギズモの色をオブジェクトタイプごとに変える
        switch (_myObjectType)
        {
            case ObjectType.Player: Gizmos.color = Color.green; break;
            case ObjectType.PlayerBullet: Gizmos.color = Color.cyan; break;
            case ObjectType.Enemy: Gizmos.color = Color.red; break;
            case ObjectType.EnemyBullet: Gizmos.color = Color.yellow; break;
        }

        //円を描画
        UnityEditor.Handles.color = Gizmos.color;
        UnityEditor.Handles.DrawWireDisc(_centerPoint, Vector3.forward, _radius);
    }
}