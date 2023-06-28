using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class MainObject : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private InterestPoint _interestPoint;
    [SerializeField] private GameObject _worldCanvas;
    private Sprite _mainObject;
    private Transform _transform;
    public Segmento[] _segmentos = new Segmento[3];
    public int _activeSegments = 1;
    public bool IsMoving = false;
    private Rigidbody2D _rb;
    private float Vel, _segmentDistance = 0, _segmentTimer = 0, _segmentStart, _alturaInicial;
    public float? Velo, Velf, Acc, Timer, Distance, Velx;
    public Vector3 maxHeight;
    public List<InterestPoint> InterestPoints = new();
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
    private void FixedUpdate()
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
        else if (LevelManager.Instance.temaId == 3 || LevelManager.Instance.temaId == 4)
        {
            _rb.velocity = new Vector2(Velx ?? 1, Vel);
            if (Acc.HasValue) Vel += Acc.Value * Time.deltaTime;
            _segmentTimer += Time.deltaTime;
            // _segmentDistance = _rb.position.x - _segmentStart;
            if (CheckConditionToStop())
            {
                // StopCoroutine(DrawDots());
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
                    Velf = MathF.Abs(_rb.velocity.y);
                    CreateInterestPoint(maxHeight, _transform.localScale.x, "Maxima altura alcanzada: " + maxHeight.y.ToString("F2") + " m");
                    CreateInterestPoint(transform.position, _transform.localScale.x, "Velocidad final alcanzada: " + Velf.Value.ToString("F2") + "m/s");
                    _rb.velocity = Vector2.zero;
                    Playground.Instance.DrawLinesWhenStop();
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
        Dato dt = _segmentos[segmentId].datos.Find(dato => dato.TipoVariableId == variableId);
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
        print("MainObject change scale");
        _transform.localScale = new Vector3(scale, scale, 1);
    }
    public void StartMovement()
    {

        Velo = _segmentos[_actualSegmentId].datos.Find(dato => dato.TipoVariableId == 4)?.Valor ?? 0f;
        Velf = _segmentos[_actualSegmentId].datos.Find(dato => dato.TipoVariableId == 5)?.Valor;
        Acc = _segmentos[_actualSegmentId].datos.Find(dato => dato.TipoVariableId == 6)?.Valor ?? 0f;
        Timer = _segmentos[_actualSegmentId].datos.Find(dato => dato.TipoVariableId == 3)?.Valor;
        Distance = _segmentos[_actualSegmentId].datos.Find(dato => dato.TipoVariableId == 2)?.Valor;
        Velx = _segmentos[_actualSegmentId].datos.Find(dato => dato.TipoVariableId == 1)?.Valor;

        Vel = Velo.Value;
        _segmentStart = _rb.position.x;
        _segmentDistance = 0;
        _segmentTimer = 0;
        _alturaInicial = transform.position.y;
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
            if (LevelManager.Instance.temaId == 3 || LevelManager.Instance.temaId == 4)
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
        _lineRenderer.positionCount = 0;
        // _lineRenderer.SetPositions(_dotsList.ToArray());
        Dato dato;
        switch (LevelManager.Instance.temaId)
        {
            case 2:
                _rb.position = new Vector2(0f, _rb.position.y);
                break;
            case 3:
                Helpers.ClearListContent(InterestPoints);
                dato = SimulatorManager._selectedObject._segmentos[0].datos.Find(dato => dato.TipoVariableId == 12);
                if (dato != null && dato.Valor >= 0)
                {
                    _rb.position = new Vector2(0f, 0f + dato.Valor);
                }
                else
                {
                    _rb.position = new Vector2(0f, 0f);
                }

                break;
            case 4:
                Helpers.ClearListContent(InterestPoints);
                dato = SimulatorManager._selectedObject._segmentos[0].datos.Find(dato => dato.TipoVariableId == 12);
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
    private void CreateInterestPoint(Vector3 pos, float scale, string message)
    {
        InterestPoints.Add(Instantiate(_interestPoint.gameObject, _worldCanvas.transform).GetComponent<InterestPoint>().CreatePoint(pos, scale, message));
    }

    private IEnumerator DrawDots()
    {
        maxHeight = Vector3.zero;
        while (IsMoving)
        {
            _dotsList.Add(transform.position);
            if (transform.position.y > maxHeight.y) maxHeight = transform.position;
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
