using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HoverManager : MonoBehaviour
{
    public static HoverManager Current;
    public Dictionary<GameObject, GameObject> Boxes = new Dictionary<GameObject, GameObject>();
    [SerializeField] private GameObject HoverBox;
    [SerializeField] private float Seconds = 3f;
    private bool ShowMessage = false;

    private void Awake()
    {
        Current = this;
    }
    public GameObject SearchBox(GameObject parent)
    {
        if (Boxes.ContainsKey(parent))
        {
            return Boxes[parent];
        }
        else
        {
            Boxes[parent] = Instantiate(HoverBox, parent.transform);
            return Boxes[parent];
        }
    }
    public void show(GameObject parent, string message, HoverMessage.Positions pos)
    {
        SearchBox(parent).GetComponent<HoverMessage>().CreateHover(message, pos);
        ShowMessage = true;
        StartCoroutine(ShowAfter(SearchBox(parent)));
    }
    public void hide(GameObject parent)
    {
        SearchBox(parent).SetActive(false);
        ShowMessage = false;
    }
    private IEnumerator ShowAfter(GameObject go)
    {
        yield return new WaitForSeconds(Seconds);
        if (ShowMessage)
        {
            go.SetActive(true);
        }
    }
}
