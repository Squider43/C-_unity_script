using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class grid : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    //static public PSceneManager _SceneManager;
    private bool xy = false;//偶奇情報
    public static SceneManager obj;
    bool LB = false;//偶奇情報


    private int statenum = 0;//状態管理の数字
    public int _startstatenum = 0;
    /*
     * 0 白マス
     * 1 黒マス
     * 2 緑マス
     * 3 白赤マス
     * 4 黒赤マス
     * 5 緑赤マス
     * 6 灰マス
     * で0-1/1-2/2-0/3-0/4-1/5-2/6-2の遷移
     */





    public class ColorGet//色指定のメソッド
    {
        /// <summary>
        /// RGB を 0 ～ 255 で指定したカラー値を取得
        /// </summary>
        /// <param name="r">赤</param>
        /// <param name="g">緑</param>
        /// <param name="b">青</param>
        public static Color Rgb(int r, int g, int b)
        {
            return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f);
        }
        /// <summary>
        /// カラーを #RRGGBB の形で取得
        /// </summary>
        /// <param name="hexrgb">16進数のカラー値 RRGGBB</param>
        public static Color Hex(int hexrgb)
        {
            int r = (hexrgb >> 16) & 0xff;
            int g = (hexrgb >> 8) & 0xff;
            int b = hexrgb & 0xff;
            return Rgb(r, g, b);
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        xy = GetgridID(this.gameObject).xy;
        _startstatenum = statenum;
        statenum = ChangeColor(statenum);//色変更
        obj.currentLBgrid = xy;//偶奇情報をPSceneManagerに送る
        obj.startstatenum = _startstatenum;


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        xy = GetgridID(this.gameObject).xy;
        if (!obj.currentLBgrid == xy) return;//偶奇の一致する時だけ考える

        int num = obj.startstatenum;


        Color imagecolor = Color.white;
        switch (num)//最初のマスと同じ色にして、上程の数字も変更
        {
            case 0:
                num = 0;
                imagecolor = Color.white;
                break;
            case 1:
                num = 1;
                imagecolor = Color.black;
                break;
            case 2:
                num = 2;
                imagecolor = ColorGet.Hex(0x51FF7E);
                break;
                /*
                case 3:
                    num = 3;
                    imagecolor = Color.white;
                    break;
                case 4:
                    num = 4;
                    imagecolor = Color.black;
                    break;
                case 5:
                    num = 5;
                    imagecolor = ColorGet.Hex(0x51FF7E);
                    break;
                */

                /*
                 case 6:
                    num = 6;
                    imagecolor = ColorGet.Hex(0x51FF7E);
                    break;
                */
        }
        statenum = num;
        GetComponent<Image>().color = imagecolor;

    }

















    private (int x, int y) OutputgridID(GameObject tile)//タイルのx,yの数字を返す
    {
        string org = tile.name;
        string id = org.Substring(5);
        int x = int.Parse(id.Substring(0, id.IndexOf("_")));
        int y = int.Parse(id.Substring(id.IndexOf("_")));
        return (x, y);
    }



    private int ChangeColor(int num)//色の変更
    {
        Color imagecolor = Color.white;
        switch (num)
        {
            case 0:
                num = 1;
                imagecolor = Color.black;
                break;
            case 1:
                num = 2;
                imagecolor = ColorGet.Hex(0x51FF7E);
                break;
            case 2:
                num = 0;
                imagecolor = Color.white;
                break;
            case 3:
                num = 0;
                imagecolor = Color.white;
                break;
            case 4:
                num = 1;
                imagecolor = Color.black;
                break;
            case 5:
                num = 2;
                imagecolor = ColorGet.Hex(0x51FF7E);
                break;
            case 6:
                num = 2;
                imagecolor = ColorGet.Hex(0x51FF7E);
                break;
        }
        gameObject.GetComponent<Image>().color = imagecolor;

        return num;
    }

    private (int x, int y, bool xy) GetgridID(GameObject tile)
    {
        string org = tile.name;
        string id = org.Substring(5);
        int x = int.Parse(id.Substring(0, id.IndexOf("_")));
        int y = int.Parse(id.Substring(id.IndexOf("_")));
        bool xy = x + y % 2 == 0;
        return (x, y, xy);
    }
    
}
