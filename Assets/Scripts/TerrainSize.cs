using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSize : MonoBehaviour
{

    [SerializeField]
    private GameObject Line = null;
    [SerializeField]
    private List<GameObject> listLines = null;

    public GameObject UI;

    private int Vertical, Columns, Rows, lines = 1;
    public int Horizontal;

    private float terrainSize;

    // Start is called before the first frame update
    void Start()
    {
        getScreenSize();
        Debug.Log("Horizontal TS: " + Horizontal);
        //spawnLines(lines);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void getScreenSize()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * Screen.width / Screen.height;
        Columns = Horizontal * 2;
        Rows = Vertical * 2;
    }

    private void spawnLines(float a)
    {
        Vector3 InitialPos = new Vector3(0f, a, 0f);
        clearLine();
        for (int i = 0; i < a; i++)
        {

            Vector3 temp = new Vector3(0f, 3f, 0f);
            // addLine(InitialPos);
            GameObject go = Instantiate(Line, InitialPos, Quaternion.identity);
            go.name = "Line" + (i + 1);
            listLines.Add(go);
            InitialPos -= temp;
        }

    }

    public void addLine()
    {
        if (lines < 3)
        {
            lines += 1;
            spawnLines(lines);
        }
        else
        {
            // TODO message error
        }
    }

    public void removeLine()
    {
        if (lines > 1)
        {
            lines -= 1;

            spawnLines(lines);
        }
        else
        {
            // TODO message error
        }
    }

    public void clearLine()
    {
        foreach (var line in listLines)
        {
            Destroy(line);
        }
        listLines.Clear();
    }
}
