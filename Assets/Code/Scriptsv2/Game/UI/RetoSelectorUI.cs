using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RetoSelectorUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _startButton;
    [SerializeField] private int _idReto;
    private Image _background;
    private static RetoSelectorUI RetoSelected;
    private void Awake()
    {
        _background = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(StartReto);
        _startButton.gameObject.SetActive(false);
        DeselectReto();
    }
    public void Init(int idReto, string title)
    {
        _title.text = title;
        _idReto = idReto;
    }
    public void StartReto()
    {
        Debug.Log("Reto: " + _idReto);
        StartCoroutine(ServerMethods.Current.GetReto(_idReto, (res) =>
        {
            PlayerUI.Instance.SetStationReto(res);
        }));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click en reto");
        SelectReto();
    }
    private void SelectReto()
    {
        if (RetoSelected != null)
        {
            RetoSelected.DeselectReto();
        }
        RetoSelected = this;
        _startButton.gameObject.SetActive(true);
        _background.color = Color.green;
    }
    private void DeselectReto()
    {
        _background.color = Helpers.HexColor("#00DEFF");
        _startButton.gameObject.SetActive(false);
    }

}
