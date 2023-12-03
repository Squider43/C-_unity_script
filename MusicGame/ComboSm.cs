using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSm : MonoBehaviour
{
    //コンボやCombotextをアタッチしているcombocounttextを格納する
    public Combotext Sm;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントからcombocounttextを検出する
        Sm = GameObject.Find("combocounttext").GetComponent<Combotext>();
        //0からスタート
    }

    //衝突した時
    private void OnCollisionEnter(Collision collision)
    {
        //コンボが0や"Player"タグのオブジェクトに衝突した場合
        if (collision.gameObject.tag == "Player")
        {
            //コンボ1増加する
            Sm.Combo += 1;
        }
    }
}