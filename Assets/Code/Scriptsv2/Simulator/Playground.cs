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
    // [SerializeField] private SizesLines _sizesLines;

    [SerializeField] private SizesLines _alturaMaxima;
    public float TotalSize;
    public float smoothTime = 1f;
    private Vector3 _cameraOriginalPos;
    private int maxSizeX = 20;
    private bool _isChanging = false;
    public float Scale = 1f;
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
    private void Update()
    {
        if (_mainObject != null)
        {
            if (_mainObject.transform.position.y > (13 * Scale) || _mainObject.transform.position.x > (20 * Scale) && _isChanging == false)
            {
                ZoomOut();
            }
        }
    }
    public void Init()
    {
        _mainObject = SimulatorManager._selectedObject;
        _cameraOriginalPos = Camera.main.transform.position;
        _building.gameObject.SetActive(false);
        _alturaMaxima.Init();
        _alturaMaxima.gameObject.SetActive(false);
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
                    Dato dato = _mainObject._segmentos[i].datos.Find(dato => dato.VariableId == 2); // Distancia
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
        else if (LevelManager.Instance.temaId == 3 || LevelManager.Instance.temaId == 4)
        {
            Dato alturaInicial = _mainObject._segmentos[0].datos.Find(dato => dato.VariableId == 12); // Altura inicial
            if (alturaInicial != null && alturaInicial.Valor >= 0)
            {
                _building.gameObject.SetActive(true);
                _building.sizeDelta = new Vector2(5, alturaInicial.Valor);
                _mainObject.transform.position = new Vector3(_mainObject.transform.position.x, alturaInicial.Valor, _mainObject.transform.position.z);
            }
            else
            {
                _building.gameObject.SetActive(false);
            }

        }
    }
    public void DrawLinesWhenStop()
    {
        if (LevelManager.Instance.temaId == 3 || LevelManager.Instance.temaId == 4)
        {
            Dato alturaInicial = _mainObject._segmentos[0].datos.Find(dato => dato.VariableId == 12);
            _alturaMaxima.gameObject.SetActive(true);
            if (alturaInicial != null)
            {
                float size = _mainObject.maxHeight.y - alturaInicial.Valor;
                _alturaMaxima.SetNewData(size, "y-max = " + size.ToString("F2") + "m");
                _alturaMaxima.transform.position = new Vector3(0, alturaInicial.Valor, 0);
            }
            else
            {
                _alturaMaxima.SetNewData(_mainObject.maxHeight.y, "y-max = " + _mainObject.maxHeight.y.ToString("F2") + "m");
                _alturaMaxima.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
    private IEnumerator ChangeCameraSize(float scale, float time)
    {
        float rate = 0;
        float timer = 0;
        float initialSize = Camera.main.orthographicSize;
        float x = Camera.main.transform.position.x;
        float y = Camera.main.transform.position.y;
        Vector3 destination = Vector3.Scale(_cameraOriginalPos, new Vector3(scale, scale, 1));
        while (rate < 1 || Camera.main.transform.position != destination)
        {
            if (rate < 1)
            {
                timer += Time.fixedDeltaTime;
                rate = timer / time;
                Camera.main.orthographicSize = Mathf.Lerp(initialSize, 10 * scale, rate);

                Camera.main.transform.position = new Vector3(Mathf.Lerp(x, destination.x, rate), Mathf.Lerp(y, destination.y, rate), -10);
            }
            yield return new WaitForFixedUpdate();
        }
        _isChanging = false;
    }
    public void ZoomIn()
    {
        Scale /= 1.5f;
        _isChanging = true;
        StartCoroutine(ChangeCameraSize(Scale, smoothTime));
        ChangeScale(Scale);
    }
    public void ZoomOut()
    {
        Scale *= 1.5f;
        _isChanging = true;
        StartCoroutine(ChangeCameraSize(Scale, smoothTime));
        ChangeScale(Scale);
    }

}
