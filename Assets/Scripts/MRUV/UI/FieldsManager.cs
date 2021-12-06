using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldsManager : MonoBehaviour
{
    public CarSO carSO;
    private SegmentField[] SegmentFields;
    private Color newCol;
    private void Awake()
    {
        SegmentFields = GetComponentsInChildren<SegmentField>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
        EventManager.Current.onChangeProblem += CheckIncognitas;
        EventManager.Current.onChangeFieldData += SaveDataOnSO;
        EventManager.Current.onSelectField += SelectSegment;
        AsignFieldsID();
    }

    private void SaveDataOnSO(int i, int j, string s)
    {
        if (s != "")
        {

            carSO.Datos[i, j] = float.Parse(s);
        }
        else
        {
            carSO.Datos[i, j] = 0;
        }
    }

    private void AsignFieldsID()
    {
        for (int i = 0; i < SegmentFields.Length; i++)
        {
            SegmentFields[i].SegmentID = i;
            SegmentFields[i].assignChildIDs();
        }
    }
    private void CheckIncognitas()
    {
        if (SegmentFields[carSO.selectedSegment].onNewProblem(HeaderManager.current.ActiveProblem))
        {
            ShowDataOnUI();
        }
    }

    public void ShowDataOnUI()
    {
        for (int i = 0; i < SegmentFields.Length; i++)
        {
            for (int j = 0; j < SegmentFields[i].transform.childCount; j++)
            {
                SegmentFields[i].transform.GetChild(j).GetComponent<TMP_InputField>().text = carSO.Datos[i, j].ToString();
            }
        }
    }

    private void SelectSegment(int segment)
    {
        carSO.selectedSegment = segment;
        // Debug.Log(segment);
    }
}
