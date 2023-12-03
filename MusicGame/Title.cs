using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        //ボタンが押された時、StartGame関数を実行する
        gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
    }
    // Update is called once per frame
    void StartGame()
    {
        //GameSceneをロードする
       SceneManager.LoadScene("GameScene");
    }
}