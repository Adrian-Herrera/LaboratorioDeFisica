using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private List<TabButtonUI> _tabs;
    [SerializeField] private List<GameObject> _pages;
    private TabButtonUI _activeTab;
    // private GameObject _activePage;

    // SPRITES
    [SerializeField] private Sprite _normalState;
    [SerializeField] private Sprite _activeState;

    private void Start()
    {
        ResetTabs();
        OnTabSelected(_tabs[0]);
    }
    public void OnTabEnter(TabButtonUI button)
    {
        button.Background.sprite = _activeState;
    }
    public void OnTabExit(TabButtonUI button)
    {
        if (_activeTab != button)
        {
            button.Background.sprite = _normalState;
        }
    }
    public void OnTabSelected(TabButtonUI button)
    {
        if (_activeTab != null)
        {
            _activeTab.Background.sprite = _normalState;
        }
        if (_activeTab == button) return;
        _activeTab = button;
        int index = _tabs.FindIndex(e => e == button);
        for (int i = 0; i < _pages.Count; i++)
        {
            if (i == index)
            {
                _pages[i].SetActive(true);
                if (_pages[i].TryGetComponent(out ITab itab))
                {
                    itab.Init();
                }
                // _pages[i].GetComponent<ITab>().Init();
            }
            else
            {
                _pages[i].SetActive(false);
            }
        }
        button.Background.sprite = _activeState;
    }
    private void ResetTabs()
    {
        foreach (TabButtonUI tab in _tabs)
        {
            tab.GetComponent<Image>().sprite = _normalState;
        }
    }
}
public interface ITab
{
    public void Init();
}
