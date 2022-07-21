using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    public int id;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void Init(int _id, int index)
    {
        id = _id;
        _button.GetComponentInChildren<TMP_Text>().text = index.ToString();
        _button.onClick.AddListener(delegate { SendId(id); });
    }
    private void SendId(int _id)
    {
        LevelManager.Instance.quizId = _id;
        LevelManager.Instance.LoadScene("Exercises");
    }
}
