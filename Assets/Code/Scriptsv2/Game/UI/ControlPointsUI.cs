using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPointsUI : MonoBehaviour
{
    [Header("Control Point")]
    [SerializeField] private ControlPointInput _controlPointInput;
    [SerializeField] private GameObject _controlPointsContainer;
    [SerializeField] private Tablet _tablet;
    // [SerializeField] private List<VariableInput> _listControlPoint = new();
    [SerializeField] private List<ControlPointInput> _listControlPoint = new();
    [SerializeField] private FinalData _finalData;
    // private List<Button> _listDeleteButton = new();
    [Header("New Point")]
    [SerializeField] private Button _addPointButton;
    [SerializeField] private TMP_InputField _addPointField;
    private void Start()
    {
        _addPointButton.onClick.AddListener(AddPoint);
    }
    public void Init()
    {
        ControlPoints.Instance.SetCinematicObject(_tablet.ActiveStation.CinematicObject);
        InstanceInputs();
        // _tablet.ActiveStation.CinematicObject.OnFinishMove += CalculateExtraInfo;
    }
    private void InstanceInputs()
    {
        Helpers.ClearListContent(_listControlPoint);
        foreach (VariableUnity distance in ControlPoints.Instance.DistancePoints)
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
        bool isAdded = ControlPoints.Instance.AddControlPointAt(float.Parse(_addPointField.text));
        if (isAdded)
        {
            InstanceInputs();
        }
    }
    private void DeletePoint(VariableInput input)
    {
        Debug.Log("delete at: " + input.Value);
        bool isDeleted = ControlPoints.Instance.DeleteControlPointAt(input.Value);
        if (isDeleted)
        {
            ControlPointInput pointInput = _listControlPoint.Find(e => e._inputPrefab == input);
            _listControlPoint.Remove(pointInput);
            Destroy(pointInput.gameObject);
        }
    }
    // private void CalculateExtraInfo()
    // {
    //     Debug.Log("Calculate Extra Info");
    //     _finalData.Init();
    // }
}
