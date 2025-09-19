using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///子ノードを順に実行していき実行結果でSuccessが返る、または全ての子ノードを実行するまで処理を続ける
/// </summary>
public class SelectorNode : NodeBace
{
	#region 変数  
    private int _listIndex = 0;

    private bool _isStart = false;

    private NodeState _currentState = NodeState.Running;

    #endregion

    #region プロパティ

    #endregion

    #region メソッド  
    public override void OnStart()
    {
        _listIndex = 0;
        _isStart = true;
    }

    public override NodeState OnUpdate()
    {
        //リスト内のノードを順に実行し、評価する

        if (_isStart)
        {
            _childNodes[_listIndex].OnStart();
            _isStart = false;
        }

        _currentState = _childNodes[_listIndex].OnUpdate();

        //子ノードの実行結果によって処理を決める
        switch (_currentState)
        {
            //子ノードの実行結果が成功なら自身も親ノードに成功を返す
            case NodeState.Success:
                _childNodes[_listIndex].OnEnd();

            return NodeState.Success;

            //子ノードの実行結果が失敗かつ全ての子ノードが実行済みなら自身も親ノードに失敗を返す
            case NodeState.Fail:
                _childNodes[_listIndex].OnEnd();
                _isStart = true;

                _listIndex++;
                if(_listIndex == _childNodes.Count)
                {
                    return NodeState.Fail;
                }
            break;
        }

        //子ノードが実行中であれば自身も実行中とする
        return NodeState.Running;
    }
    #endregion
}