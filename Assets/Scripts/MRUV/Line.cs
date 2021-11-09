using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Line : MonoBehaviour, IPointerDownHandler
{

    public TMP_Text LabelSelectedSegment;
    public soCar soCar;
    public GameObject segment;

    private float lineSize, initialPos;

    public List<GameObject> segmentsList;

    // Start is called before the first frame update
    void Start()
    {
        soCar.numberSegments = 1;
        initialPos = 0f;
        soCar.selectedSegment = 0;
        // showData();
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
        soCar.selectedSegment = segmentsList.IndexOf(eventData.pointerCurrentRaycast.gameObject);
        if (soCar.selectedSegment >= 0)
        {

            // showData();
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
        // Elimina todos los segmentos de la lista y los GameObjects
        foreach (var item in segmentsList)
        {
            Destroy(item);
        }
        segmentsList.Clear();

        //Cuenta si los segmentos tienen un tamaño asignado por el usuario
        if (soCar.numberSegments > 1)
        {
            for (int i = 0; i < soCar.numberSegments; i++)
            {
                if (soCar.dataTable[i, 5] == 0)
                {
                    emptyData++;
                }
            }
        }

        //Instancia los segmentos de cada linea
        for (int i = 0; i < soCar.numberSegments; i++)
        {
            var sg = Instantiate(segment, this.transform);
            segmentsList.Add(sg);
            sg.name = "Segment" + (i + 1);

            if (soCar.dataTable[i, 5] == 0)
            {
                SetPositionSegment(sg, lineSize / soCar.numberSegments);
                sg.transform.Find("Panel").transform.Find("X").GetComponentInChildren<TMP_InputField>().text = "X <sub>" + (i + 1) + "</sub>";

                // Debug.Log(sg.transform.Find("Panel").transform.Find("X").GetComponentInChildren<TMP_InputField>().text);

            }
            else
            {
                var SizePercent = ((soCar.dataTable[i, 5] * 100) / soCar.x_total) / 100;
                SetPositionSegment(sg, (lineSize - ((lineSize / soCar.numberSegments) * emptyData)) * SizePercent);
                sg.GetComponentInChildren<TMP_Text>().text = soCar.dataTable[i, 5].ToString();
            }

        }
        initialPos = 0;
    }

    public void addSegment()
    {
        if (soCar.numberSegments < 3)
        {
            soCar.numberSegments += 1;

            drawSegment();

        }
    }

    public void removeSegment()
    {
        if (soCar.numberSegments > 1)
        {
            soCar.numberSegments -= 1;

            for (int i = 0; i < soCar.dataTable.GetLength(1); i++)
            {
                soCar.dataTable[soCar.numberSegments, i] = 0;
            }

            drawSegment();

        }
    }

    private void SetPositionSegment(GameObject segment, float size)
    {
        segment.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        segment.GetComponent<RectTransform>().anchoredPosition = new Vector2(initialPos, 0);
        initialPos += size;
    }

}
