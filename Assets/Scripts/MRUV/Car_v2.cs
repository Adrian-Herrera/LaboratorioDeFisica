using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car_v2 : MonoBehaviour
{

    // [SerializeField] private TMP_InputField data_vel_inicial = null;
    // [SerializeField] private TMP_InputField data_dist_inicial = null;
    // [SerializeField] private TMP_InputField data_acceleration = null;
    // [SerializeField] private TMP_InputField data_tiempo_espera = null;

    public soCar dataCar;
    public bool onPlay, firstUse;

    private Rigidbody2D rb;

    private Transform startPoint, endPoint;

    private float distance;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dataCar.vel = 0;
        onPlay = false;
        firstUse = true;

        startPoint = transform;
        endPoint = transform.GetChild(0);
        distance = Vector3.Distance(endPoint.position, transform.position);
        dataCar.dist = distance;

    }

    void Update()
    {
        rb.velocity = new Vector2(dataCar.vel, 0);

    }
    void FixedUpdate()
    {

    }

    public void Play()
    {
        if (firstUse)
        {
            firstUse = false;
            

        }
        dataCar.Play();
        onPlay = true;

    }
    public void Pause()
    {
        onPlay = false;
        rb.velocity = new Vector2(0, 0);
    }



    private float checkText(string s)
    {
        return s == "" ? 0 : float.Parse(s);
    }


}
