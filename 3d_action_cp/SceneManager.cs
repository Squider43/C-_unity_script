using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour

{



    public grid prefabTile;
    public GameObject canvas;
    public int boardHeight = 10;
    public int boardWidth = 10;
    public float gridsize = 5;
    bool fieldexistence = false;
    bool [,] _iseven;
    public int[,] _boardInfo;
    public bool currentLBgrid = false;
    public int startstatenum = 0;

    void Start()
    {
        fieldexistence = true;
    }

    public Camera camera_object; //カメラを取得
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物

    void Update()
    {


        if(fieldexistence)
        {
            var FI = CreateField(boardHeight,boardWidth);
            _iseven = FI.IsEven;
            _boardInfo = FI.board;
            fieldexistence = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入

            if (Physics.Raycast(ray, out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                string objectName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる
                if(hit.collider.gameObject.tag == "Grid")
                {
                    var GridID = OutputgridID(hit.collider.gameObject);
                    if(_iseven[GridID.x,GridID.y])
                    {
                        currentLBgrid = true;//これがtrueなら市松にぬれる
                    }

                    Debug.Log(currentLBgrid);

                }
                Debug.Log(objectName); //オブジェクト名をコンソールに表示
            }

            
        }

    }


    public (int x,int y) OutputgridID(GameObject tile)//タイルのx,yの数字を返す
    {
        string org = tile.name;
        string id = org.Substring(5);
        int x = int.Parse(id.Substring(0, id.IndexOf("_")));
        int y = int.Parse(id.Substring(id.IndexOf("_")));
        return (x, y);
    }


    private static (int[,] board, bool[,] IsEven) CreateField(int height, int width)
    {
        int[,] board = new int[height, width];
        bool[,] IsEven = new bool[height, width];//すべてfalse
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                board[x, y] = 0;
                grid tile = Instantiate(prefabTile, new Vector3(5 * x, 5 * y, 0), Quaternion.identity);
                tile.name = "grid_" + x + "_" + y;
                tile.transform.SetParent(canvas.transform, false);
                if (x + y % 2 == 0)
                {
                    IsEven[x, y] = true;
                }
            }
        }
        return (board, IsEven);
    }

}
