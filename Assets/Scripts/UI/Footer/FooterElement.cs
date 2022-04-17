using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FooterElement : MonoBehaviour
{
    [SerializeField] TMP_Text _segmentText;
    private Image _image;
    private Segment[] _segments;
    private ExtraData _extraData;
    private int _activateSegments;
    private void Awake()
    {
        _segments = GetComponentsInChildren<Segment>();
        _extraData = GetComponentInChildren<ExtraData>();
    }
    public void Init(MainObject mainObject)
    {
        Debug.Log("Footer Element Init");
        _activateSegments = 1;
        _segmentText.text = _activateSegments.ToString();
        _image = mainObject.Sprite;
        for (int i = 0; i < _segments.Length; i++)
        {
            _segments[i].Init(mainObject.names, i);
        }
        _extraData.Init(mainObject.extraNames);

        SetElements(mainObject);
        ActivateSegments(_activateSegments);

    }

    private void SetElements(MainObject mainObject)
    {
        // Debug.Log("Set Elements");
        for (int i = 0; i < mainObject.DataLengthi; i++)
        {
            for (int j = 0; j < mainObject.DataLengthJ; j++)
            {
                mainObject.SetElement(i, j, _segments[i]._segmentElements[j]);
            }
        }
    }

    private void ActivateSegments(int actives)
    {
        int temp = 1;
        foreach (Segment segment in _segments)
        {
            segment.gameObject.SetActive(temp <= actives);
            temp++;
        }
    }
    public void AddSegment()
    {
        if (_activateSegments < 3)
        {
            _activateSegments++;
            ActivateSegments(_activateSegments);
            _segmentText.text = _activateSegments.ToString();
        }
    }
    public void RemoveSegment()
    {
        if (_activateSegments > 0)
        {
            _activateSegments--;
            ActivateSegments(_activateSegments);
            _segmentText.text = _activateSegments.ToString();
        }
    }
}
