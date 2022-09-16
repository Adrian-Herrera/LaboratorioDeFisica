using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphic : MonoBehaviour
{
    [SerializeField] private HorizontalLine[] _lines;
    private void Awake()
    {
        _lines = GetComponentsInChildren<HorizontalLine>();
    }
    public void Init(Pregunta pregunta)
    {
        // Dato[] datos = pregunta.info
        List<Dato> segmento0 = new();
        List<Dato> segmento1 = new();
        List<Dato> segmento2 = new();
        List<Dato> segmento3 = new();
        _lines[1].gameObject.SetActive(false);
        _lines[2].gameObject.SetActive(false);
        foreach (Dato dato in pregunta.Datos)
        {
            switch (dato.Segmento)
            {
                case 1:
                    segmento1.Add(dato);
                    break;
                case 2:
                    segmento2.Add(dato);
                    break;
                case 3:
                    segmento3.Add(dato);
                    break;
                default:
                    segmento0.Add(dato);
                    break;
            }
        }

        _lines[0].Init(segmento1.ToArray());

        if (segmento2.Count > 0)
        {
            _lines[1].gameObject.SetActive(true);
            _lines[1].Init(segmento2.ToArray());
        }
        if (segmento3.Count > 0)
        {
            _lines[2].gameObject.SetActive(true);
            _lines[2].Init(segmento3.ToArray());
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
