using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    public Card CardPrefab;
    public GameObject Dealer;
    public GameObject Player;

    public GameObject BetsInputDialog;
    public InputField BetsInput;

    public Button BetsInputOKButton;

    public Text BetsText;
    public Text PointText;

    public Text ResultText;
    public float WaitResultSeconds = 2;

    public GameObject RetryButton;

    //?????
    public int StartPoint = 20;
    int currentPoint;
    int currentBets;

    public Text GoalPointText;
    public int goalPoint = 40;

    public AudioClip flip_se;
    public AudioClip win_se;
    public AudioClip lose_se;
    public AudioClip winner_se;
    public AudioClip loser_se;
    AudioSource audioSource;

    [Min(100)]
    public int ShuffleCount = 100;//100以上で自由に変更可

    List<Card.Data> cards;//dataのリスト可？


    public enum Action
    {
        WaitAction = 0,
        Hit = 1,
        Stand = 2,
    }

    Action CurrentAction = Action.WaitAction;

    public void SetAction(int action) //actionで行動を管理
    {
        CurrentAction = (Action)action;　//Enum数字指定の時は(Action)で指定
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        BetsInput.onValidateInput = BetsInputOnValidateInput;
        BetsInput.onValueChanged.AddListener(BetsInputOnValueChanged);//値が変わるとOKボタンを有効かするかどうかチェック
        GoalPointText.text = "目標ポイント "+goalPoint.ToString();
    }

    public void RetryGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BlackJackScene");
    }

    char BetsInputOnValidateInput(string text, int startIndex, char addedChar)//数字かどうかチェック
    {
        if (!char.IsDigit(addedChar)) return '\0';//数字じゃないなら拒否
        return addedChar;
    }

    void BetsInputOnValueChanged(string text)
    {
        BetsInputOKButton.interactable = false;　//数値がないと不可
        if (int.TryParse(BetsInput.text, out var bets))//テキストを整数に変換できるならbetsで出力
        {
            if (0 < bets && bets <= currentPoint)
            {
                BetsInputOKButton.interactable = true;
            }
        }
    }


    IEnumerator GameLoop()
    {
        currentPoint = StartPoint;
        BetsText.text = "0";
        PointText.text = currentPoint.ToString();

        ResultText.gameObject.SetActive(false);

        while (true)
        {
            InitCards(); //山札作成

            yield return null;//?????????????

            //ベットが決まるまで待機
            do
            {
                BetsInputDialog.SetActive(true);
                yield return new WaitWhile(() => BetsInputDialog.activeSelf);//BetsInputDialog.activeSelfがfalseになるまで待機
                //入力でOKを押すとfalse?
                //テキストが整数かどうかチェック
                if (int.TryParse(BetsInput.text, out var bets))
                {
                    if (0 < bets && bets <= currentPoint)
                    {
                        currentBets = bets;
                        break;//入力が正しければdo-whileから抜ける
                    }
                }
            } while (true);
            Debug.Log("bet入力完了");
            //インプット画面を消す
            BetsInputDialog.SetActive(false);
            BetsText.text = currentBets.ToString();

            //カードを全体に配る
            DealCards();

            // 手番待ち
            bool waitAction = true;
            bool doWin = false;

            Debug.Log("配布");
            //hit stand
            do
            {
                CurrentAction = Action.WaitAction;
                Debug.Log("待機");
                yield return new WaitWhile(() => CurrentAction == Action.WaitAction);//waitじゃなくなるまで
                Debug.Log("クリック");
                // ????????????????
                switch (CurrentAction)
                {
                    case Action.Hit:
                        PlayerDealCard();
                        waitAction = true;
                        if(!CheckPlayerCard())
                        {
                            waitAction = false;
                            doWin = false;
                        }
                        break;//switchから脱出
                    case Action.Stand:
                        waitAction = false;
                        doWin = StandAction();
                        break;
                    default://それ以外
                        waitAction = true;
                        throw new System.Exception("知らない行動をしようとしています。");
                }
            } while (waitAction);//waitActionがfalseになれば終了

            //結果を判定
            yield return new WaitForSeconds(0.3f);

            ResultText.gameObject.SetActive(true);
            if (doWin)
            {
                currentPoint += currentBets;
                ResultText.text = "Win!! +" + currentBets;
                audioSource.PlayOneShot(win_se);
            }
            else
            {
                currentPoint -= currentBets;
                ResultText.text = "Lose... -" + currentBets;
                audioSource.PlayOneShot(lose_se);
            }
            PointText.text = currentPoint.ToString();

            yield return new WaitForSeconds(WaitResultSeconds);
            ResultText.gameObject.SetActive(false);
            //さらにくりかえす

            if (currentPoint <= 0)
            {
                ResultText.gameObject.SetActive(true);
                ResultText.text = "Game Over...";
                audioSource.PlayOneShot(loser_se);
                yield return new WaitForSeconds(WaitResultSeconds);
                RetryButton.SetActive(true);
                break;
            }
            if (currentPoint >= goalPoint)
            {
                ResultText.gameObject.SetActive(true);
                ResultText.text = "Game Clear!!";
                audioSource.PlayOneShot(winner_se);
                yield return new WaitForSeconds(WaitResultSeconds);
                RetryButton.SetActive(true);
                break;
            }
        }
    }





    Coroutine _gameLoopCoroutine;

    private void Start()
    {
        _gameLoopCoroutine = StartCoroutine(GameLoop());
    }





    //以下関数の定義


    void InitCards()
    {
        cards = new List<Card.Data>(13 * 4);
        var marks = new List<Card.Mark>() {
            Card.Mark.Heart,
            Card.Mark.Diamond,
            Card.Mark.Spade,
            Card.Mark.Crub,
        };

        foreach (var mark in marks)
        {
            for (var num = 1; num <= 13; ++num)
            {
                var card = new Card.Data()
                {
                    Mark = mark,
                    Number = num,
                };
                cards.Add(card);
            }
        }

        ShuffleCards();
    }

    void ShuffleCards()
    {
        //シャッフルする
        var random = new System.Random();
        for (var i = 0; i < ShuffleCount; ++i)
        {
            var index = random.Next(cards.Count);
            var index2 = random.Next(cards.Count);//２つの違う変数

            //入れ替え
            var tmp = cards[index];
            cards[index] = cards[index2];
            cards[index2] = tmp;
        }
    }//以上二つで山札を作成

    Card.Data DealCard()　//dealcard関数の定義:cardsから1マイ抽選
    {
        if (cards.Count <= 0) return null;//山札があれば1マイ取り出す

        var card = cards[0];
        cards.Remove(card);
        return card;
    }

    void DealCards()　//最初にカードを配る関数Dealcardsの定義
    {
        foreach (Transform card in Dealer.transform)　//dealrのカード破壊
        {
            Object.Destroy(card.gameObject);
        }

        foreach (Transform card in Player.transform)　//playerのカード破壊
        {
            Object.Destroy(card.gameObject);
        }


        {
            //delerもカードの配り直し
            var holeCardObj = Object.Instantiate(CardPrefab, Dealer.transform);//cardprefabを元にdealerのtfに生成
            holeCardObj.IsLarge = holeCardObj.Number == 1; //ディーラーのエースカードは必ず11にする
            var holeCard = DealCard();
            holeCardObj.SetCard(holeCard.Number, holeCard.Mark, true);

            var upCardObj = Object.Instantiate(CardPrefab, Dealer.transform);
            upCardObj.IsLarge = upCardObj.Number == 1; //ディーラーのエースカードは必ず11にする
            var upCard = DealCard();
            upCardObj.SetCard(upCard.Number, upCard.Mark, false);
        }

        {
            //playerのカードの配り直し2マイ
            for (var i = 0; i < 2; ++i)
            {
                var cardObj = Object.Instantiate(CardPrefab, Player.transform);
                var card = DealCard();
                cardObj.SetCard(card.Number, card.Mark, false);
            }
        }
    }
    void PlayerDealCard()　//playerに追加のカード
    {
        var cardObj = Object.Instantiate(CardPrefab, Player.transform);
        var card = DealCard();
        cardObj.SetCard(card.Number, card.Mark, false);
    }

     bool CheckPlayerCard()
    {
        var sumNumber = 0;
        foreach (var card in Player.transform.GetComponentsInChildren<Card>())
        {
            sumNumber += card.UseNumber;
        }
        return (sumNumber < 21);
    }

    bool StandAction()
    {
        var sumPlayerNumber = 0;
        foreach (var card in Player.transform.GetComponentsInChildren<Card>())
        {
            sumPlayerNumber += card.UseNumber;
        }

        var sumDealerNumber = 0;
        foreach (var card in Dealer.transform.GetComponentsInChildren<Card>())
        {
            sumDealerNumber += card.UseNumber;
            if (card.IsReverse)
            {//裏面のカードを表向きにする
                audioSource.PlayOneShot(flip_se);
                card.SetCard(card.Number, card.CurrentMark, false);
            }
        }
        if (!CheckPlayerCard()) return false;
        return sumPlayerNumber > sumDealerNumber;//ならtrue ひとしければ負け
    }

}