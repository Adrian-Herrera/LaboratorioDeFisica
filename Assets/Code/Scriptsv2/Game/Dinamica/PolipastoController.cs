using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PolipastoController : MonoBehaviour
{
    // [SerializeField] private Polea2 _poleaTemplate;
    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _addPoleaButton;
    [SerializeField] private Button _removePoleaButton;
    [Header("Objetos")]
    [SerializeField] private DynamicObject _box;
    [SerializeField] private DynamicObject _balance;
    [SerializeField] private Polea2 _extraPulley;
    [SerializeField] private Rope _ropeTemplate;
    [SerializeField] private Transform _ropeContainer;
    [SerializeField] private List<Polea2> _listActivePulleys = new();
    private int _numberActivePulleys;
    private List<Rope> _listRopes;
    private void Start()
    {
        _listRopes = new();
        _numberActivePulleys = 1;
        ActivatePulleys(_numberActivePulleys);
        _box.Masa = 4;
        _balance.Masa = 1;
        CalculateTension();

        _box.OnChangeMass += CalculateTension;
        _balance.OnChangeMass += CalculateTension;

        _box.OnStopMovement += StopAllMovement;
        _balance.OnStopMovement += StopAllMovement;

        _startButton.onClick.AddListener(() => StartAllMovement());
        _resetButton.onClick.AddListener(() => ResetAllPosition());
        _addPoleaButton.onClick.AddListener(() => AddPulley());
        _removePoleaButton.onClick.AddListener(() => RemovePulley());

    }
    private void ActivatePulleys(int numberActivePulleys)
    {
        for (int i = 0; i < _listActivePulleys.Count; i++)
        {
            _listActivePulleys[i].gameObject.SetActive(i < numberActivePulleys);
        }
        InstanceRopes();
    }
    public void AddPulley()
    {
        if (_numberActivePulleys == 4) return;
        _numberActivePulleys++;
        ActivatePulleys(_numberActivePulleys);
    }
    public void RemovePulley()
    {
        if (_numberActivePulleys == 1) return;
        _numberActivePulleys--;
        ActivatePulleys(_numberActivePulleys);
    }
    // Ropes
    private void CreateRope(Transform start, Transform end)
    {
        Rope rope = Instantiate(_ropeTemplate, _ropeContainer);
        rope.gameObject.SetActive(true);
        rope.SetPoints(start, end);
        _listRopes.Add(rope);
    }
    private void InstanceRopes()
    {
        Helpers.ClearListContent(_listRopes);
        CreateRope(_listActivePulleys[0].RightPoint, _extraPulley.MiddlePoint);
        CreateRope(_extraPulley.MiddlePoint, _balance.transform);
        if (_numberActivePulleys == 1)
        {
            CreateRope(_listActivePulleys[0].LeftPoint, _box.transform);
        }
        else
        {
            int dir = -1; // -1 left, 1 right
            Polea2 lastPulley = null;
            for (int i = 0; i < _numberActivePulleys; i++)
            {
                if (lastPulley == null)
                {
                    lastPulley = _listActivePulleys[i];
                    continue;
                }
                if (dir < 0)
                {
                    CreateRope(_listActivePulleys[i].LeftPoint, lastPulley.LeftPoint);
                }
                else
                {
                    CreateRope(_listActivePulleys[i].RightPoint, lastPulley.RightPoint);
                }
                dir *= -1;
                if (i == _numberActivePulleys - 1)
                {
                    CreateRope(dir < 0 ? _listActivePulleys[i].LeftPoint : _listActivePulleys[i].RightPoint, lastPulley.MiddlePoint);
                }
                lastPulley = _listActivePulleys[i];
            }
            CreateRope(_listActivePulleys[1].MiddlePoint, _box.transform);
        }
    }
    private void CalculateTension()
    {
        // Debug.Log($"num: {(Mathf.Abs(_box.Peso) - (_numberActivePulleys * _balance.Peso))}");
        // Debug.Log($"den: {(_box.Masa + (Mathf.Pow(_numberActivePulleys, 2) * _balance.Masa * (_numberActivePulleys > 1 ? -1 : 1)))}");
        float acc = Helpers.RoundFloat((Mathf.Abs(_box.Peso) - (_numberActivePulleys * _balance.Peso)) / (_box.Masa + (Mathf.Pow(_numberActivePulleys, 2) * _balance.Masa * (_numberActivePulleys > 1 ? -1 : 1))));
        float tension;
        if (acc >= 0)
        {
            tension = _box.Peso + _box.Masa * Mathf.Abs(acc);
        }
        else
        {
            // Debug.Log($"a: {Mathf.Abs(_balance.Peso)}");
            // Debug.Log($"b: {(_balance.Masa * (_numberActivePulleys > 1 ? acc : Mathf.Abs(acc)))}");
            if (_numberActivePulleys > 1)
            {
                tension = (Mathf.Abs(_box.Peso) - (_box.Masa * (_numberActivePulleys > 1 ? acc : Mathf.Abs(acc)))) / _numberActivePulleys;
            }
            else
            {
                tension = (Mathf.Abs(_balance.Peso) - (_balance.Masa * (_numberActivePulleys > 1 ? acc : Mathf.Abs(acc)))) / _numberActivePulleys;
            }

        }
        tension = Helpers.RoundFloat(tension);
        Debug.Log($"acc: {acc}, tension: {tension}");
        VariableUnity tensionFuerzaBox = new(BaseVariable.Tension, tension);
        VariableUnity tensionFuerzaBalance = new(BaseVariable.Tension, tension / _numberActivePulleys);
        _box.AddYForce(tensionFuerzaBox);
        _balance.AddYForce(tensionFuerzaBalance);

        _box.SetAcc(_numberActivePulleys > 1 ? acc : -acc);
        _balance.SetAcc(_numberActivePulleys > 1 ? -acc * _numberActivePulleys : acc);
    }
    public void StartAllMovement()
    {
        _box.StartMovement();
        _balance.StartMovement();
    }
    public void StopAllMovement()
    {
        _box.StopMovement();
        _balance.StopMovement();
    }
    public void ResetAllPosition()
    {
        _box.ResetPosition();
        _balance.ResetPosition();
    }
}
