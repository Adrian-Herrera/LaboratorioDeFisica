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

    public soCar soCar;
    enum DataType
    {
        Vo, Vf, Acc, Time, Xo, Xf
    }
     
    public bool onPlay, firstUse;

    private Rigidbody2D rb;

    private Transform startPoint, endPoint;

    private float distance;
    public float s_vel, s_acc, s_dist; //Valores para el sprite

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        s_vel = 0;
        onPlay = false;
        firstUse = true;

        startPoint = transform;
        endPoint = transform.GetChild(0);
        distance = Vector3.Distance(endPoint.position, transform.position);
        s_dist = distance;

    }

    void Update()
    {
        rb.velocity = new Vector2(s_vel, 0);

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
        soCar.Play();
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

    // Funciones para el movimiento del sprite

    private void setVelocity(int SegmentNumber)
    {
        var percent = 0f;
        if (soCar.dataTable[SegmentNumber, 5] != 0)
        {
            percent = ((s_dist * 100f) / soCar.dataTable[SegmentNumber, 5]) / 100f;
            // percent = ((dataTable[SegmentNumber, 5] * 100f) / x_total) / 100f;
        }
        s_vel = soCar.dataTable[SegmentNumber, 0] * percent;
        s_acc = soCar.dataTable[SegmentNumber, 2] * percent;
    }


}
