using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelector : View
{
    [SerializeField] private Button _retoButton;
    [SerializeField] private Button _libreButton;
    [SerializeField] private Button _pruebaButton;
    protected override void Init()
    {
        if (CredentialManager.Current != null)
        {
            _retoButton.interactable = CredentialManager.Current.IsAuth;
        }
        _retoButton.onClick.AddListener(() =>
        {
            PlayerUI.Instance.ShowStationUI(Station.ModeEnum.Reto);
        });
        _libreButton.onClick.AddListener(() =>
        {
            PlayerUI.Instance.ShowStationUI(Station.ModeEnum.Libre);
        });
    }

}
