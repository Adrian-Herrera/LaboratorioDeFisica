using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Car = null;
    [SerializeField]
    private List<Vector3Int> Tiles = null;
    [SerializeField]
    private TileBase TileTop = null;
    [SerializeField]
    private TileBase TileBottom = null;
    [SerializeField]
    private Tilemap tilemap = null;

    public float[,] Grid;
    private int usedLines, points;
    private GameObject PanelButton;
    //private List<Vector2Int> tilesPos;
    int Vertical, Horizontal, Columns, Rows;
    // Start is called before the first frame update
    void Start()
    {
        usedLines = 1;
        points = 0;
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * Screen.width / Screen.height;
        Columns = Horizontal * 2;
        Rows = Vertical * 2;
        Grid = new float[Columns, Rows];

        PanelButton = GameObject.Find("ButtonPanel");
        AddLine();


    }
    private void SpawnTile(int x, int y)
    {
        //Tilemap tilemap = GetComponent<Tilemap>();
        //GameObject g = new GameObject("x: " + x + " y: " + y);
        //Tiles.Add(g);
        //g.transform.position = new Vector3(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
        Vector3Int gridPos = new Vector3Int(x - (Horizontal), y - (Vertical), 0);
        //var s = g.AddComponent<SpriteRenderer>();
        //Debug.Log(gridPos);
        tilemap.SetTile(gridPos, y % 2 == 0 ? TileBottom : TileTop);
        Tiles.Add(gridPos);


    }

    public void AddLine()
    {

        for (int i = 0; i < Columns; i++)
        {
            for (int j = usedLines; j < usedLines + 2; j++)
            {
                SpawnTile(i, j);
                if (j == usedLines && i == 0)
                {
                    GameObject Point = new GameObject("Inicio " + points);
                    points += 1;
                    Point.transform.position = new Vector3(i - (Horizontal - 1f), j - (Vertical - 1f));
                    Instantiate(Car, Point.transform);

                }
                //Grid[i, j] = Random.Range(0.0f, 1.0f);
            }
        }
        usedLines += 3;
    }

    public void RemoveLine()
    {

        for (int i = 0; i < Columns; i++)
        {
            for (int j = usedLines - 2; j < usedLines; j++)
            {
                int leng = Tiles.Count;
                tilemap.SetTile(Tiles[leng - 1], null);
                Tiles.RemoveAt(leng - 1);
            }
        }
        GameObject Point = GameObject.Find("Inicio " + (points - 1));
        Destroy(Point);
        points -= 1;
        usedLines -= 3;
    }

}
