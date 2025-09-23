using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 状況の判定を行い、成功か失敗を返す
/// </summary>
public class ConditionNode : NodeBace
{
	#region 変数  
	private ConditionType _myType;
	private ConditionBace _myJudge;
	#endregion

	#region プロパティ

	#endregion

	#region メソッド  
	public enum ConditionType
    {
		IsDieUpper,  //エネミー上部位が破壊されたか
        IsDieDowner, //エネミー下部位が破壊されたか
        IsDieAll,　　//エネミー両部位が破壊されたか
    }

	public ConditionNode(ConditionType type)
    {
		_myType = type;

        //タイプにより、判断スクリプトを取得する
        switch (_myType)
        {
			case ConditionType.IsDieUpper:
                _myJudge = new IsDieUpper();
            break;

            case ConditionType.IsDieDowner:
                _myJudge = new IsDieDownner();
            break;

            case ConditionType.IsDieAll:
                _myJudge = new IsDieUpperDowner();
            break;
        }
    }

    public override void OnStart()
    {
        _myJudge.GameDirector = _gameDirector;
    }

    public override NodeState OnUpdate()
    {
		return _myJudge.IsJudge();
    }
    #endregion
}