using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisionmusic : MonoBehaviour
{
    //音データの再生装置を格納する
    private new AudioSource audio;

    //音データを格納する
    [SerializeField]
    private AudioClip sound;

    //Start is called before the first frame update
    private void Start()
    {
        //コンポーネントから再生装置を検出する
        audio = gameObject.AddComponent<AudioSource>();

    }

    //衝突したとき
    private void OnCollisionEnter(Collision collision)
    {
        //"Player"タグがついているオブジェクトに衝突した場合
        if (collision.gameObject.tag == "Player")
        {
            //音を鳴らす
            audio.PlayOneShot(sound);

            //0.2秒後にオブジェクトが消える
            Destroy(gameObject, 0.2f);
        }
    }
}