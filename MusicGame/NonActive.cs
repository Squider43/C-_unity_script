using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonActive : MonoBehaviour
{
    //衝突したとき
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        //オブジェクトを非アクティブにする
        this.gameObject.SetActive(false);
    }
}