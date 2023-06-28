using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetoSelectorMenu : View
{
    [SerializeField] private RetoSelectorUI _selectorPrefab;
    [SerializeField] private GameObject _selectorContainer;
    [SerializeField] private List<RetoSelectorUI> _menuItems = new();
    public void Init(int codigoId, int temaId)
    {
        gameObject.SetActive(true);
        Helpers.ClearListContent(_menuItems);
        StartCoroutine(ServerMethods.Current.GetRetos(codigoId, temaId, (res) =>
        {
            foreach (Cuestionario reto in res)
            {
                RetoSelectorUI rs = Instantiate(_selectorPrefab, _selectorContainer.transform);
                rs.Init(reto.Id, reto.Titulo);
                _menuItems.Add(rs);
            }
        }));
    }

}
