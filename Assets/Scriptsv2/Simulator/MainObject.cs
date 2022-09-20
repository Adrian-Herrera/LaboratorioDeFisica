using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MainObject : MonoBehaviour
{
    private Sprite _mainObject;
    private Transform _transform;
    public Segmento[] _segmentos = new Segmento[3];
    public int _activeSegments = 1;
    public bool IsMoving = false;
    private Rigidbody2D _rb;
    private float Vel, _segmentDistance = 0, _segmentTimer = 0, _segmentStart;
    public float? Velo, Velf, Acc, Timer, Distance;
    private int _actualSegmentId = 0;
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
        if (IsMoving)
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
        _transform.position = new Vector3(_transform.position.x, 0.5f * scale, 0);
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
    }
    private bool CheckConditionToStop()
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
        return false;
    }
    public void Reset()
    {
        IsMoving = false;
        _actualSegmentId = 0;
        _rb.position = new Vector2(0f, _rb.position.y);
        _rb.velocity = Vector2.zero;

    }

}
[Serializable]
public class Segmento
{
    public List<Dato> datos = new();
}
