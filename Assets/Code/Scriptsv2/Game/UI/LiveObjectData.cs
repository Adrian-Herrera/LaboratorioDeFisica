using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveObjectData : MonoBehaviour
{
    [SerializeField] private Car3d _car;
    [SerializeField] private TMP_Text _velocityTxt;
    [SerializeField] private TMP_Text _distanceTxt;
    [SerializeField] private TMP_Text _timeTxt;
    private void Update()
    {
        if (_car.IsMoving)
        {
            _velocityTxt.text = _car.Vel.ToString();
            _distanceTxt.text = _car.DistanceFromStart.ToString("F2");
            _timeTxt.text = (_car.TimeMoving / 1000).ToString("F2");
        }
    }
}
