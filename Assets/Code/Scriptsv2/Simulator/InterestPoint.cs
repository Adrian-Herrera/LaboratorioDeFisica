using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InterestPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text _label;
    [SerializeField] GameObject _background;
    public InterestPoint CreatePoint(Vector3 pos, float scale, string message)
    {
        transform.position = pos;
        transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
        _label.text = message;
        return this;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _label.gameObject.SetActive(true);
        _background.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _label.gameObject.SetActive(false);
        _background.SetActive(false);
    }
}
