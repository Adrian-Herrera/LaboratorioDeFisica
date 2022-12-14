using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MessageBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject MessageGO;
    private RectTransform Arrow;
    private bool isInstantiated = false;
    public enum Positions
    {
        up, down, left, rigth
    }
    public enum ShowMode
    {
        HideAfterDelay, ShowAfterDelay
    }
    [SerializeField] private Positions _pos;
    [SerializeField] private ShowMode _showMode;
    [SerializeField] private float Seconds = 3f;

    private RectTransform _RT;
    void Start()
    {
        MessageGO.SetActive(false);
    }
    public void SetPosition(Positions position)
    {
        Debug.Log("set position " + gameObject.name + " " + position);
        switch (position)
        {
            case Positions.down:
                ChangePivotAndPosition(_RT, 0.5f, 0);
                SetArrowOptions(0.5f, 1, 0, -12.5f, 180);

                _RT.anchoredPosition = new Vector2(0, -_RT.sizeDelta.y - 15);

                break;
            case Positions.left:
                ChangePivotAndPosition(_RT, 0, 0.5f);
                SetArrowOptions(1, 0.5f, 0, 10, 90);
                _RT.anchoredPosition = new Vector2(-_RT.sizeDelta.x - 15, 0);

                break;
            case Positions.rigth:
                ChangePivotAndPosition(_RT, 1, 0.5f);
                SetArrowOptions(0, 0.5f, 0, 10, -90);
                _RT.anchoredPosition = new Vector2(_RT.sizeDelta.x + 15, 0);

                break;
            default: // Positions.up
                ChangePivotAndPosition(_RT, 0.5f, 1);
                SetArrowOptions(0.5f, 0, 0, -12.5f, 0);
                _RT.anchoredPosition = new Vector2(0, _RT.sizeDelta.y + 15);
                break;
        }
    }
    private void ChangePivotAndPosition(RectTransform rt, float x, float y)
    {
        rt.pivot = new Vector2(x, y);
        rt.anchorMin = new Vector2(x, y);
        rt.anchorMax = new Vector2(x, y);
    }
    private void SetArrowOptions(float x, float y, float posx, float posy, float rotation)
    {
        Arrow.pivot = new Vector2(x, y);
        Arrow.anchorMin = new Vector2(x, y);
        Arrow.anchorMax = new Vector2(x, y);
        Arrow.anchoredPosition = new Vector2(posx, posy);
        Arrow.eulerAngles = new Vector3(0, 0, rotation);
    }

    public void SetText(string Message)
    {
        float size = (float)Message.Length / 20;
        _RT.sizeDelta = new Vector2(200, 50 * (size / 2));

        MessageGO.GetComponentInChildren<TMP_Text>().text = Message;
    }

    public void Show(string Message)
    {
        if (isInstantiated)
        {
            SetText(Message);
            SetPosition(_pos);
        }
        else
        {
            MessageGO = Instantiate(MessageGO, transform);
            _RT = MessageGO.GetComponent<RectTransform>();
            Arrow = MessageGO.transform.Find("Arrow").gameObject.GetComponent<RectTransform>();
            SetText(Message);
            SetPosition(_pos);
            isInstantiated = true;
        }
        switch (_showMode)
        {
            case ShowMode.HideAfterDelay:
                StartCoroutine(HideAfter(Seconds));
                break;
            case ShowMode.ShowAfterDelay:
                StartCoroutine(ShowAfter(Seconds));
                break;
            default: return;
        }
        // StartCoroutine(ShowWithDelay(Seconds));
    }
    private IEnumerator HideAfter(float time)
    {
        MessageGO.SetActive(true);
        yield return new WaitForSeconds(time);
        MessageGO.SetActive(false);
    }
    private IEnumerator ShowAfter(float time)
    {
        MessageGO.SetActive(false);
        yield return new WaitForSeconds(time);
        MessageGO.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MessageGO.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MessageGO.SetActive(false);
    }

}
