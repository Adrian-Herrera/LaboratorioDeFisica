using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    private enum SceneName
    {
        MainMenu,
        Exercises,
        Virtual,
        Calculadora
    }
    [SerializeField] private SceneName _changeTo;
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeScene);
    }
    public void ChangeScene()
    {
        Debug.Log(_changeTo.ToString());
        LevelManager.Instance.LoadScene(_changeTo.ToString());
    }
}
