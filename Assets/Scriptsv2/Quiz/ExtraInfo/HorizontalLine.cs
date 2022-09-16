using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorizontalLine : MonoBehaviour
{
    [SerializeField] private LineInfo _lineInfo;
    [SerializeField] private TMP_Text _start;
    [SerializeField] private TMP_Text _end;
    [SerializeField] private GameObject _infoContainer;

    public LineInfo[] _infoArr;

    public void Init(Dato[] datos)
    {
        _infoArr = _infoContainer.GetComponentsInChildren<LineInfo>(true);
        // _start.text = start;
        // _end.text = end;

        for (int i = 0; i < datos.Length; i++)
        {
            if (i < _infoArr.Length)
            {
                _infoArr[i].gameObject.SetActive(true);
                _infoArr[i].Init(datos[i]);
            }
            else
            {
                GameObject info = Instantiate(_lineInfo.gameObject, _infoContainer.transform);
                info.GetComponent<LineInfo>().Init(datos[i]);
            }
        }
        if (datos.Length < _infoArr.Length)
        {
            for (int i = datos.Length; i < _infoArr.Length; i++)
            {
                _infoArr[i].gameObject.SetActive(false);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_infoContainer.GetComponent<RectTransform>());
    }
}
