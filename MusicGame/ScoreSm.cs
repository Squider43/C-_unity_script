using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSm : MonoBehaviour
{
    //スコアやScoretextをアタッチしているscorecounttextを格納する
    public Scoretext Sm;//scoretextクラスはscoretextをアタッチしていることが条件

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントからscorecounttextを検出する
        Sm = GameObject.Find("scorecounttext").GetComponent<Scoretext>();
    }

    //衝突した時
    private void OnCollisionEnter(Collision collision)
    {
        //スコアが0や"Player"タグのオブジェクトに衝突した場合
        if (collision.gameObject.tag == "Player")
        {
            //スコア20増加する
            Sm.Score += 20;
        }
    }
}