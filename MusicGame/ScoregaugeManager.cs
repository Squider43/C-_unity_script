using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoregaugeManager : MonoBehaviour
{
    //スライダーを格納する
    public Slider scoregauge;

    // Start is called before the first frame update
    void Start()
    {
        //ヒエラルキーからscoregaugeを検出する
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
            //0.008増加する
            scoregauge.value += 0.008f;
        }

        if (collision.gameObject.tag == "resetbox")
        {
            //オブジェクトが消える
            scoregauge.value -= 0.040f;
        }
    }

}