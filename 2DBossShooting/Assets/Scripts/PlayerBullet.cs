using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBullet : BulletBace
{
    #region 変数
    private Vector2 _nowPosition;
    #endregion

    #region プロパティ

    #endregion

    #region メソッド
    private void FixedUpdate()
    {
        //無効化されている場合処理を行わない
        if (!_isEnable)
        {
            return;
        }

        //弾を右方向に直進させる
        _nowPosition = gameObject.transform.position;
        _nowPosition += Vector2.right * 0.1f;
        gameObject.transform.position = _nowPosition;
    }
    #endregion
}