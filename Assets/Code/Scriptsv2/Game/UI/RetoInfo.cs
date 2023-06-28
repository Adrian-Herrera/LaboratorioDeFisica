using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RetoInfo : View
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
    // public void Init(RetoDato[] retoDatos, int retoIntentos)
    // {
    //     // Debug.Log("Creando con los datos: " + retoIndex);
    //     Helpers.ClearListContent(_inputList);
    //     Helpers.ClearListContent(_dataList);
    //     foreach (RetoDato retoDato in retoDatos)
    //     {
    //         if (retoDato.EsDato)
    //         {
    //             VariableInput input = Instantiate(_inputPrefab, _infoDataContainer.transform);
    //             input.Init(retoDato.Variable, retoDato.Valor, true);
    //             _dataList.Add(input);
    //         }
    //         else
    //         {
    //             VariableInput input = Instantiate(_inputPrefab, _userDataContainer.transform);
    //             input.Init(retoDato.Variable, retoDato.Valor, true);
    //             _inputList.Add(input);
    //         }
    //     }

    //     _title.text = "RETO " + retoIndex;
    //     _intentos.text = "Intentos: " + retoIntentos;
    // }
    public void Init(Cuestionario reto, int intentos)
    {
        Helpers.ClearListContent(_inputList);
        Helpers.ClearListContent(_dataList);
        foreach (Dato retoDato in reto.Preguntas[0].Variables)
        {
            if (retoDato.TipoDatoId == 1)
            {
                VariableInput input = Instantiate(_inputPrefab, _infoDataContainer.transform);
                input.Init(retoDato.TipoVariable, retoDato.Valor, true);
                _dataList.Add(input);
            }
            else
            {
                VariableInput input = Instantiate(_inputPrefab, _userDataContainer.transform);
                input.Init(retoDato.TipoVariable, retoDato.Valor, true);
                _inputList.Add(input);
            }
        }

        _title.text = reto.Titulo;
        _intentos.text = "Intentos: " + intentos;
    }

}
