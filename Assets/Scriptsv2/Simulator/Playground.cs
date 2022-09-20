using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    public static Playground Instance;
    private MainObject _mainObject;
    [SerializeField] private LineDrawer[] _lines;
    public float TotalSize;
    private int maxSize = 20;
    public event Action<float> OnChangeScale;
    public void ChangeScale(float scale)
    {
        OnChangeScale?.Invoke(scale);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Init()
    {
        _mainObject = SimulatorManager._selectedObject;
        DrawLines();
    }
    public void DrawLines()
    {
        TotalSize = 0;
        for (int i = 0; i < _lines.Length; i++)
        {
            if (i < _mainObject._activeSegments)
            {
                _lines[i].gameObject.SetActive(true);
                Dato dato = _mainObject._segmentos[i].datos.Find(dato => dato.VariableId == 2);
                if (dato != null && dato._valor > 0)
                {
                    print("Dato existe en segmneto " + i);
                    _lines[i].Init(dato._valor, TotalSize);
                    TotalSize += dato._valor;
                }
                else
                {
                    print("Dato no existe en segmneto " + i);
                    _lines[i].Init(15, TotalSize, false);
                    TotalSize += 15;
                }
            }
            else
            {
                _lines[i].gameObject.SetActive(false);
            }
        }
        float scale = TotalSize / maxSize > 1 ? TotalSize / maxSize : 1;

        Camera.main.orthographicSize = 10 * scale;
        Camera.main.transform.position = new Vector3(15 * scale, 0, -10);

        ChangeScale(scale);
    }

}
