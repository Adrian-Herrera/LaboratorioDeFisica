using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Data_panel : MonoBehaviour
{
    public BasePointSO BasePointSO;
    public GameObject[] SegmentsButtons;
    public GameObject[] SegmentsFields;
    public List<Button> IndexButtonsList = new List<Button>();
    public List<Button> HideButtonsList = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        SaveButtons(SegmentsButtons);
        EnableSegment(BasePointSO.numberOfSegments);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnableSegment(int index)
    {
        for (int i = 0; i < SegmentsFields.Length; i++)
        {
            if (i < index)
            {
                SegmentsFields[i].SetActive(true);
                SegmentsButtons[i].SetActive(true);
                IndexButtonsList[i].GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
            }
            else
            {
                SegmentsFields[i].SetActive(false);
                SegmentsButtons[i].SetActive(false);
            }
        }
        if (index < 3)
        {
            SegmentsButtons[index].SetActive(true);
            IndexButtonsList[index].GetComponentInChildren<TMP_Text>().text = "+";
        }
        HideButton();
    }
    private void SaveButtons(GameObject[] fields)
    {
        IndexButtonsList.Clear();
        HideButtonsList.Clear();
        foreach (var segment in fields)
        {
            // Debug.Log(segment.transform.GetChild(0).gameObject.name);
            IndexButtonsList.Add(segment.transform.GetChild(0).gameObject.GetComponent<Button>());
            HideButtonsList.Add(segment.transform.GetChild(1).gameObject.GetComponent<Button>());
        }
        AddListenersToButton();
    }
    private void AddListenersToButton()
    {
        foreach (var _button in IndexButtonsList)
        {
            _button.onClick.AddListener(AddSegment);
        }
        foreach (var _button in HideButtonsList)
        {
            _button.onClick.AddListener(RemoveSegment);
        }
    }
    private void AddSegment()
    {
        if (BasePointSO.numberOfSegments < 3)
        {
            BasePointSO.numberOfSegments++;
            EnableSegment(BasePointSO.numberOfSegments);
        }
    }
    private void RemoveSegment()
    {
        if (BasePointSO.numberOfSegments > 1)
        {
            BasePointSO.numberOfSegments--;
            EnableSegment(BasePointSO.numberOfSegments);
        }
    }
    private void HideButton()
    {
        foreach (var item in HideButtonsList)
        {
            item.gameObject.SetActive(false);
        }
        if (BasePointSO.numberOfSegments > 1)
        {

            HideButtonsList[BasePointSO.numberOfSegments - 1].gameObject.SetActive(true);
        }
    }


}
