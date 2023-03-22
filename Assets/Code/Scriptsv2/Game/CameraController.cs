using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private GameObject _player;
    [SerializeField] private ButtonEvents _rotateRight, _rotateLeft;
    [SerializeField] private float distance = 3f;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private int _selectIndex;
    private Transform _actualTarget;
    private Quaternion _originalTargetRotation;
    float rotateDir = 0f;
    float rotateSpeed = 40f;
    private bool _isRotate = false;
    private void Start()
    {
        Init();
        _rotateRight.onDownEvent = RotateRight;
        _rotateRight.onUpEvent = () => { _isRotate = false; };

        _rotateLeft.onDownEvent = RotateLeft;
        _rotateLeft.onUpEvent = () => { _isRotate = false; };
    }
    private void Update()
    {
        if (_isRotate)
        {
            _actualTarget.transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
        }
    }
    private void Init()
    {
        _selectIndex = 0;
        _originalTargetRotation = _targets[_selectIndex].transform.localRotation;
        _actualTarget = _targets[_selectIndex];
        ChangeTarget(_selectIndex);
    }
    private void ChangeTarget(int value)
    {

        _actualTarget.transform.localRotation = _originalTargetRotation;

        _selectIndex = value;
        _actualTarget = _targets[_selectIndex];
        ChangeFollow(_actualTarget);
        ChangeLookAt(_actualTarget);
        _originalTargetRotation = _actualTarget.transform.localRotation;
        Debug.Log(_player.transform.position);
        Debug.Log(_actualTarget.position);
        _player.transform.position = _actualTarget.position;
    }
    private void ChangeFollow(Transform target)
    {
        _virtualCamera.Follow = target;
    }
    private void ChangeLookAt(Transform target)
    {
        _virtualCamera.LookAt = target;
    }
    public void NextTarget()
    {
        if (_selectIndex < _targets.Length - 1)
        {
            _selectIndex++;
        }
        else
        {
            _selectIndex = 0;
        }
        ChangeTarget(_selectIndex);
    }
    public void PreviousTarget()
    {
        if (_selectIndex > 0)
        {
            _selectIndex--;
        }
        else
        {
            _selectIndex = _targets.Length - 1;
        }
        ChangeTarget(_selectIndex);
    }
    public void RotateRight()
    {
        rotateDir = -1f;
        _isRotate = true;
    }
    public void RotateLeft()
    {
        rotateDir = 1f;
        _isRotate = true;
    }
}
