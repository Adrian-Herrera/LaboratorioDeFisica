using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanelButton : MonoBehaviour
{
    [SerializeField] private View _view;
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(_view.SwitchView);
    }
}
