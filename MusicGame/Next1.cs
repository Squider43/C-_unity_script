using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトを非アクティブにする
        this.gameObject.SetActive(false);
        //95秒後にOpen関数を実行する
        Invoke("Open", 95);//曲が95秒
    }

    //
    void Open()
    {
        //オブジェクトをアクティブにする
        this.gameObject.SetActive(true);
    }
}