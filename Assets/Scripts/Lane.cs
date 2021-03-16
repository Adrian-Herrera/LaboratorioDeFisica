using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{

    [SerializeField]
    private GameObject Terrain = null;
    [SerializeField]
    private GameObject startPoint = null;
    [SerializeField]
    private GameObject endPoint = null;
    [SerializeField]
    // private GameObject Car = null;
    private int Horizontal, Vertical;
    private float terrainSize;

    public soCar _soCar;

    // Start is called before the first frame update
    void Start()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * Screen.width / Screen.height;

        // Horizontal = GameObject.Find("MainArea").GetComponent<TerrainSize>().Horizontal;
        //Debug.Log("Horizontal: " + Horizontal);
        spawnPoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnPoints()
    {
        // Se instancia el punto inicial
        // startPoint.transform.SetParent(this.transform, false);
        //Debug.Log(startPoint.transform.localPosition);
        startPoint.transform.localPosition = new Vector3(0 - Horizontal + 2, 0f, 0f);
        //Debug.Log(startPoint.transform.localPosition);

        // Se instancia el punto final
        endPoint.transform.localPosition = new Vector3(startPoint.transform.localPosition.x + _soCar.finalPosition, 0f, 0f);
        // endPoint.transform.SetParent(this.transform, false);

        // El terreno se ubica al medio de los dos puntos y aumenta su tamaño
        terrainSize = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        float midPoint = (startPoint.transform.position.x + endPoint.transform.position.x) / 2;
        Terrain.transform.localPosition = new Vector3(midPoint, 0, 0);
        Terrain.transform.localScale = new Vector3(terrainSize, 1f, 0f);
        GetComponent<BoxCollider2D>().size = new Vector2(terrainSize, 1f);

        // Se genera la linea que medira la distancia total del terreno
        LineRenderer lnr = Terrain.AddComponent<LineRenderer>();
        lnr.positionCount = 2;
        lnr.startWidth = 0.2f;
        lnr.SetPosition(0, startPoint.transform.position - new Vector3(0f, 1f, 0f));
        lnr.SetPosition(1, endPoint.transform.position - new Vector3(0f, 1f, 0f));

        // Se genera el texto que indicara el tamaño del terreno
        GameObject Text = new GameObject("sizeText");
        Text.transform.SetParent(Terrain.transform);
        TextMesh sizeText = Text.AddComponent<TextMesh>();
        sizeText.text = "100 mts.";
        sizeText.characterSize = 0.5f;
        sizeText.anchor = TextAnchor.UpperCenter;
        sizeText.transform.localPosition = new Vector3(0f, -1.3f, 0f);


    }

}
