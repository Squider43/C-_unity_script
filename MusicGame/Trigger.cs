using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public void PointerDown()
    {
        //オブジェクトをアクティブにする
        this.gameObject.SetActive(true);
    }

    public void PointerUp()
    {
        //オブジェクトを非アクティブにする
        this.gameObject.SetActive(false);
    }
}