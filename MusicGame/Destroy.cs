using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //衝突した時
    private void OnCollisionEnter(Collision collision)
    {
        //"resetbox"タグに衝突した場合
        if (collision.gameObject.tag == "resetbox")
        {
            //オブジェクトが消える
            Destroy(this.gameObject);
        }
    }
}