using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 行動待機処理
/// </summary>
public class OnIdol : ActionBace
{
    #region 変数
    private float _targetIdolTime;
    private float _nowIdolTime = 0;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    public override void Initialized()
    {
        //合計待機時間をリセットする
        _nowIdolTime = 0;

        //エネミーの状態によって待機時間を変化させる
        if (GameDirector.Instance.IsEnemyHPMin())
        {
            _targetIdolTime = _enemyData.MinIdolTime;
        }
        else if (GameDirector.Instance.IsEnemyHPMid())
        {
            _targetIdolTime = _enemyData.MidIdolTime;
        }
        else
        {
            _targetIdolTime = _enemyData.NormalIdolTime;
        }
    }

    public override NodeBace.NodeState OnAction()
    {
        //待機時間を計測
        _nowIdolTime += Time.deltaTime;

        if(_nowIdolTime >= _targetIdolTime)
        {
            //合計待機時間が目標待機時間に達した場合、成功を返す
            return NodeBace.NodeState.Success;
        }

        //合計待機時間が目標待機時間に達していない場合、実行中を返す
        return NodeBace.NodeState.Running;
    }

    public override void End()
    {
        
    }
    #endregion
}