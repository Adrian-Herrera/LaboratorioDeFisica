using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveObjectData : MonoBehaviour
{
    [SerializeField] private CinematicObject _cinematicObject;
    [SerializeField] private TMP_Text _velocityTxt;
    [SerializeField] private TMP_Text _distanceTxt;
    [SerializeField] private TMP_Text _timeTxt;
    private void Update()
    {
        _velocityTxt.text = _cinematicObject.ActualVelX.ToString();
        _distanceTxt.text = _cinematicObject.DistanceFromStart.ToString("F2");
        _timeTxt.text = _cinematicObject.TimeMoving.ToString("F2");
    }
}
