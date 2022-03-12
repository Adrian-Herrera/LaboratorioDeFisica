using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    [SerializeField] private GameObject Number;
    [SerializeField] private GameObject Exponent;
    [SerializeField] private GameObject Fraction;
    [SerializeField] private GameObject OP;
    [SerializeField] private GameObject Sqrt;
    public GameObject step;

    private void Start()
    {
        CreateText("ts");
        CreateText("=");
        CreateFraction(CreateText("Vo"), CreateText("g"));
        CreateText("=");
        CreateFraction(CreateOP(CreateText("12"), CreateFraction(CreateText("m"), CreateText("s")), ""), CreateOP(CreateText("9.8"), CreateFraction(CreateText("m"), CreateText("s")), ""));
        CreateText("=");
        CreateText("1,22s");
        // GameObject n1 = CreateText("123");
        // CreateSqrt(n1);
        // CreateSqrt(CreateText("456"));
        CreateSqrt(CreateOP(CreateText("12"), CreateText("34"), "*"));
        // CreateSqrt(CreateFraction(CreateFraction(CreateText("1"), CreateText("2")), CreateText("3")));
    }
    private GameObject CreateText(string s)  // number script
    {
        GameObject go = Instantiate(Number, step.transform);
        go.GetComponent<StepNumber>().init(s);
        return go;
    }
    private void CreateExponent(GameObject g, string s) // number script
    {
        GameObject go = Instantiate(Exponent, g.transform);
        go.GetComponent<StepNumber>().init(s);
    }
    private GameObject CreateFraction(GameObject Numerator, GameObject Denominator) // fraction script
    {
        GameObject go = Instantiate(Fraction, step.transform);
        go.GetComponent<StepFraction>().checkSize(Numerator, Denominator);
        return go;
    }
    private GameObject CreateOP(GameObject g1, GameObject g2, string op) // doesn´t have script
    {
        GameObject go = Instantiate(OP, step.transform);
        GameObject Op = CreateText(op);

        RectTransform rt1 = g1.GetComponent<RectTransform>();
        RectTransform rt2 = g2.GetComponent<RectTransform>();
        RectTransform rtOp = Op.GetComponent<RectTransform>();

        float width = rt1.rect.width + rt2.rect.width + rtOp.rect.width;
        float height = Mathf.Max(rt1.rect.height, rt2.rect.height);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        g1.transform.SetParent(go.transform);
        Op.transform.SetParent(go.transform);
        g2.transform.SetParent(go.transform);

        return go;
    }

    private GameObject CreateSqrt(GameObject g1) // sqrt script
    {
        GameObject go = Instantiate(Sqrt, step.transform);
        go.GetComponent<StepSqrt>().CalculateSize(g1);
        return go;
    }
}
