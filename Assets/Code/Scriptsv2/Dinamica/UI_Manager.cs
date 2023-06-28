using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;
    [SerializeField] private UI_ObjectSelected _objectSelected;

    [SerializeField] private GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] private EventSystem m_EventSystem;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _objectSelected.SetSelectedObject(SelectGO());

        }
    }
    public GameObject SelectGO()
    {
        Vector2 rayPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        Collider2D detectedCollider =
            Physics2D.OverlapBox(rayPos, new Vector2(0.2f, 0.2f), 0);

        if (detectedCollider)
        {
            Debug.Log(detectedCollider.transform.name);
            detectedCollider.transform.GetComponent<DragObject>().StartMovement();
            return detectedCollider.transform.gameObject;
        }
        else return null;
    }
}
