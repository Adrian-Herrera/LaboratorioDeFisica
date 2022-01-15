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
    private MessageBox Message;
    private AlertMessages alertMessages = new AlertMessages();
    public CarSO carSO;
    public Field[] childFields;
    public ProblemsSO ActiveProblem;
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
        ColorUtility.TryParseHtmlString("#FFCC70", out newCol);
    }
    private void Start()
    {
        Message = GetComponent<MessageBox>();
    }

    public int NumberEmptyFields()
    {
        int NumberOfEmptyFields = 0;
        int incognita = (int)ActiveProblem.Incognita;
        foreach (Field field in childFields)
        {
            if (field.column != incognita && field.isEmpty())
            {
                NumberOfEmptyFields++;
            }
        }
        return NumberOfEmptyFields;
    }

    public string EmptyField()
    {
        int _emptyField = 0;
        int incognita = (int)ActiveProblem.Incognita;
        foreach (Field field in childFields)
        {
            if (field.column != incognita && field.isEmpty())
            {
                _emptyField = field.column;
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

    public bool CalculateProblem(ProblemsSO problem)
    {
        ActiveProblem = problem;
        int incognita = (int)ActiveProblem.Incognita;
        if (NumberEmptyFields() < 2)
        {
            ResetColor();
            error = false;
            ActiveProblem.Calculate(EmptyField());
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
}
