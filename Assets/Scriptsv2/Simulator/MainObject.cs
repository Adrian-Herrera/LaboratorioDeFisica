using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MainObject : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    private Sprite _mainObject;
    private Transform _transform;
    public Segmento[] _segmentos = new Segmento[3];
    public int _activeSegments = 1;
    public bool IsMoving = false;
    private Rigidbody2D _rb;
    private float Vel, _segmentDistance = 0, _segmentTimer = 0, _segmentStart;
    public float? Velo, Velf, Acc, Timer, Distance;
    private int _actualSegmentId = 0;
    private List<Vector3> _dotsList = new();
    public event Action<int> OnAddSegmentData;
    public event Action<int> OnRemoveSegmentData;
    public void AddSegmentData(int segmentId)
    {
        OnAddSegmentData?.Invoke(segmentId);
    }
    public void RemoveSegmentData(int segmentId)
    {
        OnRemoveSegmentData?.Invoke(segmentId);
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Init()
    {
        _transform = GetComponent<Transform>();
        Playground.Instance.OnChangeScale += ChangeScale;
    }
    private void Update()
    {
        if (!IsMoving) return;
        if (LevelManager.Instance.temaId == 2)
        {
            // _rb.MovePosition(_rb.position + new Vector2(Vel, 0) * Time.deltaTime);
            _rb.velocity = new Vector2(Vel, 0);
            if (Acc.HasValue) Vel += Acc.Value * Time.deltaTime;
            _segmentTimer += Time.deltaTime;
            _segmentDistance = _rb.position.x - _segmentStart;
            if (CheckConditionToStop())
            {
                if (_actualSegmentId + 1 < _activeSegments)
                {
                    _actualSegmentId++;
                    print("actual Segment: " + _actualSegmentId);
                    StartMovement();
                }
                else
                {
                    print("Detenido");
                    _rb.velocity = Vector2.zero;
                    IsMoving = false;
                }
            }
        }
        else if (LevelManager.Instance.temaId == 3)
        {
            _rb.velocity = new Vector2(1, Vel);
            if (Acc.HasValue) Vel += Acc.Value * Time.deltaTime;
            _segmentTimer += Time.deltaTime;
            _segmentDistance = _rb.position.x - _segmentStart;
            if (CheckConditionToStop())
            {
                StopCoroutine(DrawDots());
                if (_actualSegmentId + 1 < _activeSegments)
                {
                    _actualSegmentId++;
                    print("actual Segment: " + _actualSegmentId);
                    StartMovement();
                }
                else
                {
                    print("Detenido");
                    StopCoroutine(DrawDots());
                    _rb.velocity = Vector2.zero;
                    IsMoving = false;
                }
            }
        }
    }
    public void AddDato(int segmentId, int variableId)
    {
        Dato dato = new(variableId, 0, 1, 1);
        _segmentos[segmentId].datos.Add(dato);
        AddSegmentData(segmentId);
    }
    public void RemoveDato(int segmentId, int variableId)
    {
        Dato dt = _segmentos[segmentId].datos.Find(dato => dato.VariableId == variableId);
        if (dt == null) return;
        _segmentos[segmentId].datos.Remove(dt);
        RemoveSegmentData(segmentId);
    }
    public bool ResolveDato(int segmentId, int variableId)
    {
        Dato dato = new(variableId, 0, 1, 1);
        bool IsAnswered = Formulary.Instance.CheckRequirements(dato, _segmentos[segmentId].datos.ToArray());
        if (IsAnswered)
        {
            _segmentos[segmentId].datos.Add(dato);
            AddSegmentData(segmentId);
        }
        return IsAnswered;
    }
    private void ChangeScale(float scale)
    {
        _transform.localScale = new Vector3(scale, scale, 1);
        switch (LevelManager.Instance.temaId)
        {
            case 2:
                _transform.position = new Vector3(_transform.position.x, 0.5f * scale, 0);
                break;
            case 3:
                _transform.position = new Vector3(0.5f * scale, _transform.position.y, 0);
                break;

            default:
                break;
        }
    }
    public void StartMovement()
    {
        Velo = _segmentos[_actualSegmentId].datos.Find(dato => dato.VariableId == 4)?.Valor ?? 0f;
        Velf = _segmentos[_actualSegmentId].datos.Find(dato => dato.VariableId == 5)?.Valor;
        Acc = _segmentos[_actualSegmentId].datos.Find(dato => dato.VariableId == 6)?.Valor ?? 0f;
        Timer = _segmentos[_actualSegmentId].datos.Find(dato => dato.VariableId == 3)?.Valor;
        Distance = _segmentos[_actualSegmentId].datos.Find(dato => dato.VariableId == 2)?.Valor;
        Vel = Velo.Value;
        _segmentStart = _rb.position.x;
        _segmentDistance = 0;
        _segmentTimer = 0;
        IsMoving = true;
        _dotsList.Clear();
        StartCoroutine(DrawDots());
    }
    private bool CheckConditionToStop()
    {
        if (LevelManager.Instance.temaId == 2)
        {
            if (Velf.HasValue)
            {
                if (Velf > Velo)
                {
                    return Vel >= Velf.Value;
                }
                else
                {
                    return Vel <= Velf.Value;
                }
            }
            else if (Timer.HasValue)
            {
                return _segmentTimer >= Timer.Value;
            }
            else if (Distance.HasValue)
            {
                return _segmentDistance >= Distance.Value;
            }
        }
        else
        {
            if (LevelManager.Instance.temaId == 3)
            {
                return SimulatorManager._selectedObject.transform.position.y < 0;
            }
        }
        return false;
    }
    public void Reset()
    {
        IsMoving = false;
        _actualSegmentId = 0;
        switch (LevelManager.Instance.temaId)
        {
            case 2:
                _rb.position = new Vector2(0f, _rb.position.y);
                break;
            case 3:
                Dato dato = SimulatorManager._selectedObject._segmentos[0].datos.Find(dato => dato.VariableId == 12);
                if (dato != null && dato.Valor >= 0)
                {
                    _rb.position = new Vector2(0f, 0f + dato.Valor);
                }
                else
                {
                    _rb.position = new Vector2(0f, 0f);
                }

                break;

            default:
                break;
        }
        // _rb.position = new Vector2(0f, _rb.position.y);
        _rb.velocity = Vector2.zero;

    }

    private IEnumerator DrawDots()
    {
        while (IsMoving)
        {
            _dotsList.Add(transform.position);
            _lineRenderer.positionCount = _dotsList.Count;
            _lineRenderer.SetPositions(_dotsList.ToArray());
            yield return new WaitForSeconds(.1f);
        }
    }

}
[Serializable]
public class Segmento
{
    public List<Dato> datos = new();
}
