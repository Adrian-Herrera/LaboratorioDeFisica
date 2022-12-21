using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MRUV_StationUI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private VariableInput _inputPrefab;
    [SerializeField] private RetoInfo _retoFinalInfo;
    [Header("Car")]
    [SerializeField] private Car3d _car;
    [Header("Containers")]
    [SerializeField] private GameObject _objectiveContainer;
    [SerializeField] private GameObject _inputContainer;
    [SerializeField] private GameObject _retoFinalInfoContainer;
    [SerializeField] private GameObject _retoFinalInfoPanel;
    [Header("Info")]
    [SerializeField] private Button _iniciarBtn;
    [SerializeField] private RetosSO[] _retos;
    [SerializeField] private RetosSO _actualReto;
    [SerializeField] private int[] _intentos;
    [SerializeField] private int _retoIndex = 0;
    // lists
    private List<VariableInput> _inputList = new();
    private List<VariableInput> _dataList = new();
    private void Start()
    {
        _car.OnFinishMove += CheckAnswer;
        _iniciarBtn.onClick.AddListener(StartMoveCar);
        _intentos = new int[_retos.Length];
    }
    public void Init()
    {
        Debug.Log("Init station");
        Helpers.ClearListContent(_inputList);
        Helpers.ClearListContent(_dataList);
        _actualReto = _retos[_retoIndex];
        RetoTemplate[] retos = _actualReto.Init();
        foreach (RetoTemplate reto in retos)
        {
            if (reto.IsData)
            {
                VariableInput input = Instantiate(_inputPrefab, _objectiveContainer.transform);
                input.Init(reto.VariableType, reto.value, true);
                _dataList.Add(input);
            }
            else
            {
                VariableInput input = Instantiate(_inputPrefab, _inputContainer.transform);
                input.Init(reto.VariableType, reto.value);
                _inputList.Add(input);
            }
        }
    }
    public void StartMoveCar()
    {
        _car.StartMovement(int.Parse(_inputList[0]._inputField.text));
        Debug.Log(_inputList.Find(r => r._enum == VariableHelper.VariableEnum.Velocidad));
    }
    public void CheckAnswer()
    {
        _intentos[_retoIndex]++;
        if (_inputList.Find(r => r._enum == VariableHelper.VariableEnum.Velocidad).CheckAnswer())
        {
            Debug.Log("Correcto - Pasa al siguiente");
            if (_retoIndex < _retos.Length - 1)
            {
                _retoIndex++;

            }
            else
            {
                Debug.Log("Todos los retos completados");
                ShowFinalInfo();
            }
        }
        else
        {
            Debug.Log("Incorrecto - Repite ejercicio");
        }
        StartCoroutine(ResetCar());
    }
    public IEnumerator ResetCar()
    {
        yield return new WaitForSeconds(3);
        _car.ResetAll();
        Init();
    }
    public void ShowFinalInfo()
    {
        _retoFinalInfoPanel.SetActive(true);
        // Debug.Log("Borrando childs");
        // foreach (Transform child in _retoFinalInfoContainer.transform)
        // {
        //     Destroy(child.gameObject);
        // }
        Debug.Log("Creando instancias");
        for (int i = 0; i < _retos.Length; i++)
        {
            RetosSO retos = _retos[i];
            RetoInfo info = Instantiate(_retoFinalInfo, _retoFinalInfoContainer.transform);
            info.Init(retos.Init(), i + 1, _intentos[i]);
            info.name = "Reto" + (i + 1);
        }
    }


}
