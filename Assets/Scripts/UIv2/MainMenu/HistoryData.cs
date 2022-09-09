using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryData : MonoBehaviour
{
    [SerializeField] private TMP_Text _attempt;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private TMP_Text _score;
    public void Init(string attempt, string time, string score)
    {
        _attempt.text = attempt;
        _time.text = time;
        _score.text = score;
    }

}
