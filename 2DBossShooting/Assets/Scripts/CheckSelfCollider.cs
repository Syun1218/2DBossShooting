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

    //判定用変数
    private float _addRadius;
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
        for(int i = 0;i < _colliders.Count; i++)
        {
            switch (_colliders[i].MyObjectType)
            {
                case SelfCircleCollider.ObjectType.Player:
                    DesidePlayerPair(i);
                break;

                case SelfCircleCollider.ObjectType.PlayerBullet:
                    DesidePBulletPair(i);
                break;

                case SelfCircleCollider.ObjectType.Enemy:
                    DesideEnemyPair(i);
                break;

                case SelfCircleCollider.ObjectType.EnemyBullet:
                    DesideEBulletPair(i);
                break;
            }
        }
    }

    /// <summary>
    /// プレイヤーオブジェクトと判定を行うオブジェクトの決定
    /// </summary>
    private void DesidePlayerPair(int i)
    {
        for (int j = 0; j < _colliders.Count; j++)
        {
            //同じオブジェクトを見ている場合、スキップする
            if (j == i)
            {
                continue;
            }

            //エネミーの弾とエネミーオブジェクトとのみ判定を行う
            if (_colliders[j].MyObjectType == SelfCircleCollider.ObjectType.EnemyBullet || _colliders[j].MyObjectType == SelfCircleCollider.ObjectType.Enemy)
            {
                if (CheckCollision(_colliders[i], _colliders[j]))
                {
                    //衝突処理を行う
                    _colliders[i].MyCollisionInterface.OnCollision(_colliders[j].MyObjectType);
                    _colliders[j].MyCollisionInterface.OnCollision(_colliders[i].MyObjectType);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーの弾と判定を行うオブジェクトの決定
    /// </summary>
    private void DesidePBulletPair(int i)
    {
        for (int j = 0; j < _colliders.Count; j++)
        {
            //同じオブジェクトを見ている場合、スキップする
            if (j == i)
            {
                continue;
            }

            //エネミーの弾とエネミーオブジェクトとのみ判定を行う
            if (_colliders[j].MyObjectType == SelfCircleCollider.ObjectType.EnemyBullet || _colliders[j].MyObjectType == SelfCircleCollider.ObjectType.Enemy)
            {
                if (CheckCollision(_colliders[i], _colliders[j]))
                {
                    //衝突処理を行う
                    _colliders[i].MyCollisionInterface.OnCollision(_colliders[j].MyObjectType);
                    _colliders[j].MyCollisionInterface.OnCollision(_colliders[i].MyObjectType);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// エネミーオブジェクトと判定を行うオブジェクトの決定
    /// </summary>
    private void DesideEnemyPair(int i)
    {
        for (int j = 0; j < _colliders.Count; j++)
        {
            //同じオブジェクトを見ている場合、スキップする
            if (j == i)
            {
                continue;
            }

            //プレイヤーの弾オブジェクトとのみ判定を行う
            if (_colliders[j].MyObjectType == SelfCircleCollider.ObjectType.PlayerBullet)
            {
                if (CheckCollision(_colliders[i], _colliders[j]))
                {
                    //衝突処理を行う
                    _colliders[i].MyCollisionInterface.OnCollision(_colliders[j].MyObjectType);
                    _colliders[j].MyCollisionInterface.OnCollision(_colliders[i].MyObjectType);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// エネミーの弾と判定を行うオブジェクトの決定
    /// </summary>
    private void DesideEBulletPair(int i)
    {
        for (int j = 0; j < _colliders.Count; j++)
        {
            //同じオブジェクトを見ている場合、スキップする
            if (j == i)
            {
                continue;
            }

            //プレイヤーの弾とプレイヤーオブジェクトとのみ判定を行う
            if (_colliders[j].MyObjectType == SelfCircleCollider.ObjectType.Player || _colliders[j].MyObjectType == SelfCircleCollider.ObjectType.PlayerBullet)
            {
                if (CheckCollision(_colliders[i], _colliders[j]))
                {
                    //衝突処理を行う
                    _colliders[i].MyCollisionInterface.OnCollision(_colliders[j].MyObjectType);
                    _colliders[j].MyCollisionInterface.OnCollision(_colliders[i].MyObjectType);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 渡された二つのオブジェクトの衝突判定を行う
    /// </summary>
    /// <param name="my">衝突判定を行う側のオブジェクト</param>
    /// <param name="other">衝突判定を行われる側のオブジェクト</param>
    /// <returns>衝突している場合、trueを返す</returns>
    private bool CheckCollision(SelfCircleCollider my,SelfCircleCollider other)
    {
        //それぞれの半径を足したものと中心点からの距離を判定し半径の合計の方が大きい場合衝突しているとみなす
        _addRadius = my.Radius + other.Radius;
        return (_addRadius * _addRadius >= (my.CenterPoint - other.CenterPoint).sqrMagnitude);
    }
    #endregion
}