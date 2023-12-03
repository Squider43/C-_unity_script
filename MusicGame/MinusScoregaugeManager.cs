using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinusScoregaugeManager : MonoBehaviour
{

    //スライダーを格納する
    public Slider scoregauge;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントからscoregaugeを検出する
        scoregauge = GameObject.Find("scoregauge").GetComponent<Slider>();
        //0からスタートする
        scoregauge.value = 0;
    }

    //衝突した時
    private void OnCollisionEnter(Collision collision)
    {
        //"Player"タグのオブジェクトに衝突した場合
        if (collision.gameObject.tag == "Player")
        {
            //0.002減少する
            scoregauge.value -= 0.04f;
        }
    }

}