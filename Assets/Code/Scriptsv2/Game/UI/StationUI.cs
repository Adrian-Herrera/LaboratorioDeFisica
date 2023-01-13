using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StationUI : View
{
    [Header("Prefabs")]
    [SerializeField] private VariableInput _inputPrefab;
    // [SerializeField] private RetoInfo _retoFinalInfo;
    [Header("Car")]
    [SerializeField] protected Car3d _car;
    [Header("Containers")]
    [SerializeField] private GameObject _objectiveContainer;
    [SerializeField] private GameObject _inputContainer;
    // [SerializeField] private GameObject _retoFinalInfoContainer;
    // [SerializeField] private GameObject _retoFinalInfoPanel;
    [Header("Info")]
    [SerializeField] private Button _iniciarBtn;
    // [SerializeField] private Reto[] _retos;
    [SerializeField] private Reto _actualReto;
    [SerializeField] private int _intentos;
    [SerializeField] private int _retoIndex = 0;
    // lists
    protected List<VariableInput> _inputList = new();
    private List<VariableInput> _dataList = new();
    private void Start()
    {
        _car.OnFinishMove += CheckAnswer;
        _iniciarBtn.onClick.AddListener(StartMoveCar);
    }
    public void Init(Reto reto)
    {
        Debug.Log("Init station");
        gameObject.SetActive(true);
        _actualReto = reto;
        _intentos = 0;
        DrawInputs();
    }
    public override void Hide()
    {
        Helpers.ClearListContent(_inputList);
        Helpers.ClearListContent(_dataList);
        base.Hide();
    }
    private void DrawInputs()
    {
        Helpers.ClearListContent(_inputList);
        Helpers.ClearListContent(_dataList);
        // _actualReto = _retos[_retoIndex];
        // RetoTemplate[] retos = _actualReto.Init();
        foreach (RetoDato retoDato in _actualReto.RetoDatos)
        {
            if (retoDato.EsDato)
            {
                VariableInput input = Instantiate(_inputPrefab, _objectiveContainer.transform);
                input.Init(retoDato.Variable, retoDato.Valor, true);
                _dataList.Add(input);
            }
            else
            {
                VariableInput input = Instantiate(_inputPrefab, _inputContainer.transform);
                input.Init(retoDato.Variable, retoDato.Valor);
                _inputList.Add(input);
            }
        }
    }
    public void StartMoveCar()
    {
        Debug.Log("Move with " + _car._type + " type");
        switch (_car._type)
        {
            case CinematicType.MRU:
                _car.SetHorizontalVel(GetVariableValue("v"), 0);
                _car.StartMovement();
                break;
            case CinematicType.MRUV:
                _car.SetHorizontalVel(GetVariableValue("Vo"), GetVariableValue("a"));
                _car.StartMovement();
                break;
            case CinematicType.CaidaLibre:
                break;
            case CinematicType.Parabolico:
                break;
            default:
                Debug.Log("No existe este tipo de cinematica");
                break;
        }
    }
    private float GetVariableValue(string abrev)
    {
        VariableInput vi = _inputList.Find(r =>
        {
            Debug.Log(r.Variable.Abrev + " = " + abrev);
            return r.Variable.Abrev == abrev;
        });
        Debug.Log(vi);
        if (vi != null)
        {
            return vi.Value;
        }
        else
        {
            return 0;
        }
    }
    public void CheckAnswer()
    {
        _intentos++;
        if (_inputList.TrueForAll(r => r.CheckAnswer()))
        {
            // Debug.Log("Correcto - Pasa al siguiente");
            // if (_retoIndex < _retos.Length - 1)
            // {
            //     _retoIndex++;
            // }
            // else
            // {
            // }
            Debug.Log("Todos los retos completados");
            ShowFinalInfo();
        }
        else
        {
            Debug.Log("Incorrecto - Repite ejercicio");
            StartCoroutine(ResetCar());
        }
    }
    public IEnumerator ResetCar()
    {
        yield return new WaitForSeconds(3);
        _car.ResetAll();
        DrawInputs();
    }
    public void ShowFinalInfo()
    {
        PlayerUI.Instance._actualView.Hide();
        PlayerUI.Instance._actualView = PlayerUI.Instance._retoFinal;
        PlayerUI.Instance._actualView.Show();
        PlayerUI.Instance._retoFinal.Init(_actualReto, _intentos);
        // _retoFinalInfoPanel.SetActive(true);
        // Debug.Log("Borrando childs");
        // foreach (Transform child in _retoFinalInfoContainer.transform)
        // {
        //     Destroy(child.gameObject);
        // }

        // for (int i = 0; i < _retos.Length; i++)
        // {
        //     Reto reto = _retos[i];
        //     RetoInfo info = Instantiate(_retoFinalInfo, _retoFinalInfoContainer.transform);
        //     info.Init(reto.RetoDatos, i + 1, _intentos[i]);
        //     info.name = "Reto" + (i + 1);
        // }
        // RetoInfo info = Instantiate(_retoFinalInfo, _retoFinalInfoContainer.transform);
        // info.Init(_actualReto, _intentos);
        // info.name = "Reto";
    }
}
