using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HeaderButton : MonoBehaviour
{
    // public Header_manager manager;
    private Sprite imageCover;
    private Button _button;
    private string title;
    private int type;

    public void setData(string _title, int _type)
    {
        title = _title;
        type = _type;
    }

    private void Awake()
    {
        // GetComponent<Image>().sprite = SO_TypeButton._sprite;
        _button = GetComponent<Button>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _button.GetComponentInChildren<TMP_Text>().text = title;

    }
}
