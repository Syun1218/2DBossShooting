using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 実際の敵AIの行動処理
/// </summary>
public class ActionNode : NodeBace
{
	#region 変数  
	private ActionType _myAction;
	private ActionBace _myControll;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド  
	public enum ActionType
    {
		Idol,           //プレイヤー未発見時の待機
		HomingShot,     //プレイヤーを追跡する弾を発射
        DiffusionShot,  //拡散する弾を発射
        TargetShot,     //プレイヤーのいる位置に直進する弾を発射
    }

	public ActionNode(ActionType action)
    {
		_myAction = action;

        //アクションのタイプにより、行動スクリプトを取得する
        switch (_myAction)
        {
			case ActionType.Idol:
                _myControll = new OnIdol();
            break;

			case ActionType.HomingShot:
                _myControll = new OnHomingShot();
            break;

            case ActionType.DiffusionShot:
            _myControll = new OnDiffusionShot();
            break;

            case ActionType.TargetShot:
            _myControll = new  OnTargetShot();
            break;
        } 
    }

    public override void OnStart()
    {
        _myControll.Owner = _aiOwner;
        _myControll.EnemyData = _enemyData;
        _myControll.Pools = _pools;
        _myControll.Index = _index;
        _myControll.GameDirector = _gameDirector;

        _myControll.Initialized();
    }

    public override NodeState OnUpdate()
    {
        return _myControll.OnAction();
    }

    public override void OnEnd()
    {
        _myControll.End();
    }
    #endregion
}