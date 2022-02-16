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
        for (int i = 0; i < childFields.Length; i++)
        {
            childFields[i].inputField.onSelect.AddListener(delegate { setSegment(); });
        }
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
    }
    private void Start()
    {
        Message = GetComponent<MessageBox>();
    }

    private void setSegment()
    {
        ExerciseManager.current.SelectedSegment = this;
    }
    public bool hasEnoughData()
    {
        int emptyFields = 0;
        int incognita = (int)ActiveProblem.Incognita;
        foreach (Field field in childFields)
        {
            if (field.column != incognita && !field.status)
            {
                emptyFields++;
            }
        }
        return emptyFields <= 1 ? true : false;
    }

    public int EmptyField()
    {
        int _emptyField = 0;
        int incognita = (int)ActiveProblem.Incognita;
        foreach (Field field in childFields)
        {
            if (field.column != incognita && !field.status)
            {
                _emptyField = field.column;
            }
        }
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
            Message.Show(alertMessages.fieldAlerts.ThreeInputRequired);
            Debug.Log("Le faltan datos");
            return false;
        }
    }

    // color methods
    public void ChangeColor(int Variable, Color color)
    {
        ResetColor();
        foreach (Field field in childFields)
        {
            if (field.column != Variable)
            {
                field.ChangeColor(color);
            }
        }
    }
    public void ResetColor()
    {
        foreach (Field field in childFields)
        {
            field.ChangeColor(Color.white);
        }
    }
}
