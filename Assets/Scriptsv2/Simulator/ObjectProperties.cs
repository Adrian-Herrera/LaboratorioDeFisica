using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectProperties : MonoBehaviour
{
    [SerializeField] private SegmentProperties[] _segments;
    [SerializeField] private Button _addSegment;
    [SerializeField] private Button _removeSegment;
    public void Init()
    {
        _addSegment.onClick.AddListener(delegate
        {
            if (SimulatorManager._selectedObject._activeSegments < 3)
            {
                SimulatorManager._selectedObject._activeSegments++;
                ActivateSegments();
            }

        });
        _removeSegment.onClick.AddListener(delegate
        {
            if (SimulatorManager._selectedObject._activeSegments > 1)
            {
                SimulatorManager._selectedObject._activeSegments--;
                ActivateSegments();
            }

        });
        ActivateSegments();
    }
    private void ActivateSegments()
    {
        for (int i = 0; i < _segments.Length; i++)
        {
            if (i < SimulatorManager._selectedObject._activeSegments)
            {
                _segments[i].gameObject.SetActive(true);
                _segments[i].Init(i);
            }
            else
            {
                _segments[i].gameObject.SetActive(false);
            }
        }
        Playground.Instance.DrawLines();
    }

}
