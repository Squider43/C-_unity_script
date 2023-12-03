using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    // 判定内にプレイヤーがいる
    [HideInInspector] public bool isOn = false;//isOnで判定
    private string playerTag = "Player";//"Player"というタグをplayerTagという文字列に収納

    #region//接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag) //衝突したもののTagを読み取り
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn = false;
        }
    }
    #endregion
}

//つまり、これは衝突したことをbool isOnとして出力するスクリプト