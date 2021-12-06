using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SegmentField : MonoBehaviour
{
    private Field[] childFields;
    public ProblemsSO ActiveProblem;

    public int SegmentID;
    private Color newCol;
    private void Awake()
    {
        childFields = GetComponentsInChildren<Field>();
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
    }

    public void assignChildIDs()
    {
        for (int i = 0; i < childFields.Length; i++)
        {
            childFields[i].FieldSegment = SegmentID;
            childFields[i].FieldID = i;
        }
    }

    public int NumberEmptyFields(int incognita)
    {
        int NumberOfEmptyFields = 0;
        foreach (Field field in childFields)
        {
            if (field.FieldID != incognita && field.isEmpty())
            {
                NumberOfEmptyFields++;
            }
        }
        return NumberOfEmptyFields;
    }

    public string EmptyField(int incognita)
    {
        int _emptyField = 0;
        foreach (Field field in childFields)
        {
            if (field.FieldID != incognita && field.isEmpty())
            {
                _emptyField = field.FieldID;
            }
        }
        switch (_emptyField)
        {
            case 0:
                return "Vo";
            case 1:
                return "Vf";
            case 2:
                return "a";
            case 3:
                return "x";
            case 4:
                return "t";

            default:
                return "";
        }
    }

    public void ChangeColor(int Variable, Color color)
    {
        Clear();
        foreach (Field field in childFields)
        {
            if (field.FieldID != Variable)
            {
                field.ChangeColor(color);
            }
        }
    }

    public void Clear()
    {
        foreach (Field field in childFields)
        {
            field.ChangeColor(Color.white);
        }
    }

    public bool onNewProblem(ProblemsSO problem)
    {
        ActiveProblem = problem;
        int incognita = (int)ActiveProblem.Incognita;
        ChangeColor(incognita, newCol);
        if (NumberEmptyFields(incognita) < 2)
        {
            ActiveProblem.Calculate(EmptyField(incognita));
            return true;
        }
        else
        {
            Debug.Log("Le faltan datos");
            return false;
        }
    }

}
