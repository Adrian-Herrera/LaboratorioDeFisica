using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuadraticSolver : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas;
    [SerializeField] private TMP_Text Message;
    [SerializeField] private Button btn1, btn2;
    private bool _x1 = false, _x2 = false;
    private float answer;
    public static QuadraticSolver Current;
    private void Awake()
    {
        Current = this;
    }
    private void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(MainCanvas.GetComponent<RectTransform>().rect.width / 4, MainCanvas.GetComponent<RectTransform>().rect.height / 4);
        _x1 = false; _x2 = false;
        btn1.onClick.AddListener(delegate { _x1 = true; });
        btn2.onClick.AddListener(delegate { _x2 = true; });
        gameObject.SetActive(false);
    }
    public IEnumerator createPanel(Field x, string variable, float x1, float x2)
    {
        gameObject.SetActive(true);
        Message.text = "La variable " + variable + " tiene dos posibles resultados. Escoja uno por favor";
        btn1.GetComponentInChildren<TMP_Text>().text = x1.ToString();
        btn2.GetComponentInChildren<TMP_Text>().text = x2.ToString();
        yield return askAnswer();
        answer = _x1 == true ? x1 : x2;
        gameObject.SetActive(false);
        x.value = answer;
        yield return null;
    }

    IEnumerator askAnswer()
    {
        yield return new WaitUntil(() => (_x1 == true || _x2 == true));
    }
}
