using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldsManager : MonoBehaviour
{
    public CarSO carSO;
    private Field[] fields;
    public List<GameObject> SegmentsFields = new List<GameObject>();

    private Color newCol;
    private bool isAnyFieldEmpty;
    private void Awake()
    {
        fields = GetComponentsInChildren<Field>();
        for (int i = 0; i < transform.childCount; i++)
        {
            SegmentsFields.Add(transform.GetChild(i).gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
        EventManager.current.onSelectType += CheckIncognitas;
        AsignFieldsID();
    }

    private void AsignFieldsID()
    {
        foreach (var item in SegmentsFields)
        {
            for (int i = 0; i < item.transform.childCount; i++)
            {
                item.transform.GetChild(i).GetComponent<Field>().SaveData(i, SegmentsFields.FindIndex(e => e == item), this);
            }
        }

    }
    private void CheckIncognitas()
    {
        Clear();
        foreach (var item in HeaderManager.current.ActiveProblem.Incognitas)
        {
            for (int i = 0; i < carSO.numberSegments; i++)
            {
                Field comp = SegmentsFields[i].transform.GetChild((int)item).GetComponent<Field>();
                comp.ChangeColor(newCol);
                if (comp.CheckIfEmpty())
                {
                    isAnyFieldEmpty = true;
                }
            }
        }
        if (isAnyFieldEmpty)
        {
            Debug.Log("Faltan Datos");
        }
    }
    private void Clear()
    {
        isAnyFieldEmpty = false;
        foreach (var item in SegmentsFields)
        {
            for (int i = 0; i < item.transform.childCount; i++)
            {
                item.transform.GetChild(i).GetComponent<Field>().ChangeColor(Color.white);
            }
        }
    }

    public void ChangeData(int segment, int id, float newValue)
    {
        carSO.Datos[segment, id] = newValue;
    }

}
