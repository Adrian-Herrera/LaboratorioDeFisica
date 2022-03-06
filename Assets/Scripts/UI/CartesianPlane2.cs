using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartesianPlane2 : MonoBehaviour
{
    [SerializeField] private GameObject Line;
    [SerializeField] private GameObject MainObject;
    private List<GameObject> DivisionsLines = new List<GameObject>();
    private GameObject MainLine;
    private float distanceCenterToBorder, screenSize;
    private float LineWidth;
    private float cameraScale;
    private RectTransform _MainLineRT;

    public enum PlaneType
    {
        MRUV, CaidaLibre, Parabolico
    }
    public PlaneType type;
    void Start()
    {
        cameraScale = 1;
        CreateMainLine();
    }
    void Update()
    {

    }
    private void getPositions()
    {
        // Debug.Log("Camera.main.orthographicSize: " + Camera.main.orthographicSize);
        // distanceCenterToBorder = (16 * Camera.main.orthographicSize / 9);
        // Debug.Log("distanceCenterToBorder: " + distanceCenterToBorder);
        // screenSize = GetComponent<RectTransform>().rect.width;
        // Debug.Log("screenSize: " + screenSize);
        // MainObject.transform.localScale = new Vector3((Camera.main.orthographicSize / 10), (Camera.main.orthographicSize / 10));
    }
    private void CreateMainLine()
    {
        MainLine = Instantiate(Line, transform);
        MainLine.name = "MainLine";
        _MainLineRT = MainLine.GetComponent<RectTransform>();
        cameraScale = Camera.main.orthographicSize / 10;
        UpdateSizes();
    }
    private void UpdateSizes()
    {
        float HorizontalDistance = (16 * Camera.main.orthographicSize / 9);
        float VerticalDistance = Camera.main.orthographicSize;
        if (type == PlaneType.MRUV)
        {
            _MainLineRT.SetAnchorMiddle();
            LineWidth = VerticalDistance / 50;
            _MainLineRT.sizeDelta = new Vector2(HorizontalDistance * 2, LineWidth);
            Camera.main.GetComponent<Transform>().position = new Vector3(0, 0, -10);
        }
        else if (type == PlaneType.CaidaLibre)
        {
            _MainLineRT.SetAnchorBottom();
            LineWidth = VerticalDistance / 50;
            _MainLineRT.sizeDelta = new Vector2(LineWidth, VerticalDistance * 2);
            Camera.main.GetComponent<Transform>().position = new Vector3(HorizontalDistance / 2, VerticalDistance * 0.7f, -10);
        }
        MainObject.transform.localScale = new Vector3(1 * cameraScale, 1 * cameraScale, 0);
        CreateDivisions();
    }
    private void CreateDivisions()
    {
        //Limpiar lista
        foreach (var item in DivisionsLines)
        {
            Destroy(item);
        }
        DivisionsLines.Clear();

        if (type == PlaneType.MRUV)
        {
            float AvailableSpace = 16 * Camera.main.orthographicSize / 9;
            float temp = 0;
            float distance = CalculateRulerDistance(AvailableSpace);
            while (temp < AvailableSpace)
            {
                GameObject newDiv = Instantiate(Line, MainLine.transform).setPosition(new Vector2(temp, 0));
                newDiv.GetComponent<RectTransform>().sizeDelta = new Vector2(0.1f, 1);
                newDiv.GetComponent<RectTransform>().localScale = new Vector3(1 * cameraScale, 1 * cameraScale, 0);
                newDiv.GetComponentInChildren<TMP_Text>().SetText(temp.ToString());
                DivisionsLines.Add(newDiv);
                temp += distance;
            }
        }
        else if (type == PlaneType.CaidaLibre)
        {
            float AvailableSpace = Camera.main.orthographicSize + Camera.main.orthographicSize / 2;
            float temp = 0;
            float distance = CalculateRulerDistance(AvailableSpace);
            while (temp < AvailableSpace)
            {
                GameObject newDiv = Instantiate(Line, transform).setPosition(new Vector2(0, temp));
                RectTransform divRT = newDiv.GetComponent<RectTransform>();
                Transform divText = newDiv.transform.Find("Text");
                divRT.sizeDelta = new Vector2(1, 0.1f);
                divRT.localScale = new Vector3(1 * cameraScale, 1 * cameraScale, 0);
                divText.GetComponent<RectTransform>().SetAnchorRigth();
                divText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1, 0);
                newDiv.GetComponentInChildren<TMP_Text>().SetText(temp.ToString());
                DivisionsLines.Add(newDiv);
                temp += distance;
            }
        }
    }
    public void ZoomIn()
    {
        Camera.main.orthographicSize -= 20;
        cameraScale = Camera.main.orthographicSize / 10;
        EventManager.Current.ChangeZoom(cameraScale);
        UpdateSizes();
    }
    public void ZoomOut()
    {
        Camera.main.orthographicSize += 20;
        cameraScale = Camera.main.orthographicSize / 10;
        EventManager.Current.ChangeZoom(cameraScale);
        UpdateSizes();
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
