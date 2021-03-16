using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car_v2 : MonoBehaviour
{

    [SerializeField] private TMP_InputField data_vel_inicial = null;
    [SerializeField] private TMP_InputField data_dist_inicial = null;
    [SerializeField] private TMP_InputField data_acceleration = null;
    // [SerializeField] private TMP_InputField data_tiempo_espera = null;

    public float vel_inicial, vel_final;
    public float dist_inicial, dist_final;
    public float acceleration, tiempo;

    public bool onPlay, firstUse;

    public float[,] data;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        vel_inicial = 0; vel_final = 0;
        dist_inicial = 0; dist_final = 0;
        acceleration = 0; tiempo = 0;

        onPlay = false; firstUse = true;
        data = new float[2, 5];

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
        if (onPlay)
        {
            Play();
            setData();
        }

    }
    void FixedUpdate()
    {
        if (onPlay)
        {
            tiempo += Time.deltaTime;
        }
    }

    public void getData()
    {
        vel_inicial = checkText(data_vel_inicial.text);
        dist_inicial = checkText(data_dist_inicial.text);
        acceleration = checkText(data_acceleration.text);
    }
    public void setData()
    {
        data_vel_inicial.text = vel_inicial.ToString();
        data_dist_inicial.text = dist_inicial.ToString();
        data_acceleration.text = acceleration.ToString();
    }

    public void Play()
    {
        if (firstUse)
        {
            getData();
            firstUse = false;
        }
        onPlay = true;
        rb.velocity = new Vector2(VelFinalTime(), 0);
    }
    public void Pause()
    {
        onPlay = false;
        rb.velocity = new Vector2(0, 0);
    }

    public float VelFinalDist()
    {
        return vel_final = Mathf.Pow(vel_inicial, 2) + 2 * acceleration * dist_inicial;
    }

    public float VelFinalTime()
    {
        return vel_final = vel_inicial + acceleration * tiempo;
    }

    public float DistFinal()
    {
        return dist_final = vel_inicial * tiempo + ((acceleration * Mathf.Pow(tiempo, 2)) / 2);
    }

    private float checkText(string s)
    {
        return s == "" ? 0 : float.Parse(s);
    }


}
