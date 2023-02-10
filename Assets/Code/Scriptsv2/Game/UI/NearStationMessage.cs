using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NearStationMessage : View
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _text;
    public void Init(Station station)
    {
        _text.text = station.Name;
    }
}
