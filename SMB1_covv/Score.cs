using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText = null;//テキスト形式の変数(scoreText)を設定
    private int oldScore = 0;

    void Start()
    {
        scoreText = GetComponent<Text>();//このスクリプトがついているテキストの項目を取得
        if (GManager.instance != null)
        {
            scoreText.text = "Score " + GManager.instance.score;//スクリプトGMのinstanceのscoreの項目
        }
        else
        {
            Debug.Log("ゲームマネージャー置き忘れてるよ！");
            Destroy(this);//複数のスコアが生成されないように、これを破壊
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oldScore != GManager.instance.score)
        {
            scoreText.text = "Score " + GManager.instance.score;
            oldScore = GManager.instance.score;
        }
    }
}

//つまり、このスクリプトはScoreを出力するようなので、Score ●●を表すTextにつける