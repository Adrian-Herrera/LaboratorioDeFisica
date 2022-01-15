using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldsManager : MonoBehaviour
{
    [SerializeField] private CarSO carSO;
    [SerializeField] private ExtraInfo extraInfo;
    private SegmentField[] SegmentFields;
    private Field[] Fields;
    private Color newCol;
    private void Awake()
    {
        SegmentFields = GetComponentsInChildren<SegmentField>();
        Fields = GetComponentsInChildren<Field>();
        // AsignFields();
    }
    // Start is called before the first frame update
    void Start()
    {
        AsignFields();
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
        EventManager.Current.onChangeProblem += CheckIncognitas;
        EventManager.Current.onSelectField += SelectSegment;


        // StartCoroutine(carSO.Datos[0, 0].ShowMessage());
    }
    private void AsignFields()
    {
        int numberField = 0;
        for (int i = 0; i < carSO.Datos.GetLength(0); i++)
        {
            for (int j = 0; j < carSO.Datos.GetLength(1); j++)
            {
                carSO.Datos[i, j] = Fields[numberField];
                Fields[numberField].row = i;
                Fields[numberField].column = j;
                numberField++;
            }
            SegmentFields[i].SegmentID = i;
            SegmentFields[i].carSO = carSO;
        }
    }
    private void CheckIncognitas()
    {
        if (SegmentFields[carSO.selectedSegment].CalculateProblem(HeaderManager.current.ActiveProblem))
        {
            // saveProblems();
        }
        extraInfo.checkTime();
        extraInfo.checkDistance();
    }

    private void SelectSegment(int segment) // called by event
    {
        carSO.selectedSegment = segment;
    }

}
