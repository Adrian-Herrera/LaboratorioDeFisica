using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterManager : MonoBehaviour
{
    [SerializeField] private FooterElement[] _footerElements;
    private void Awake()
    {
        _footerElements = GetComponentsInChildren<FooterElement>();
    }
    public void Init(MainObject mainObject)
    {
        Debug.Log("Footer Manager Init");
        foreach (FooterElement footerElement in _footerElements)
        {
            footerElement.Init(mainObject);
        }
    }
}
