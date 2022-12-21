using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RetoInfo : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private VariableInput _inputPrefab;
    [Header("Containers")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _intentos;
    [SerializeField] private GameObject _infoDataContainer;
    [SerializeField] private GameObject _userDataContainer;
    [Header("Info")]
    [SerializeField] private int _retoNumber;
    [SerializeField] private int _retoIntentos;
    private List<VariableInput> _inputList = new();
    private List<VariableInput> _dataList = new();
    public void Init(RetoTemplate[] retos, int retoIndex, int retoIntentos)
    {
        Debug.Log("Creando con los datos: " + retoIndex);
        Helpers.ClearListContent(_inputList);
        Helpers.ClearListContent(_dataList);
        foreach (RetoTemplate reto in retos)
        {
            if (reto.IsData)
            {
                VariableInput input = Instantiate(_inputPrefab, _infoDataContainer.transform);
                input.Init(reto.VariableType, reto.value, true);
                _dataList.Add(input);
            }
            else
            {
                VariableInput input = Instantiate(_inputPrefab, _userDataContainer.transform);
                input.Init(reto.VariableType, reto.value, true);
                _inputList.Add(input);
            }
        }

        _title.text = "RETO " + retoIndex;
        _intentos.text = "Intentos: " + retoIntentos;
    }

}
