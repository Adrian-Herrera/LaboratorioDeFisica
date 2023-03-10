using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RetoManager : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] private TableroReto _tableroReto;
    private Reto _activeReto;
    // private CinematicObject _cinematicObject;
    public void Init(Reto newReto)
    {
        _activeReto = newReto;
        _tableroReto.SetNewReto(_activeReto);
    }
    public void CheckAnswer()
    {
        // if (_station.ActualMode != Station.ModeEnum.Reto) return;
        bool allAnswered = _activeReto.RetoDatos.All(e =>
        {
            float value = _station.Template.FindVarById(e.Variable.Id).Value;
            return value == e.Valor;
        });
        Debug.Log($"Reto Manager => CheckAnswer: {allAnswered}");
        if (!allAnswered)
        {
            _station.CinematicObject.ResetAll(3);
        }
        // return allAnswered;
    }

}
