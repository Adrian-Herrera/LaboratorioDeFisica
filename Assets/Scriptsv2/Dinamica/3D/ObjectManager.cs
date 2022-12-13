using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private TMP_Text _distanceTxt;
    [SerializeField] private Button _addForceButton;
    [SerializeField] private Button _stopMovementButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Slider _degreeSlider;
    [SerializeField] private TMP_InputField _pushForceInput;
    [SerializeField] private TMP_InputField _timeLimitInput;
    [SerializeField] private TMP_InputField _frictionInput;
    [Header("Objects")]
    [SerializeField] private GameObject _ground1;
    [SerializeField] private GameObject _cube1;
    [Header("Variables")]
    [SerializeField] private float _groungDegrees;
    [SerializeField] private int _timeBeforeStop = 2000;
    [SerializeField] private float _pushForce = 1;
    [Range(0f, 1f)]
    [SerializeField] private float _friction;
    [SerializeField] private float _acc;
    private bool _onMovement;
    private float _timer;
    private Vector3 _startPosition;
    private float _distance;
    private void Start()
    {
        // _testButton.onClick.AddListener(SendMessage);
        _degreeSlider.onValueChanged.AddListener(ChangeDegree);
        _addForceButton.onClick.AddListener(AddObjectForce);
        _stopMovementButton.onClick.AddListener(StopObjectMovement);
        _resetButton.onClick.AddListener(ResetObject);
        _pushForceInput.onValueChanged.AddListener((s) => { _pushForce = float.Parse(s); });
        _timeLimitInput.onValueChanged.AddListener((s) => { _timeBeforeStop = int.Parse(s) * 1000; });
        _frictionInput.onValueChanged.AddListener((s) => { _friction = float.Parse(s); });
        _startPosition = _cube1.transform.position;
        _groungDegrees = 0;
        // _timeBeforeStop = 0;
        // _pushForce = 1;
    }
    private void FixedUpdate()
    {
        // Vector3 newDegree = Vector3.Lerp(_ground1.transform.eulerAngles, new Vector3(0, 0, _groungDegrees < 0 ? _groungDegrees + 360 : _groungDegrees), Time.deltaTime * 10f);
        _ground1.transform.eulerAngles = new Vector3(0, 0, _groungDegrees);
        if (_onMovement)
        {
            _timer += 10;
            _timerTxt.text = "Tiempo: " + (_timer / 1000).ToString() + " s";
            if (_timeBeforeStop > 0 && _timer >= _timeBeforeStop)
            {
                StopObjectMovement();
            }
            else
            {
                _cube1.GetComponent<Rigidbody>().AddForce(new Vector3(TotalForce(), 0, 0));
                // Debug.Log(_cube1.GetComponent<Rigidbody>().velocity);
            }
        }
    }
    public void ChangeDegree(float newValue)
    {
        _groungDegrees = newValue;
    }
    private void AddObjectForce()
    {

        _onMovement = true;
    }
    private float TotalForce()
    {
        float Normal = _cube1.GetComponent<Rigidbody>().mass * 10;
        float ForceFriction = _friction * Normal;
        _acc = (_pushForce - ForceFriction) / _cube1.GetComponent<Rigidbody>().mass;
        return _pushForce - ForceFriction;

    }
    private void StopObjectMovement()
    {
        // _cube1.GetComponent<ConstantForce>().force = Vector3.zero;
        _onMovement = false;
        _cube1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _distance = Vector3.Distance(_cube1.transform.position, _startPosition);
        _distanceTxt.text = "Dist: " + (Mathf.Round(_distance * 100) / 100).ToString() + " mts";
    }
    private void ResetObject()
    {
        _cube1.transform.position = _startPosition;
        _timer = 0;
        _timerTxt.text = "Tiempo: " + (_timer / 1000).ToString() + " s";
    }
}
