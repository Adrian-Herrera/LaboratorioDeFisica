using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectProperties : MonoBehaviour
{
    [SerializeField] private SegmentProperties[] _segments;
    public void Init()
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
    }
}
