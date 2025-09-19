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
		Idol,   //プレイヤー未発見時の待機
		Shot,   //プレイヤー発見時の攻撃
    }

	public ActionNode(ActionType action)
    {
		_myAction = action;

        //アクションのタイプにより、行動スクリプトを取得する
        switch (_myAction)
        {
			case ActionType.Idol:
                //_myControll = new OnIdol();
			break;

			case ActionType.Shot:
                //_myControll = new OnShot();
			break;
        }
    }

    public override void OnStart()
    {
        _myControll.Owner = _aiOwner;

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