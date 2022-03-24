using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Steps : MonoBehaviour
{
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _exponent;
    [SerializeField] private GameObject _fraction;
    [SerializeField] private GameObject _group;
    [SerializeField] private GameObject _sqrt;
    [SerializeField] private GameObject _slash;
    public GameObject Step;
    private List<GameObject> _stepList = new List<GameObject>();
    private GameObject _actualStep;
    private short indice;
    public static Steps current;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        indice = 1;
    }
    public void NewText(string text)
    {
        _actualStep = Instantiate(Step, transform);
        GameObject tempGo = CreateObject($"{indice}." + text);
        float width = GetComponent<RectTransform>().rect.width;
        // Debug.Log($"width: {width}");
        tempGo.GetComponent<TMP_Text>().enableWordWrapping = true;
        tempGo.GetComponent<TMP_Text>().horizontalAlignment = HorizontalAlignmentOptions.Left;
        float NumberLines = (int)(tempGo.GetComponent<RectTransform>().rect.width / width) + 1;
        Debug.Log($"NumberLines: {NumberLines}");
        if (NumberLines >= 1)
        {
            Vector2 newSize = new Vector2(width, tempGo.GetComponent<RectTransform>().rect.height * ((NumberLines * 2) - 1));
            _actualStep.GetComponent<RectTransform>().sizeDelta = newSize;
            tempGo.GetComponent<RectTransform>().sizeDelta = newSize;
        }
        indice++;
    }
    public void NewLine(params object[] list)
    {
        _actualStep = Instantiate(Step, transform);
        GameObject tempGo;
        float width = 0, height = 0;
        foreach (object item in list)
        {
            tempGo = CreateObject(item);
            width += tempGo.GetComponent<RectTransform>().rect.width;
            height = Mathf.Max(tempGo.GetComponent<RectTransform>().rect.height, height);
            tempGo.transform.SetParent(_actualStep.transform);

        }
        _actualStep.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
    public GameObject CreateText(string s)  // text script
    {
        GameObject go = Instantiate(_text, _actualStep.transform);
        go.GetComponent<StepText>().init(s);
        return go;
    }
    public GameObject SupS(object _mainText, object s) // doesn´t have script
    {
        GameObject mainText = CreateObject(_mainText);

        GameObject exponent = CreateObject(s);
        exponent.transform.SetParent(mainText.transform);
        exponent.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        exponent.GetComponent<RectTransform>().SetAnchorTopRigth();
        exponent.GetComponent<RectTransform>().anchoredPosition = new Vector2(exponent.GetComponent<RectTransform>().rect.width / 2 * 0.7f, 0);

        GameObject cont = Instantiate(_group, _actualStep.transform);
        cont.name = "Pow";
        cont.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.LowerLeft;
        mainText.transform.SetParent(cont.transform);

        float newWidth = (exponent.GetComponent<RectTransform>().rect.width * 0.7f) + mainText.GetComponent<RectTransform>().rect.width;
        float newHeight = (exponent.GetComponent<RectTransform>().rect.height / 2 * 0.7f) + mainText.GetComponent<RectTransform>().rect.height;
        cont.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, newHeight);
        return cont;
    }
    public GameObject Frac(object _numerator, object _denominator) // fraction script
    {
        GameObject go = Instantiate(_fraction, _actualStep.transform);
        GameObject numerator = CreateObject(_numerator);
        GameObject denominator = CreateObject(_denominator);
        go.GetComponent<StepFraction>().checkSize(numerator, denominator);
        return go;
    }
    public GameObject Group(params object[] list) // doesn´t have script
    {
        GameObject group = Instantiate(_group, _actualStep.transform);
        GameObject tempGo;
        float width = 0, height = 0;
        foreach (object item in list)
        {
            tempGo = CreateObject(item);
            width += tempGo.GetComponent<RectTransform>().rect.width;
            height = Mathf.Max(tempGo.GetComponent<RectTransform>().rect.height, height);
            tempGo.transform.SetParent(group.transform);

        }
        group.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        return group;
    }

    public GameObject Sqrt(params object[] list) // _sqrt script
    {
        GameObject sqrt = Instantiate(_sqrt, _actualStep.transform);
        GameObject[] objs = new GameObject[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            objs[i] = CreateObject(list[i]);
        }
        sqrt.GetComponent<StepSqrt>().init(objs);
        return sqrt;
    }
    public GameObject Cancel(object obj)
    {
        GameObject tempGo = CreateObject(obj);
        Instantiate(_slash, tempGo.transform);
        return tempGo;
    }

    private GameObject CreateObject(object obj)
    {
        GameObject tempGo;
        // Debug.Log(obj.GetType());
        switch (obj)
        {
            case float f:
                tempGo = CreateText(obj.ToString());
                break;
            case int i:
                tempGo = CreateText(obj.ToString());
                break;
            case string s:
                tempGo = CreateText((string)obj);
                break;
            default:
                tempGo = (GameObject)obj;
                break;
        }
        return tempGo;
    }

}
