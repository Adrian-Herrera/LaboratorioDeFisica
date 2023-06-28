using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetoFinalInfo : View
{
    [Header("Prefabs")]
    [SerializeField] private RetoInfo _retoInfoPrefab;
    [Header("Container")]
    [SerializeField] private GameObject _container;
    [Header("Components")]
    [SerializeField] private Button _exitBtn;

    private void Awake()
    {
        _exitBtn.onClick.AddListener(() =>
        {
            PlayerUI.Instance._actualView.Hide();
            PlayerUI.Instance._actualView = PlayerUI.Instance._retoSelector;
            PlayerUI.Instance._actualView.Show();
            PlayerUI.Instance._retoSelector.Init(1, PlayerUI.Instance.Player.NearStation.TemaId);
        });
    }
    // Start is called before the first frame update
    public void Init(Cuestionario actualReto, int intentos)
    {
        Debug.Log("Creando instancias");
        RetoInfo info = Instantiate(_retoInfoPrefab, _container.transform);
        info.Init(actualReto, intentos);
        info.name = "Reto";
    }
}
