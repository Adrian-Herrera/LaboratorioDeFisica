using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Line : MonoBehaviour, IPointerDownHandler
{

    public GameObject DataGrid;
    private TMP_InputField[] UIData;

    public TMP_InputField Dist_total, Time_total;

    public TMP_Text LabelSelectedSegment;
    public soCar dataCar;
    public GameObject segment;

    private float lineSize, initialPos;
    private int selectedSegment;

    public List<GameObject> segmentsList;

    void Awake()
    {
        UIData = DataGrid.GetComponentsInChildren<TMP_InputField>();
    }


    // Start is called before the first frame update
    void Start()
    {
        dataCar.numberSegments = 1;
        initialPos = 0f;
        selectedSegment = 0;
        showData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initialSize(float size)  //call by DistanceBars script
    {
        lineSize = size;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (lineSize));

        // Debug.Log(lineSize);
        drawSegment();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        // Debug.Log(segmentsList.IndexOf(eventData.pointerCurrentRaycast.gameObject));
        selectedSegment = segmentsList.IndexOf(eventData.pointerCurrentRaycast.gameObject);
        if (selectedSegment >= 0)
        {

            showData();
            LabelSelectedSegment.text = "Parte N° " + (segmentsList.IndexOf(eventData.pointerCurrentRaycast.gameObject) + 1);
        }

    }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log(" Entrar.");
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     Debug.Log(" Salir.");
    // }

    public void drawSegment()
    {
        var emptyData = 0;
        foreach (var item in segmentsList)
        {
            Destroy(item);
        }
        segmentsList.Clear();

        if (dataCar.numberSegments > 1)
        {
            for (int i = 0; i < dataCar.numberSegments; i++)
            {
                if (dataCar.dataTable[i, 5] == 0)
                {
                    emptyData++;
                }
            }
        }
        // Debug.Log(emptyData);

        for (int i = 0; i < dataCar.numberSegments; i++)
        {
            var sg = Instantiate(segment, this.transform);
            segmentsList.Add(sg);
            sg.name = "Segment" + (i + 1);

            if (dataCar.dataTable[i, 5] == 0)
            {
                SetPositionSegment(sg, lineSize / dataCar.numberSegments);
                sg.GetComponentInChildren<TMP_Text>().text = "X <sub>" + (i + 1) + "</sub>";

            }
            else
            {
                var SizePercent = ((dataCar.dataTable[i, 5] * 100) / dataCar.x_total) / 100;
                SetPositionSegment(sg, (lineSize - ((lineSize / dataCar.numberSegments) * emptyData)) * SizePercent);
                sg.GetComponentInChildren<TMP_Text>().text = dataCar.dataTable[i, 5].ToString();
            }

        }
        initialPos = 0;
    }

    public void addSegment()
    {
        if (dataCar.numberSegments < 3)
        {
            dataCar.numberSegments += 1;

            drawSegment();

        }
    }

    public void removeSegment()
    {
        if (dataCar.numberSegments > 1)
        {
            dataCar.numberSegments -= 1;

            for (int i = 0; i < dataCar.dataTable.GetLength(1); i++)
            {
                dataCar.dataTable[dataCar.numberSegments, i] = 0;
            }

            drawSegment();

        }
    }

    public void showData()
    {
        // 0.Vo 1.Vf 2.Acc 3.Time 4.Xo 5.Xf
        for (int i = 0; i < UIData.Length; i++)
        {
            UIData[i].text = dataCar.dataTable[selectedSegment, i].ToString();
        }
    }

    private void reloadData()
    {
        dataCar.Dist_Total();
        Dist_total.text = dataCar.x_total.ToString();

    }

    public void changeTextVo(string data)
    {
        if (data != "")
        {
            dataCar.dataTable[selectedSegment, 0] = float.Parse(data);
            reloadData();
        }
    }
    public void changeTextVf(string data)
    {
        dataCar.dataTable[selectedSegment, 1] = float.Parse(data);
        reloadData();
    }
    public void changeTextAcc(string data)
    {
        dataCar.dataTable[selectedSegment, 2] = float.Parse(data);
        reloadData();
    }
    public void changeTextTime(string data)
    {
        dataCar.dataTable[selectedSegment, 3] = float.Parse(data);
        reloadData();
    }
    public void changeTextXo(string data)
    {
        dataCar.dataTable[selectedSegment, 4] = float.Parse(data);
        reloadData();
    }
    public void changeTextXf(string data)
    {
        dataCar.dataTable[selectedSegment, 5] = float.Parse(data);
        reloadData();
        drawSegment();
    }

    public void changeTextXTotal(string data)
    {
        dataCar.x_total = float.Parse(data);
    }
    public void changeTextTimeTotal(string data)
    {
        dataCar.time_total = float.Parse(data);
    }

    private void SetPositionSegment(GameObject segment, float size)
    {
        segment.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        segment.GetComponent<RectTransform>().anchoredPosition = new Vector2(initialPos, 0);
        initialPos += size;
    }
}
