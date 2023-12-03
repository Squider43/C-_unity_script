using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Changescene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //98秒後にChangeScene関数を実行する
        Invoke("ChangeScene", 98);
    }
    // Update is called once per frame
    void ChangeScene()
    {
        //titleシーンをロードする
        SceneManager.LoadScene("Title");
    }
}