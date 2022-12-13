using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform _playerCameraTransform;
    [SerializeField] private Transform _objectGrabPointTransform;
    [SerializeField] private LayerMask _pickUpLayerMask;
    [SerializeField] private Image _reticle;
    [SerializeField] private float _pickUpDistance;
    private StarterAssetsInputs _input;
    private ObjectGrabbable _objectGrabbable;
    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void Start()
    {
        _pickUpDistance = 8f;
        _reticle.color = new Color(1, 1, 1, 0.75f);
    }

    private void Update()
    {
        if (Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out RaycastHit hit, _pickUpDistance, _pickUpLayerMask))
        {
            _reticle.color = new Color(1, 0, 0, 0.75f);
        }
        else
        {
            _reticle.color = new Color(1, 1, 1, 0.75f);
        }
        if (_input.grab)
        {
            if (_objectGrabbable == null)
            {
                if (Physics.Raycast(_playerCameraTransform.position, _playerCameraTransform.forward, out RaycastHit raycastHit, _pickUpDistance, _pickUpLayerMask))
                {
                    // Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out _objectGrabbable))
                    {
                        _objectGrabbable.Grab(_objectGrabPointTransform);
                    }
                    if (raycastHit.transform.TryGetComponent(out IInteractable _IInteractable))
                    {
                        _IInteractable.Interact();
                    }
                    Debug.Log(_input.grab);
                }
            }
            else
            {
                _objectGrabbable.Drop();
                _objectGrabbable = null;
            }
            _input.grab = false;
        }
    }
}
