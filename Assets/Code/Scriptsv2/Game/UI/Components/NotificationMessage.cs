using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationMessage : MonoBehaviour
{
    public static NotificationMessage Instance;
    // Components
    [SerializeField] private GameObject _content;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private int _secondsShow;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Show(string title, string message, int seconds = 0)
    {
        _title.text = title;
        _message.text = message;
        if (seconds != 0)
        {
            StartCoroutine(ShowWithTime(seconds));
        }
        _content.SetActive(true);
    }
    public void Hide()
    {
        _content.SetActive(false);
    }
    private IEnumerator ShowWithTime(int seconds)
    {
        _content.SetActive(true);
        yield return new WaitForSeconds(seconds);
        _content.SetActive(false);
    }

}
