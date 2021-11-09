using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Data_panel : MonoBehaviour
{
    public CarSO CarSO;
    public GameObject[] SegmentsButtons;
    public GameObject[] SegmentsFields;
    public List<Button> IndexButtonsList = new List<Button>();
    public List<Button> HideButtonsList = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        SaveButtons(SegmentsButtons);
        EnableSegment(CarSO.numberSegments);
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
        if (CarSO.numberSegments < 3)
        {
            CarSO.numberSegments++;
            EnableSegment(CarSO.numberSegments);
        }
    }
    private void RemoveSegment()
    {
        if (CarSO.numberSegments > 1)
        {
            CarSO.numberSegments--;
            EnableSegment(CarSO.numberSegments);
        }
    }
    private void HideButton()
    {
        foreach (var item in HideButtonsList)
        {
            item.gameObject.SetActive(false);
        }
        if (CarSO.numberSegments > 1)
        {

            HideButtonsList[CarSO.numberSegments - 1].gameObject.SetActive(true);
        }
    }


}
