using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CartesianPlane : MonoBehaviour
{
    private Vector3 LPosition, RPosition;
    private List<GameObject> Vlines = new List<GameObject>();
    public GameObject point;
    private float distanceCenterToBorder, screenSize;
    private float cameraSize;
    public GameObject Hline;

    [SerializeField]
    private GameObject VLine;
    private void Awake()
    {
        cameraSize = Camera.main.orthographicSize;
    }
    // Start is called before the first frame update
    void Start()
    {
        getPositions();
        DrawLines();
        // startMovement();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void getPositions()
    {
        Debug.Log("Camera.main.orthographicSize: " + Camera.main.orthographicSize);
        distanceCenterToBorder = (16 * Camera.main.orthographicSize / 9);
        Debug.Log("distanceCenterToBorder: " + distanceCenterToBorder);
        screenSize = GetComponent<RectTransform>().rect.width;
        Debug.Log("screenSize: " + screenSize);
        point.transform.localScale = new Vector3((Camera.main.orthographicSize / 10), (Camera.main.orthographicSize / 10));
    }

    private void DrawLines()
    {
        foreach (var item in Vlines)
        {
            Destroy(item);
        }
        Vlines.Clear();
        float temp = 0;
        float screenTemp = 0;
        Vlines.Add(Instantiate(VLine, new Vector3(0, 0), Quaternion.identity, Hline.transform));
        // Debug.Log(Mathf.Round(distanceCenterToBorder));
        // Debug.Log(Mathf.RoundToInt(distanceCenterToBorder));
        float distance = CalculateRulerDistance(distanceCenterToBorder);
        while (temp < distanceCenterToBorder)
        {
            temp += distance;
            // Debug.Log("temp: " + (distanceCenterToBorder / 6) + " -> " + temp);
            screenTemp = temp * (screenSize / 2) / distanceCenterToBorder;
            InstantiateLines(screenTemp, temp.ToString());
        }


    }

    private void InstantiateLines(float position, string text)
    {
        Vector3 Pos = new Vector3(position, 0, 0);
        // Debug.Log(Pos);
        // GameObject gor = Instantiate(VLine, Pos, Quaternion.identity, Hline.transform);
        GameObject gor = Instantiate(VLine, this.transform);
        gor.GetComponent<RectTransform>().anchoredPosition = new Vector2(position, 0);
        // gor.transform.position = new Vector2(position, 0);
        gor.GetComponentInChildren<TMP_Text>().text = text;
        Vlines.Add(gor);
    }


    private void startMovement()
    {
        point.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0);
        // point.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0);
    }
    public void ZoomIn()
    {
        Camera.main.orthographicSize -= 40;
        getPositions();
        DrawLines();
    }
    public void ZoomOut()
    {
        Camera.main.orthographicSize += 40;
        getPositions();
        DrawLines();
    }

    private float CalculateRulerDistance(float size)
    {
        int firstDigit, numberOfDigits = 0;
        float temp = size;
        while (temp > 0)
        {
            temp = (int)temp / 10;
            numberOfDigits++;
        }
        firstDigit = (int)(size / Mathf.Pow(10, (numberOfDigits - 1)));
        if (firstDigit < 2)
        {
            return (2 * Mathf.Pow(10, (numberOfDigits - 2)));
        }
        else if (firstDigit < 4)
        {
            return (5 * Mathf.Pow(10, (numberOfDigits - 2)));
        }
        else
        {
            return (10 * Mathf.Pow(10, (numberOfDigits - 2)));
        }
    }
}
