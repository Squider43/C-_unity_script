using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public Tile prefabTile;
    public GameObject canvas;
    public Tile currentTile;

    public float LineWidth = 5;
    public int CountWidth = 10;
    public int CountHeight = 10;
    public float BoardWidth = 400; //外線一つ除いたサイズ
    public float BoardHeight = 400;

    // Start is called before the first frame update
    void Start()
    {
        float PanelLength = Mathf.Min(BoardWidth / CountWidth, BoardHeight / CountHeight);
        var TileSize = prefabTile.GetComponent<RectTransform>().sizeDelta;
        TileSize.x = PanelLength - LineWidth;
        TileSize.y = PanelLength - LineWidth;
        prefabTile.GetComponent<RectTransform>().sizeDelta = TileSize;

        for (int i = 0; i < CountWidth; i++)
        {
            for (int j = 0; j < CountHeight; j++)
            {
                float x = i * PanelLength - PanelLength * (CountWidth - 1) / 2;
                float y = j * PanelLength - PanelLength * (CountHeight - 1) / 2;

                Vector3 pos = new Vector3(x, y, 0);
                Tile tile = Instantiate(prefabTile, pos, Quaternion.identity);
                tile.transform.SetParent(canvas.transform, false);
                Debug.Log(tile.transform);
                i++; j++;
                tile.name = "tile_" + i.ToString() + "_" + j.ToString();
                if((i + j) % 2 == 0)
                {
                    tile.IsEven = true;
                }
                Debug.Log(tile.name);
                i--; j--;
            }
        }

    }
    // Update is called once per frame

    public Camera camera_object; //カメラを取得
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入

            if (Physics.Raycast(ray, out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                string objectName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる
                Debug.Log(objectName); //オブジェクト名をコンソールに表示
            }
        }
        
    }




    private int[] OutputTileID(Tile tile)//[i,j]の配列を返す
    {
        string org = tile.name;
        string id = org.Substring(5);
        int i = int.Parse(id.Substring(0, id.IndexOf("_")));
        int j = int.Parse(id.Substring(id.IndexOf("_")));
        int[] number_ij = { i, j };
        return number_ij;
    }


}