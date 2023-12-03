using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//『using UnityEngine.UI;』が本来必要だが、音ゲーはTextMeshProを使用している為『using TMPro;』が必要になる

public class Scoretext : MonoBehaviour
{
    //スコア加算したいtextと紐付ける
    public float Score;
    public TextMeshProUGUI scorecounttext;//tmpro用格納

    //Scoretextにアクセスして実行する
    internal static void SetScoretext()
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //0からスタートする
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //textのフォーマットを設定する
        scorecounttext.text = string.Format("{0}", Score);
    }
}