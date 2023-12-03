using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public class Data
    {
        public Mark Mark;
        public int Number;
    }

    // Start is called before the first frame update
    public enum Mark
    {
        Heart,
        Diamond,
        Spade,
        Crub,
    }

    public bool IsReverse = false;
    [Range(1, 13)]//次の数字をスライダーで表示
    public int Number = 1;
    public Mark CurrentMark = Mark.Heart;

    public bool IsLarge = false;
    public int UseNumber//数字をget以下で定義
    {
        get
        {
            if (Number > 10) return 10;
            if(Number == 1)
            {
                return IsLarge ? 11 : 1;
            }
            return Number;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Number == 1)
        {
            IsLarge = !IsLarge;
            SetCard(Number, CurrentMark, IsReverse);
        }
    }

    public void SetCard(int number, Mark mark, bool isReverse)
    {
        Number = Mathf.Clamp(number, 1, 13);//numberの範囲を指定
        CurrentMark = mark;
        IsReverse = isReverse;

        //CardプレハブのGameObjectを更新する。

        //カードの裏表に合わせて色などを設定する
        var image = GetComponent<Image>();
        if (IsReverse)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!IsReverse);
        }

        //マークに合わせてGameObjectを設定する
        var markObj = transform.Find("Mark");//プレハブのCardの子要素のMarkのGameObjectを取得
        var markText = markObj.GetComponent<Text>();
        switch (CurrentMark)
        {
            case Mark.Heart:
                markText.text = "❤️";
                markText.color = Color.red;
                break;
            case Mark.Diamond:
                markText.text = "♦️";
                markText.color = Color.red;
                break;
            case Mark.Spade:
                markText.text = "♠️";
                markText.color = Color.black;
                break;
            case Mark.Crub:
                markText.text = "♣️";
                markText.color = Color.black;
                break;
        }

        //数字に合わせてGameObjectを設定する
        var numberObj = transform.Find("NumberText");
        var numberText = numberObj.GetComponent<Text>();
        if (Number == 1)
        {
            numberText.text = "A";
        }
        else if (Number == 11)
        {
            numberText.text = "J";
        }
        else if (Number == 12)
        {
            numberText.text = "Q";
        }
        else if (Number == 13)
        {
            numberText.text = "K";
        }
        else
        {
            numberText.text = Number.ToString();//文字列として表示
        }

        var optionalNumberObj = transform.Find("OptionalNumberText");//子要素から探す
        optionalNumberObj.gameObject.SetActive(!IsReverse && Number == 1);
        if (Number == 1)
        {
            var optionalNumberText = optionalNumberObj.GetComponent<Text>();
            optionalNumberText.text = UseNumber.ToString();
        }
    }

    private void OnValidate()//中の関数をインスペクタの変更に対して即座に反映するメソッド
    {
        SetCard(Number, CurrentMark, IsReverse);
    }

}