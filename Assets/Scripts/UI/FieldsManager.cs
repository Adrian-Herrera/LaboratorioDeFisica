using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldsManager : MonoBehaviour
{
    [SerializeField] private BasePointSO BasePointSO;
    [SerializeField] private ExtraInfo extraInfo;
    [SerializeField] private GameObject BasicInfo;
    private SegmentField[] SegmentFields;
    private Field[] Fields;
    // private Color newCol;
    private void Awake()
    {
        SegmentFields = BasicInfo.GetComponentsInChildren<SegmentField>();
        Fields = BasicInfo.GetComponentsInChildren<Field>();

    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Current.onChangeProblem += CheckIncognitas;
        AsignFields();
        extraInfo.InstanceInputs(BasePointSO.getNames(), BasePointSO);
    }
    private void AsignFields()
    {
        int numberField = 0;
        for (int i = 0; i < BasePointSO.Datos.GetLength(0); i++)
        {
            for (int j = 0; j < BasePointSO.Datos.GetLength(1); j++)
            {
                BasePointSO.Datos[i, j] = Fields[numberField];
                Fields[numberField].row = i;
                Fields[numberField].column = j;
                numberField++;
            }
            SegmentFields[i].SegmentID = i;
            // SegmentFields[i].carSO = carSO;
        }
    }
    private void CheckIncognitas()
    {
        if ((int)HeaderManager.current.ActiveProblem.Incognita <= 4)
        {
            ExerciseManager.current.SelectedSegment.CalculateProblem(HeaderManager.current.ActiveProblem);
        }
        else
        {
            // HeaderManager.current.ActiveProblem.Calculate();
        }
        ExerciseManager.current.CheckEveryTime(BasePointSO);
        // extraInfo.checkDistance();
    }
}
