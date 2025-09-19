using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 円形のコライダーとしての判定用データを持たせる
/// </summary>
public class SelfCircleCollider : MonoBehaviour,CollisionInterface
{
	#region 変数
	private Vector2 _centerPoint;
	private float _radius;
	private ObjectType _myObjectType;
    private CollisionInterface _myCollisionInterface;

    //弾の衝突後処理用変数
    private bool _isCollision = false;
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

    public Vector2 CenterPoint
    {
        get { return _centerPoint; }
    }

    public CollisionInterface MyCollisionInterface
    {
        get { return _myCollisionInterface; }
        set { _myCollisionInterface = value; }
    }

    public bool IsCollsion
    {
        get { return _isCollision; }
        set { _isCollision = value; }
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

    private void Awake()
    {
        _myCollisionInterface = this;
    }

    public void FixedUpdate()
    {
        _centerPoint = gameObject.transform.position;
    }

    /// <summary>
    /// 衝突した弾をプールに返却する処理
    /// </summary>
    public void OnCollision(SelfCircleCollider.ObjectType otherType)
    {
        _isCollision = true;
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