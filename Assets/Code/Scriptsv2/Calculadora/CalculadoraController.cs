using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CalculadoraController : MonoBehaviour
{
    public static CalculadoraController Instance;
    [SerializeField] VariableButton VariablePrefab;
    [SerializeField] RequisitoUI FormulaPrefab;
    [SerializeField] private Transform VariableContainer;
    [SerializeField] private Transform FormulaContainer;
    [SerializeField] private int TemaId;
    [SerializeField] private int VariableId;
    [SerializeField] private TipoVariable VariableSelected;
    private List<VariableButton> ListVariable = new();
    private List<RequisitoUI> ListFormula = new();
    // List Variables
    private TipoVariable[] _mruVariables = { BaseVariable.Velocidad, BaseVariable.Distancia, BaseVariable.Tiempo };
    private TipoVariable[] _mruvVariables = {
        BaseVariable.VelocidadInicial, BaseVariable.VelocidadFinal, BaseVariable.Aceleracion, BaseVariable.Distancia, BaseVariable.Tiempo  };
    private void Awake()
    {
        Instance = this;
    }
    public void SetTemaId(int newTemaId)
    {
        TemaId = newTemaId;
        InstanceVariables(newTemaId);
    }
    public void SetVariableId(int newVariableId)
    {
        VariableId = newVariableId;
        InstanceFormulas(newVariableId);
    }

    private void InstanceVariables(int temaId)
    {
        Helpers.ClearListContent(ListVariable);
        switch (temaId)
        {
            case 1:
                CreateVariable(_mruVariables);
                break;
            case 2:
                CreateVariable(_mruvVariables);
                break;
            default:
                break;
        }
    }
    private void CreateVariable(TipoVariable[] ids)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            VariableButton VB = Instantiate(VariablePrefab, VariableContainer);
            VB.SetData(ids[i].Nombre, ids[i].Id);
            ListVariable.Add(VB);
        }
    }
    private void InstanceFormulas(int variableId)
    {
        Helpers.ClearListContent(ListFormula);
        foreach (RequisitoStruct Req in RequisitosFormula.FindRequisitos(variableId))
        {
            RequisitoUI req = Instantiate(FormulaPrefab, FormulaContainer);
            req.gameObject.SetActive(true);
            req.InstanceInputs(Req);
            ListFormula.Add(req);
        }
    }
}
