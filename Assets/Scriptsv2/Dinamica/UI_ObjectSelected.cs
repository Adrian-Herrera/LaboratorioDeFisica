using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ObjectSelected : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TMP_Text _objectName;
    public GameObject GoSelected;
    [SerializeField] private C_Slider _degree;

    public void SetSelectedObject(GameObject go)
    {
        if (go == null)
        {
            // _container.SetActive(false);
            return;
        }
        GoSelected = go;
        _container.SetActive(true);
        _objectName.text = GoSelected.name;
        _degree._slider.onValueChanged.AddListener((v) => { GoSelected.transform.eulerAngles = new Vector3(0, 0, v); });
    }

}
