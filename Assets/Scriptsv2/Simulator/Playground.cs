using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    public static Playground Instance;
    private MainObject _mainObject;
    [SerializeField] private LineDrawer[] _lines;
    [SerializeField] private RectTransform _building;
    public float TotalSize;
    private int maxSizeX = 20;
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
        if (LevelManager.Instance.temaId == 2)
        {
            TotalSize = 0;
            for (int i = 0; i < _lines.Length; i++)
            {
                if (i < _mainObject._activeSegments)
                {
                    _lines[i].gameObject.SetActive(true);
                    Dato dato = _mainObject._segmentos[i].datos.Find(dato => dato.VariableId == 2);
                    if (dato != null && dato.Valor > 0)
                    {
                        print("Dato existe en segmneto " + i);
                        _lines[i].Init(dato.Valor, TotalSize);
                        TotalSize += dato.Valor;
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
            float scale = TotalSize / maxSizeX > 1 ? TotalSize / maxSizeX : 1;

            Camera.main.orthographicSize = 10 * scale;
            Camera.main.transform.position = new Vector3(15 * scale, 0, -10);

            ChangeScale(scale);
        }
        else if (LevelManager.Instance.temaId == 3)
        {
            _building.gameObject.SetActive(true);
            Dato dato = _mainObject._segmentos[0].datos.Find(dato => dato.VariableId == 12);
            if (dato != null && dato.Valor >= 0)
            {
                _building.sizeDelta = new Vector2(5, dato.Valor);
                SimulatorManager._selectedObject.transform.position = new Vector3(SimulatorManager._selectedObject.transform.position.x, dato.Valor, SimulatorManager._selectedObject.transform.position.z);
            }
        }
    }

}
