using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCanvas : MonoBehaviour
{
    public MainMenuCanvasSelector.SelectCanvas DestinationCanvas;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Change);
    }
    private void Change()
    {
        MainMenuCanvasSelector.Instance.GoToCanvas(DestinationCanvas);
    }
}
