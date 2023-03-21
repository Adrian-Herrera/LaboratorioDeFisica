using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] protected VariableUnity _masa;
    [Header("Fuerzas")]
    [SerializeField] protected VariableUnity _peso;
    [Header("Datos Movimiento")]
    [SerializeField] protected VariableUnity _acc;
    [SerializeField] protected VariableUnity _vel;
    [SerializeField] private bool _onGround;
    [SerializeField] private bool _onMove;
    [Header("Forces")]
    [SerializeField] private List<VariableUnity> _yForces = new();
    // [SerializeField] private List<VariableUnity> _xForces = new();
    private Rigidbody _rb;
    public event Action OnChangeMass;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public float Masa
    {
        get { return _masa.Value; }
        set
        {
            _masa.Value = value;
            _peso.Value = -_masa.Value * 10; // 10 es la gravedad
            OnChangeMass?.Invoke();
        }
    }
    public float Peso
    {
        get { return Mathf.Abs(_peso.Value); }
    }
    private void Start()
    {
        _masa = new(BaseVariable.Masa);
        _peso = new(BaseVariable.Peso);
        _acc = new(BaseVariable.Aceleracion);
        _vel = new(BaseVariable.Velocidad, 0);
        Masa = 1;
        _yForces.Add(_peso);
        _onGround = false;
        _onMove = false;
    }
    private void FixedUpdate()
    {
        if (!_onMove) return;
        _rb.AddRelativeForce(new Vector3(0, -_acc.Value, 0), ForceMode.Acceleration);
    }
    public void StartMovement()
    {
        _onMove = true;
    }
    public void AddYForce(VariableUnity newForce)
    {
        VariableUnity oldforce = _yForces.Find(e => e.TipoVariable == newForce.TipoVariable);
        if (oldforce == null)
        {
            _yForces.Add(newForce);
        }
        else
        {
            oldforce.Value = newForce.Value;
        }
        CalculateAcc();
    }
    private void CalculateAcc()
    {
        float yTotalForces = SumForces(_yForces);
        _acc.Value = Mathf.Round(yTotalForces / Masa * 100) / 100;
    }
    private float SumForces(List<VariableUnity> listForces)
    {
        float total = 0;
        foreach (VariableUnity force in listForces)
        {
            total += force.Value;
        }
        return total;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _onGround = true;
        }
        _onMove = false;
        _rb.velocity = Vector3.zero;
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _onGround = false;
        }
    }



}
