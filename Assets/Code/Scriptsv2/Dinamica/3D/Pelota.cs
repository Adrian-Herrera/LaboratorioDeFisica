using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private float _velX;
    [SerializeField] private float _velY;
    // Variables
    private Vector3 _initialPosition;
    [SerializeField] private List<Vector3> _points = new();
    private bool _isMoving = false;
    // IEnumerators
    private IEnumerator _IESavePoints;
    // Components
    private Rigidbody _rb;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private InterestLinePoint _interestPoint;
    [SerializeField] private List<InterestLinePoint> _interestPoints = new();
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _initialPosition = transform.position;
        _IESavePoints = SavePoints();
        DrawLine();
    }
    public void Reset()
    {
        transform.position = _initialPosition;
        Helpers.ClearListContent(_interestPoints);
    }
    public void Shoot()
    {
        _isMoving = true;
        _rb.AddForce(new Vector3(-_velX, _velY, 0), ForceMode.VelocityChange);
        _points.Clear();
        // if (_points.Count == 0)
        // {
        //     _points.Add(transform.position);
        // }
        StartCoroutine(_IESavePoints);
    }
    private void DrawLine()
    {
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
    }
    private void OnCollisionEnter()
    {
        _isMoving = false;
        _rb.velocity = Vector3.zero;
        StopCoroutine(_IESavePoints);
    }
    private IEnumerator SavePoints()
    {
        while (true)
        {
            _points.Add(transform.position);
            _lineRenderer.positionCount = _points.Count;
            _lineRenderer.SetPositions(_points.ToArray());
            InterestLinePoint point = Instantiate(_interestPoint);
            point.CreatePoint(transform.position, _rb.velocity, _points[0] - transform.position);
            Debug.Log(_initialPosition + " - " + transform.position + " = " + (_points[0] - transform.position));
            _interestPoints.Add(point);
            yield return new WaitForSeconds(.1f);

        }
    }
}
