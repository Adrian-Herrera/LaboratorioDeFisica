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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dataCar.vel = 0;
        onPlay = false; firstUse = true;
        // data = new float[2, 5];

        // data[0, 0] = vel_inicial;
        // data[0, 1] = vel_final;
        // data[0, 2] = dist_final;
        // data[0, 3] = acceleration;
        // data[0, 4] = tiempo;

        // data[1, 0] = vel_inicial;
        // data[1, 1] = vel_final;
        // data[1, 2] = dist_final;
        // data[1, 3] = acceleration;
        // data[1, 4] = tiempo;

    }

    void Update()
    {
        rb.velocity = new Vector2(dataCar.vel, 0);

    }
    void FixedUpdate()
    {

    }

    // public void Play()
    // {
    //     if (firstUse)
    //     {
    //         getData();
    //         firstUse = false;
    //     }
    //     onPlay = true;

    // }
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
