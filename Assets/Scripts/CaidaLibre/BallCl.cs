using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCl : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            Debug.Log("Vel Nivel Base " + rb.velocity);
        }
    }
}
