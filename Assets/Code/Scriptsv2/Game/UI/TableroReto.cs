using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TableroReto : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TableroDato _tableroDatoPrefab;
    [SerializeField] private TMP_Text _enunciado;
    private List<TableroDato> _datos = new();
    [SerializeField] private Cuestionario _reto;
    public void SetNewReto(Cuestionario reto)
    {
        _reto = reto;
        Helpers.ClearListContent(_datos);
        for (int i = 0; i < _reto.Preguntas[0].Variables.Length; i++)
        {
            _enunciado.text = _reto.Preguntas[0].Enunciado;
            if (_reto.Preguntas[0].Variables[i].TipoDatoId == 1)
            {
                TableroDato newDato = Instantiate(_tableroDatoPrefab, _container.transform);
                newDato.Init(_reto.Preguntas[0].Variables[i]);
                _datos.Add(newDato);
            }
        }
    }

}
