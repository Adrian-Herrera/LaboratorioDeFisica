using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3d : MonoBehaviour
{
    [Header("Variables")]
    public VariableUnity _masa;
    [Header("Fuerzas")]
    public VariableUnity _tension;
    public VariableUnity _peso;
    public VariableUnity _normal;
    public float Masa
    {
        get { return _masa.Value; }
        set
        {
            _masa.Value = value;
            _peso.Value = _masa.Value * 10;
        }
    }
    [Header("Datos")]
    public VariableUnity _acc;
    public VariableUnity _vel;
    [SerializeField] private float _yForces;
    [SerializeField] private bool _debug = false;
    public bool _onGround;
    private Rigidbody _rb;
    [SerializeField] private bool _stopMovement;
    public bool StopMovement
    {
        get { return _stopMovement; }
        set
        {
            _stopMovement = value;
            if (value)
            {
                Debug.Log(gameObject.name + " stop: " + value);
                _rb.isKinematic = true;
            }
            else
            {
                Debug.Log(gameObject.name + " stop: " + value);
                _rb.isKinematic = false;
                CalculateAcc();
            }
        }
    }
    private float _tempMasa, _tempTension;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _masa = new(BaseVariable.Masa, 1);
        _tension = new(BaseVariable.Tension);
        _peso = new(BaseVariable.Peso);
        _normal = new(BaseVariable.Normal);
        _acc = new(BaseVariable.Aceleracion);
        _vel = new(BaseVariable.Velocidad, 0);
        _onGround = false;
        // _vel = 0;
    }
    private void FixedUpdate()
    {
        if (_masa.Value != _tempMasa)
        {
            if (_tension.Value != _tempTension)
            {
                _tempMasa = _masa.Value;
                _tempTension = _tension.Value;
            }
            else
            {
                return;
            }
        }

        if (_acc.Value != 0)
        {
            _vel.Value = _acc.Value * Time.fixedDeltaTime;
            // _rb.AddForce(0, _acc, 0, ForceMode.Acceleration);
            // _rb.AddForce(_acc * _masa * Vector3.down);
            _rb.velocity += new Vector3(0, _vel.Value, 0);
            if (_debug)
            {
                Debug.Log(gameObject.name + " velocity: " + _rb.velocity + " onGround: " + _onGround);
            }
        }
        else
        {
            // Debug.Log(transform.name + " last vel: " + _rb.velocity);
            _rb.velocity = Vector3.zero;
        }

        // _yForces.SumForces();
    }
    public void SetTension(float newTension)
    {
        _tension.Value = newTension;
        _onGround = false;
        CalculateAcc();
    }
    private float CalculateNormal()
    {
        float normal;
        if (_onGround)
        {
            normal = _peso.Value - _tension.Value;
            if (normal < 0) normal = 0;
        }
        else
        {
            normal = 0;
        }
        return normal;
    }
    private void CalculateAcc()
    {
        _yForces = CalculateYForces();
        // if (StopMovement)
        // {
        //     _acc = 0;
        //     _rb.velocity = Vector3.zero;
        // }
        // else
        // {
        // }
        _acc.Value = Mathf.Round(_yForces / Masa * 100) / 100;
        if (_debug)
        {
            Debug.Log("acc: " + _acc);
        }
    }
    private float CalculateYForces()
    {
        float total;
        _peso.Value = Masa * 10;
        if (_onGround)
        {
            _normal.Value = CalculateNormal();
            total = _normal.Value + _tension.Value - _peso.Value;
            // return total;
        }
        else
        {
            total = _tension.Value - _peso.Value;
        }
        if (_debug)
        {
            Debug.Log("normal: " + _normal + " _tension: " + _tension + " _peso: " + _peso + " total: " + total);
        }
        return total;
    }
    private void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.CompareTag("Floor"))
        // {
        // }
        _onGround = true;
        _rb.velocity = Vector3.zero;
        // _normal = CalculateNormal();
        //  Debug.Log( ReturnDirection( collision.gameObject, this.gameObject ) );
    }
    private void OnCollisionExit(Collision other)
    {
        _onGround = false;
        // _normal = CalculateNormal();
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }
}
