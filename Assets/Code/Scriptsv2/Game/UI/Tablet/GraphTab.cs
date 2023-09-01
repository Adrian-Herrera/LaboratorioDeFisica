using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GraphTab : MonoBehaviour, ITab
{
    [SerializeField] private Window_Graph _graphs;
    [SerializeField] private Button _buttonTemplate;
    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Tablet _tablet;
    [SerializeField] private List<Button> _buttons = new();
    [SerializeField] private Dictionary<TipoVariable, Dictionary<float, float>> _values = new();
    [SerializeField] private Dictionary<TipoVariable, Dictionary<float, float>> _oldValues = new();
    // time moving object
    private float newTime = 0;
    private float oldTime = 0;
    private void Start()
    {
        Player.Instance.OnExitStation += () =>
        {
            Helpers.ClearListContent(_buttons);
            _values.Clear();
            _oldValues.Clear();
        };
    }
    public void Init()
    {
        Debug.Log("GraphTab Init");
        oldTime = newTime;
        newTime = _tablet.ActiveStation.CinematicObject.TimeMoving;
        if (_tablet.ActiveStation != null)
        {
            if (_values != null)
            {
                _oldValues = _values.ToDictionary(entry => entry.Key,
                                                   entry => entry.Value);
            }
            if (newTime > 0)
            {
                _values = CreateValueList(newTime, 10);
                InstanceUI();
            }
            // Debug.Log(_values.Count);
            // Debug.Log(_oldValues.Count);
        }
    }
    private void InstanceUI()
    {
        Helpers.ClearListContent(_buttons);
        for (int i = 0; i < _values.Count; i++)
        {
            var item = _values.ElementAt(i);
            Button newButton = Instantiate(_buttonTemplate, _buttonContainer);
            newButton.gameObject.SetActive(true);
            newButton.GetComponentInChildren<TMP_Text>().text = item.Key.Nombre;
            _buttons.Add(newButton);
            if (_oldValues.Count > 0)
            {
                Debug.Log(item.Value);
                // foreach (var item2 in _oldValues.ElementAt(i).Value)
                // {
                //     Debug.Log(item2);
                // }
                // Debug.Log(_oldValues.ElementAt(i).Value);
                var item2 = _oldValues.ElementAt(i);
                newButton.onClick.AddListener(() =>
                {
                    _graphs.Init(item.Value,
                 item2.Value,
                  oldTime > newTime ? oldTime : newTime);
                });
            }
            else
            {
                newButton.onClick.AddListener(() => { _graphs.Init(item.Value, new Dictionary<float, float>(), oldTime > newTime ? oldTime : newTime); });
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(newButton.GetComponent<RectTransform>());
        }
        // foreach (var item in _values)
        // {
        //     Button newButton = Instantiate(_buttonTemplate, _buttonContainer);
        //     newButton.gameObject.SetActive(true);
        //     newButton.GetComponentInChildren<TMP_Text>().text = item.Key.Nombre;
        //     _buttons.Add(newButton);
        //     newButton.onClick.AddListener(() => { _graphs.Init(item.Value, oldTime > newTime ? oldTime : newTime); });
        //     LayoutRebuilder.ForceRebuildLayoutImmediate(newButton.GetComponent<RectTransform>());
        // }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_buttonContainer.GetComponent<RectTransform>());
    }
    private Dictionary<TipoVariable, Dictionary<float, float>> CreateValueList(float totalTime, float numberOfValues)
    {
        Debug.Log("Create Value List");
        // float totalTime = 3;
        // float numberOfValues = 10;
        Dictionary<TipoVariable, Dictionary<float, float>> tempValues = new();
        CinematicObject _cinematic = _tablet.ActiveStation.CinematicObject;

        switch (_cinematic.Type)
        {
            case CinematicType.MRU:
                Dictionary<float, float> MRUvaluesVel = new();
                Dictionary<float, float> MRUvaluesDist = new();
                for (int i = 0; i <= numberOfValues; i++)
                {
                    float time = Mathf.Lerp(0, totalTime, i / numberOfValues);
                    float vel = _cinematic.VelX;
                    float dist = Formulary2.Formula_mru_x(v: vel, t: time);
                    MRUvaluesVel.Add(time, vel);
                    MRUvaluesDist.Add(time, dist);
                }
                tempValues.Add(BaseVariable.Velocidad, MRUvaluesVel);
                tempValues.Add(BaseVariable.Distancia, MRUvaluesDist);
                break;
            case CinematicType.MRUV:
                Dictionary<float, float> valuesVel = new();
                Dictionary<float, float> valuesDist = new();
                for (int i = 0; i <= numberOfValues; i++)
                {
                    float time = Mathf.Lerp(0, totalTime, i / numberOfValues);
                    float vel = Formulary2.Formula_1(vo: _cinematic.VelX, a: _cinematic.AccX, t: time);
                    float dist = Formulary2.Formula_2(vo: _cinematic.VelX, vf: vel, t: time);
                    valuesVel.Add(time, vel);
                    valuesDist.Add(time, dist);
                }
                tempValues.Add(BaseVariable.VelocidadFinal, valuesVel);
                tempValues.Add(BaseVariable.Distancia, valuesDist);
                break;
            case CinematicType.Parabolico:
                Dictionary<float, float> valuesVelX = new();
                Dictionary<float, float> valuesVelY = new();
                Dictionary<float, float> valuesDistX = new();
                Dictionary<float, float> valuesHeight = new();
                for (int i = 0; i <= numberOfValues; i++)
                {
                    float time = Mathf.Lerp(0, totalTime, i / numberOfValues);
                    float velY = Formulary2.Formula_1(vo: _cinematic.VelY, a: -_cinematic.AccY, t: time);
                    float velX = _cinematic.VelX;
                    float dist = Formulary2.Formula_mru_x(v: _cinematic.VelX, t: time);
                    float altura = Formulary2.Altura(x: _cinematic.VelX, y: _cinematic.VelY, dist: dist, grav: _cinematic.AccY);
                    valuesVelX.Add(time, velX);
                    valuesVelY.Add(time, Mathf.Abs(velY));
                    valuesDistX.Add(time, dist);
                    valuesHeight.Add(time, altura);
                }
                tempValues.Add(BaseVariable.Velocidad, valuesVelX);
                tempValues.Add(BaseVariable.VelocidadFinal, valuesVelY);
                tempValues.Add(BaseVariable.Distancia, valuesDistX);
                tempValues.Add(BaseVariable.Altura, valuesHeight);
                break;
            default:
                break;
        }
        return tempValues;
    }
    private List<float> CreateRandomList(int size)
    {
        List<float> newList = new();
        for (int i = 0; i < size; i++)
        {
            newList.Add(Random.Range(0, 100));
        }
        return newList;
    }

}
