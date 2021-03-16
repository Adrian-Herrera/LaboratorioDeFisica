using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Line : MonoBehaviour, IPointerDownHandler
{
    private GameObject data;

    public GameObject car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(car.name + " Was Clicked.");
    }
}
