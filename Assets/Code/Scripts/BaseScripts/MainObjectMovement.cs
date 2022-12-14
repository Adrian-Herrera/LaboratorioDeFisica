using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainObjectMovement : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    
}
