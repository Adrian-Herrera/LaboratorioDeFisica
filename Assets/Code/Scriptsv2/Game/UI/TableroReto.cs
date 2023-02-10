using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TableroReto : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TableroDato _tableroDatoPrefab;
    private List<TableroDato> _datos = new();
    [SerializeField] private Reto _reto;
    public void SetNewReto(Reto reto)
    {
        _reto = reto;
        Helpers.ClearListContent(_datos);
        for (int i = 0; i < _reto.RetoDatos.Length; i++)
        {
            if (_reto.RetoDatos[i].EsDato)
            {
                TableroDato newDato = Instantiate(_tableroDatoPrefab, _container.transform);
                newDato.Init(_reto.RetoDatos[i]);
                _datos.Add(newDato);
            }
        }
    }

}
