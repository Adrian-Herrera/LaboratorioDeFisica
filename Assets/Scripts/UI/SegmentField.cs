using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SegmentField : MonoBehaviour
{
    [SerializeField] private Sprite NormalFieldSprite, ErrorFieldSprite;
    private Color newCol;
    private MessageBox Message; // por quitar
    private AlertMessages alertMessages = new AlertMessages(); // por quitar
    // public CarSO carSO;
    public Field[] childFields;
    public problem ActiveProblem;
    public int SegmentID;
    public bool _error;
    public bool error
    {
        get { return _error; }
        set
        {
            if (value == true)
            {
                GetComponent<Image>().sprite = ErrorFieldSprite;
            }
            else
            {
                GetComponent<Image>().sprite = NormalFieldSprite;

            }
            _error = value;
        }
    }
    private void Awake()
    {
        childFields = GetComponentsInChildren<Field>();
    }
    private void Start()
    {
        for (int i = 0; i < childFields.Length; i++)
        {
            childFields[i].inputField.onSelect.AddListener(delegate { setSegment(); });
        }
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
        Message = GetComponent<MessageBox>();
    }

    private void setSegment()
    {
        ExerciseManager.current.SelectedSegment = this;
    }
    public bool hasEnoughData()
    {
        int validFields = 0;
        // int incognita = (int)ActiveProblem.Incognita;
        // foreach (Field field in childFields)
        // {
        //     if ((field.column != incognita) && !field.status)
        //     {
        //         emptyFields++;
        //     }
        // }
        if (ActiveProblem.Requisitos.Length == 0) return true;
        foreach (int id in ActiveProblem.Requisitos)
        {
            if (childFields[id].status) validFields++;
        }
        return validFields == ActiveProblem.Necesarios ? true : false;
    }

    public int EmptyField()
    {
        int _emptyField = 0;
        int incognita = ActiveProblem.Incognita;
        // foreach (Field field in childFields)
        // {
        //     if (field.column != incognita && !field.status)
        //     {
        //         _emptyField = field.column;
        //     }
        // }
        if (ActiveProblem.Requisitos.Length == 0) return incognita;
        foreach (int id in ActiveProblem.Requisitos)
        {
            if (!childFields[id].status) _emptyField = id;
        }
        // Debug.Log($"_emptyField: {_emptyField}");
        return _emptyField;
        // switch (_emptyField)
        // {
        //     case 0:
        //         return Formulary.option.Vo;
        //     case 1:
        //         return Formulary.option.Vf;
        //     case 2:
        //         return Formulary.option.a;
        //     case 3:
        //         return Formulary.option.x;
        //     default:
        //         return Formulary.option.t;
        // }
    }
    public bool CalculateProblem(problem problem)
    {
        ActiveProblem = problem;
        int incognita = (int)ActiveProblem.Incognita;
        ExerciseManager.current.PreFormula();
        if (hasEnoughData())
        {
            ResetColor();
            error = false;
            // ActiveProblem.Calculate(EmptyField());
            ExerciseManager.current.searchFormula(EmptyField());
            return true;
        }
        else
        {
            ChangeColor(incognita, newCol);
            if (ActiveProblem.Necesarios == 3) Message.Show(alertMessages.fieldAlerts.ThreeInputRequired);
            Debug.Log("Le faltan datos");
            return false;
        }
    }

    // color methods
    public void ChangeColor(int Variable, Color color)
    {
        ResetColor();
        foreach (int id in ActiveProblem.Requisitos)
        {
            childFields[id].ChangeColor(color);
        }
        // foreach (Field field in childFields)
        // {
        //     if (field.column != Variable)
        //     {
        //         field.ChangeColor(color);
        //     }
        // }
    }
    public void ResetColor()
    {
        foreach (Field field in childFields)
        {
            field.ChangeColor(Color.white);
        }
    }
    public void setInteractable(int id, bool value)
    {
        childFields[id].SetInteractable(value);
    }
    public void setInteractableAll(bool value)
    {
        foreach (Field field in childFields)
        {
            field.SetInteractable(value);
        }
    }
}
