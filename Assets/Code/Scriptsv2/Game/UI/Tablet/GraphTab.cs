using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphTab : MonoBehaviour, ITab
{
    [SerializeField] private Window_Graph _graphs;
    [SerializeField] private Button _buttonTemplate;
    [SerializeField] private Transform _buttonContainer;
    [SerializeField] private Tablet _tablet;
    [SerializeField] private List<Button> _buttons = new();
    private Dictionary<TipoVariable, List<float>> _values;
    public void Init()
    {
        Debug.Log("GraphTab Init");
        if (_tablet.ActiveStation != null)
        {
            CreateValueList(_tablet.ActiveStation.CinematicObject.TimeMoving, 10);
            InstanceUI();
        }
    }
    private void InstanceUI()
    {
        Helpers.ClearListContent(_buttons);
        foreach (var item in _values)
        {
            Button newButton = Instantiate(_buttonTemplate, _buttonContainer);
            newButton.gameObject.SetActive(true);
            newButton.GetComponentInChildren<TMP_Text>().text = item.Key.Nombre;
            _buttons.Add(newButton);
            newButton.onClick.AddListener(() => { _graphs.Init(item.Value, _tablet.ActiveStation.CinematicObject.TimeMoving); });
            LayoutRebuilder.ForceRebuildLayoutImmediate(newButton.GetComponent<RectTransform>());
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_buttonContainer.GetComponent<RectTransform>());
    }
    private void CreateValueList(float totalTime, float numberOfValues)
    {
        Debug.Log("Create Value List");
        // float totalTime = 3;
        // float numberOfValues = 10;
        _values = new();
        CinematicObject _cinematic = _tablet.ActiveStation.CinematicObject;

        switch (_cinematic.Type)
        {
            case CinematicType.MRU:
                List<float> MRUvaluesVel = new();
                List<float> MRUvaluesDist = new();
                for (int i = 0; i < numberOfValues; i++)
                {
                    float time = Mathf.Lerp(0, totalTime, i / numberOfValues);
                    float vel = _cinematic.VelX;
                    float dist = Formulary2.Formula_mru_x(v: vel, t: time);
                    MRUvaluesVel.Add(vel);
                    MRUvaluesDist.Add(dist);
                }
                _values.Add(BaseVariable.Velocidad, MRUvaluesVel);
                _values.Add(BaseVariable.Distancia, MRUvaluesDist);
                break;
            case CinematicType.MRUV:
                List<float> valuesVel = new();
                List<float> valuesDist = new();
                for (int i = 0; i < numberOfValues; i++)
                {
                    float time = Mathf.Lerp(0, totalTime, i / numberOfValues);
                    float vel = Formulary2.Formula_1(vo: _cinematic.VelX, a: _cinematic.AccX, t: time);
                    float dist = Formulary2.Formula_2(vo: _cinematic.VelX, vf: vel, t: time);
                    valuesVel.Add(vel);
                    valuesDist.Add(dist);
                }
                _values.Add(BaseVariable.VelocidadFinal, valuesVel);
                _values.Add(BaseVariable.Distancia, valuesDist);
                break;
            case CinematicType.Parabolico:
                List<float> valuesVelX = new();
                List<float> valuesVelY = new();
                List<float> valuesDistX = new();
                List<float> valuesHeight = new();
                for (int i = 0; i <= numberOfValues; i++)
                {
                    float time = Mathf.Lerp(0, totalTime, i / numberOfValues);
                    float velY = Formulary2.Formula_1(vo: _cinematic.VelY, a: -_cinematic.AccY, t: time);
                    float velX = _cinematic.VelX;
                    float dist = Formulary2.Formula_mru_x(v: _cinematic.VelX, t: time);
                    float altura = Formulary2.Altura(x: _cinematic.VelX, y: _cinematic.VelY, dist: dist, grav: _cinematic.AccY);
                    valuesVelX.Add(velX);
                    valuesVelY.Add(Mathf.Abs(velY));
                    valuesDistX.Add(dist);
                    valuesHeight.Add(altura);
                }
                _values.Add(BaseVariable.Velocidad, valuesVelX);
                _values.Add(BaseVariable.VelocidadFinal, valuesVelY);
                _values.Add(BaseVariable.Distancia, valuesDistX);
                _values.Add(BaseVariable.Altura, valuesHeight);
                break;
            default:
                break;
        }

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
