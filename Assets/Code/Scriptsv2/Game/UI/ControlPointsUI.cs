using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPointsUI : MonoBehaviour
{
    [Header("Control Point")]
    [SerializeField] private ControlPoints _controlPoints;
    [SerializeField] private ControlPointInput _controlPointInput;
    [SerializeField] private GameObject _controlPointsContainer;
    // [SerializeField] private List<VariableInput> _listControlPoint = new();
    [SerializeField] private List<ControlPointInput> _listControlPoint = new();
    // private List<Button> _listDeleteButton = new();
    [Header("New Point")]
    [SerializeField] private Button _addPointButton;
    [SerializeField] private TMP_InputField _addPointField;
    private void Start()
    {
        _addPointButton.onClick.AddListener(AddPoint);
    }

    public void Init(ControlPoints controlPoints)
    {
        _controlPoints = controlPoints;
        InstanceInputs();
    }
    private void InstanceInputs()
    {
        Helpers.ClearListContent(_listControlPoint);
        foreach (VariableUnity distance in _controlPoints.DistancePoints)
        {
            // _listControlPoint.Add(Instantiate(_inputPrefab, _controlPointsContainer.transform).Init(distance, true));
            ControlPointInput tempInput = Instantiate(_controlPointInput, _controlPointsContainer.transform);
            tempInput.Init(distance, true);
            _listControlPoint.Add(tempInput);
            tempInput._deleteButton.onClick.AddListener(() => { DeletePoint(tempInput._inputPrefab); });
            // Button deleteBtn = Instantiate(_deleteButton, )
        }
    }
    private void AddPoint()
    {
        bool isAdded = _controlPoints.AddControlPointAt(float.Parse(_addPointField.text));
        if (isAdded)
        {
            InstanceInputs();
        }
    }
    private void DeletePoint(VariableInput input)
    {
        Debug.Log("delete at: " + input.Value);
        bool isDeleted = _controlPoints.DeleteControlPointAt(input.Value);
        if (isDeleted)
        {
            ControlPointInput pointInput = _listControlPoint.Find(e => e._inputPrefab == input);
            _listControlPoint.Remove(pointInput);
            Destroy(pointInput.gameObject);
        }
    }
}
