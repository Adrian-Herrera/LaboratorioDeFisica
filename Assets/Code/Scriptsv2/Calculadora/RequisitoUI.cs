using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequisitoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _indice;
    [SerializeField] private VariableInput _variablePrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _answerButton;
    private List<VariableInput> _inputs = new();
    private RequisitoStruct _requisito;
    private void Start()
    {
        _answerButton.onClick.AddListener(Resolve);
    }
    public void InstanceInputs(RequisitoStruct requisito)
    {
        _requisito = requisito;
        Helpers.ClearListContent(_inputs);
        foreach (TipoVariable variable in requisito.Requisitos)
        {
            VariableInput VI = Instantiate(_variablePrefab, _container);
            VI.gameObject.SetActive(true);
            VI.Init(new VariableUnity(variable));
            _inputs.Add(VI);
        }
    }
    private void Resolve()
    {
        float a = _requisito.myDelegate(_inputs);
        Debug.Log(a);
    }

}
