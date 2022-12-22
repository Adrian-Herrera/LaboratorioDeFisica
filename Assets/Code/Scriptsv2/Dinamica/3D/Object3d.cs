using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3d : MonoBehaviour
{
    [Header("Variables")]
    [Range(1, 10)]
    [SerializeField] private float _masa = 1;
    [Header("Fuerzas")]
    [SerializeField] private float _tension;
    [SerializeField] private float _peso;
    [SerializeField] private float _normal;
    public float Masa
    {
        get { return _masa; }
        set
        {
            _masa = value;
            _peso = _masa * 10;
        }
    }
    [Header("Datos")]
    [SerializeField] private float _acc;
    [SerializeField] private float _vel;
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
        Application.targetFrameRate = 0;
        _onGround = false;
        _vel = 0;
    }
    private void FixedUpdate()
    {
        if (_masa != _tempMasa)
        {
            if (_tension != _tempTension)
            {
                _tempMasa = _masa;
                _tempTension = _tension;
            }
            else
            {
                return;
            }
        }


        // _acc = StopMovement ? 0 : _yForces / Masa;

        // Debug.Log(_tension + " - " + _peso + " / " + Masa + " = " + _a);
        // _rb.velocity += new Vector3(0, _acc * Time.deltaTime, 0);
        // if (StopMovement)
        // {
        //     _rb.velocity = Vector3.zero;
        //     return;
        // }
        if (_acc != 0)
        {
            _vel = _acc * Time.fixedDeltaTime;
            // _rb.AddForce(0, _acc, 0, ForceMode.Acceleration);
            // _rb.AddForce(_acc * _masa * Vector3.down);
            _rb.velocity += new Vector3(0, _vel, 0);
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
        _tension = newTension;
        _onGround = false;
        CalculateAcc();
    }
    private float CalculateNormal()
    {
        float normal;
        if (_onGround)
        {
            normal = _peso - _tension;
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
        _acc = Mathf.Round(_yForces / Masa * 100) / 100;
        if (_debug)
        {
            Debug.Log("acc: " + _acc);
        }
    }
    private float CalculateYForces()
    {
        float total;
        _peso = Masa * 10;
        if (_onGround)
        {
            _normal = CalculateNormal();
            total = _normal + _tension - _peso;
            // return total;
        }
        else
        {
            total = _tension - _peso;
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
    // private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }
    //  private HitDirection ReturnDirection( GameObject Object, GameObject ObjectHit ){

    //      HitDirection hitDirection = HitDirection.None;
    //      RaycastHit MyRayHit;
    //      Vector3 direction = ( Object.transform.position - ObjectHit.transform.position ).normalized;
    //      Ray MyRay = new Ray( ObjectHit.transform.position, direction );

    //      if ( Physics.Raycast( MyRay, out MyRayHit ) ){

    //          if ( MyRayHit.collider != null ){

    //              Vector3 MyNormal = MyRayHit.normal;
    //              MyNormal = MyRayHit.transform.TransformDirection( MyNormal );

    //              if( MyNormal == MyRayHit.transform.up ){ hitDirection = HitDirection.Top; }
    //              if( MyNormal == -MyRayHit.transform.up ){ hitDirection = HitDirection.Bottom; }
    //              if( MyNormal == MyRayHit.transform.forward ){ hitDirection = HitDirection.Forward; }
    //              if( MyNormal == -MyRayHit.transform.forward ){ hitDirection = HitDirection.Back; }
    //              if( MyNormal == MyRayHit.transform.right ){ hitDirection = HitDirection.Right; }
    //              if( MyNormal == -MyRayHit.transform.right ){ hitDirection = HitDirection.Left; }
    //          }    
    //      }
    //      return hitDirection;
    //  }
}
public class Forces
{
    public float[] AllForces;
    public float TotalForce;
    public float SumForces()
    {
        TotalForce = 0;
        if (AllForces.Length == 0) return 0;
        foreach (float force in AllForces)
        {
            TotalForce += force;
        }
        return TotalForce;
    }
}
