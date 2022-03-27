using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Steps : MonoBehaviour
{
    [SerializeField] private GameObject _content, _step, _text, _fraction, _group, _sqrt, _slash;
    private GameObject _actualStep;
    private short _indice;
    private float _stepsWidth;
    public static Steps Current;
    // General variables
    private RectTransform _rt;
    // Collapse variables
    private bool _isActive;
    private enum HideDirection
    {
        up, down, left, right
    }
    [SerializeField] private HideDirection _hideDirection;
    [SerializeField] private Button _collapseButton;
    private void Awake()
    {
        Current = this;
        _rt = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _indice = 1;
        _stepsWidth = _rt.rect.width - (_content.GetComponent<VerticalLayoutGroup>().padding.left + _content.GetComponent<VerticalLayoutGroup>().padding.right);
        _collapseButton.onClick.AddListener(Collapse);
        _isActive = false;
        // Debug.Log($"_stepsWidth: {_stepsWidth}");
    }
    public void NewText(string text)
    {
        _actualStep = Instantiate(_step, _content.transform);
        GameObject tempGo = CreateObject($"{_indice}." + text);
        float NumberLines = (int)(tempGo.GetComponent<RectTransform>().rect.width / _stepsWidth) + 1;
        Debug.Log($"NumberLines: {NumberLines}");
        tempGo.GetComponent<TMP_Text>().enableWordWrapping = true;
        tempGo.GetComponent<TMP_Text>().horizontalAlignment = HorizontalAlignmentOptions.Left;
        if (NumberLines >= 1)
        {
            Vector2 newSize = new Vector2(_stepsWidth, tempGo.GetComponent<RectTransform>().rect.height * ((NumberLines * 2) - 1));
            _actualStep.GetComponent<RectTransform>().sizeDelta = newSize;
            tempGo.GetComponent<RectTransform>().sizeDelta = newSize;
        }
        _indice++;
    }
    public void NewLine(params object[] list)
    {
        _actualStep = Instantiate(_step, _content.transform);
        GameObject tempGo;
        float width = 0, height = 0;
        foreach (object item in list)
        {
            tempGo = CreateObject(item);
            width += tempGo.GetComponent<RectTransform>().rect.width;
            height = Mathf.Max(tempGo.GetComponent<RectTransform>().rect.height, height);
            tempGo.transform.SetParent(_actualStep.transform);

        }
        float NumberLines = (int)(width / _stepsWidth) + 1;
        Debug.Log($"NumberLines: {NumberLines}");
        _actualStep.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height * NumberLines);
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
    private void Collapse()
    {
        switch (_hideDirection)
        {
            case HideDirection.right:
                _rt.anchoredPosition = _rt.anchoredPosition + new Vector2(_isActive ? _rt.rect.width : -_rt.rect.width, 0);
                break;
            case HideDirection.left:
                _rt.anchoredPosition = _rt.anchoredPosition + new Vector2(_isActive ? -_rt.rect.width : _rt.rect.width, 0);
                break;
            case HideDirection.up:
                _rt.anchoredPosition = _rt.anchoredPosition + new Vector2(0, _isActive ? _rt.rect.height : -_rt.rect.height);
                break;
            case HideDirection.down:
                _rt.anchoredPosition = _rt.anchoredPosition + new Vector2(0, _isActive ? -_rt.rect.height : _rt.rect.height);
                break;
            default:
                break;
        }
        _isActive = !_isActive;
        _collapseButton.GetComponent<RectTransform>().Rotate(0, 0, 180);
    }

}
