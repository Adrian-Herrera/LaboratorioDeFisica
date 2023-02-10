﻿using System;
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
        // Debug.Log("FieldManager Awake");
        SegmentFields = BasicInfo.GetComponentsInChildren<SegmentField>();
        Fields = BasicInfo.GetComponentsInChildren<Field>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AsignFields();
        ExerciseManager.current.SelectedSegment = SegmentFields[0];
        EventManager.Current.onChangeProblem += CheckIncognitas;
        // EventManager.Current.onUpdateData += delegate { ExerciseManager.current.CheckEveryTime(BasePointSO); };

        extraInfo.InstanceInputs(BasePointSO);

        BasePointSO.setDefaultValues();
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
        if ((int)HeaderManager.current.ActiveProblem.Incognita <= BasePointSO.Datos.GetLength(1))
        {
            ExerciseManager.current.SelectedSegment.CalculateProblem(HeaderManager.current.ActiveProblem);
        }
        else
        {
            // HeaderManager.current.ActiveProblem.Calculate();
        }
        ExerciseManager.current.CheckEveryTime(BasePointSO);
    }
}