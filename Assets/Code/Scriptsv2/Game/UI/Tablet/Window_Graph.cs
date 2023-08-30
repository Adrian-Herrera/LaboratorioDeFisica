/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Window_Graph : MonoBehaviour
{

    [SerializeField] private Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer;
    [SerializeField] private RectTransform labelTemplateX;
    [SerializeField] private RectTransform labelTemplateY;
    [SerializeField] private RectTransform dashTemplateX;
    [SerializeField] private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;

    private void Awake()
    {
        gameObjectList = new List<GameObject>();
    }

    public void Init(Dictionary<float, float> valueList, Dictionary<float, float> oldValueList, float totalTime)
    {
        // List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17 };
        Helpers.ClearListContent(gameObjectList);
        ShowGraph(new List<Dictionary<float, float>>() { valueList, oldValueList }, -1, totalTime);
    }

    private void ShowGraph(List<Dictionary<float, float>> valueList, int maxVisibleValueAmount = -1, float xTime = 0, Func<float, string> getAxisLabelY = null)
    {
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        if (maxVisibleValueAmount <= 0)
        {
            maxVisibleValueAmount = 10;
        }

        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        float yMaximum = valueList[0][0];
        float yMinimum = valueList[0][0];

        foreach (Dictionary<float, float> list in valueList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                float value = list.ElementAt(i).Value;
                if (value > yMaximum)
                {
                    yMaximum = value;
                }
                if (value < yMinimum)
                {
                    yMinimum = value;
                }
            }
        }

        float yDifference = yMaximum - yMinimum;
        if (yDifference <= 0)
        {
            yDifference = 5f;
        }
        yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

        yMinimum = 0f; // Start the graph at zero

        float xSize = graphWidth / maxVisibleValueAmount;

        int xIndex = 0;

        GameObject lastCircleGameObject = null;
        foreach (Dictionary<float, float> list in valueList)
        {
            if (list.Count > 0)
            {
                lastCircleGameObject = null;
                Color newColor = valueList.IndexOf(list) == 0 ? Color.white : Color.yellow;
                for (int i = 0; i < list.Count; i++)
                {
                    float xPosition = graphWidth * list.ElementAt(i).Key / xTime;
                    // Debug.Log($"{graphWidth}*{list[i]}/{xTime}");
                    float yPosition = graphHeight * list.ElementAt(i).Value / yMaximum;
                    GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), newColor);
                    gameObjectList.Add(circleGameObject);
                    if (lastCircleGameObject != null)
                    {
                        GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, newColor);
                        gameObjectList.Add(dotConnectionGameObject);
                    }
                    lastCircleGameObject = circleGameObject;
                    // Debug.Log(list.ElementAt(i).Key +" : " + xTime);
                    if (list.ContainsKey(xTime))
                    {
                        RectTransform labelX = Instantiate(labelTemplateX);
                        labelX.SetParent(graphContainer, false);
                        labelX.gameObject.SetActive(true);
                        labelX.anchoredPosition = new Vector2(xPosition, -12f);
                        // Debug.Log($"i: {i}, count: {valueList.Count - 1} => res: {(float)i / (valueList.Count - 1)}");
                        float labelValue = Mathf.Round(Mathf.Lerp(0, xTime, (float)i / (list.Count - 1)) * 100) / 100;
                        labelX.GetComponent<TMP_Text>().text = labelValue.ToString();
                        gameObjectList.Add(labelX.gameObject);

                        RectTransform dashX = Instantiate(dashTemplateX);
                        dashX.SetParent(graphContainer, false);
                        dashX.gameObject.SetActive(true);
                        dashX.anchoredPosition = new Vector2(xPosition, -3f);
                        gameObjectList.Add(dashX.gameObject);
                    }

                }
            }
        }

        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<TMP_Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            gameObjectList.Add(labelY.gameObject);

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition, Color color)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        return gameObject;
    }

    float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
