using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaidaLibre : MonoBehaviour
{
    public TMP_InputField[] UIData;
    public Toggle[] Direccion;
    public GameObject Panel, startPoint, ball;
    private float vel_i, vel_f, altura_base, altura_max, tiempo;

    private float acc = 9.81f, desp, tiempo_subida;

    public TMP_Text info_vel_f, info_altura, info_tiempo;

    private bool onMove = false;

    private Rigidbody2D rb;

    void Awake()
    {
        Time.timeScale = 0;
    }
    void Start()
    {
        UIData = Panel.GetComponentsInChildren<TMP_InputField>();
        /*
            UIData[0] = vel_i
            UIData[1] = Altura Base
            UIData[2] = Altura Maxima
        */
        Direccion = Panel.GetComponentsInChildren<Toggle>();
        // 0. Arriba 1. Abajo

        rb = ball.GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (onMove && desp >= 0)
        {

            tiempo += Time.deltaTime;
            tiempo = Round2Digits(tiempo);

            rb.velocity = new Vector2(0, (vel_i - (acc * tiempo)));
            // vel_f = Round2Digits(vel_f);

            desp = altura_base + (vel_i * tiempo) - ((acc * Mathf.Pow(tiempo, 2)) / 2);
            ball.transform.localPosition = new Vector3(0, desp, 0);
            // desp_i = Round2Digits(desp_i);

            if (desp < 50)
            {
                Time.timeScale = 0.5f;
            }

            altura_max = (Mathf.Pow(vel_i, 2) / (2 * acc));

            info_altura.text = "Altura: " + desp.ToString("F3");
            info_vel_f.text = "Vel:" + rb.velocity.y.ToString("F3");
            info_tiempo.text = "Tiempo: " + tiempo.ToString("F3");

        }
        else
        {
            if (onMove)
            {
                var vel = Mathf.Sqrt(2 * acc * (altura_base + altura_max));
                info_altura.text = "Altura: 0";
                info_vel_f.text = "Vel: " + Round2Digits(vel);  // + altura maxima???
                UIData[2].text = altura_max.ToString("F3");
                rb.velocity = new Vector2(0, 0);
                // ball.transform.localPosition = new Vector3(0, 0, 0);

                altura_max = (Mathf.Pow(vel_i, 2) / (2 * acc));
                Debug.Log("vo^2 " + Mathf.Pow(vel_i, 2) + " (2 * acc) " + (2 * acc) + " acc " + acc);
                tiempo_subida = vel_i / acc;
                Debug.Log("Altura max: " + altura_max + " en segundos: " + tiempo_subida);

                info_tiempo.text = "Tiempo: " + (Round2Digits(vel / acc) + tiempo_subida);
                Time.timeScale = 0;
                Pause();
                onMove = false;

            }
        }
    }


    public void getValues()
    {
        vel_i = UIData[0].text != "" ? float.Parse(UIData[0].text) : 0;
        altura_base = UIData[1].text != "" ? float.Parse(UIData[1].text) : 0;
        // altura_max = UIData[2].text != "" ? float.Parse(UIData[2].text) : 0;
    }
    public void Play()
    {
        getValues();


        startPoint.transform.localPosition = new Vector3(0, altura_base, 0);
        ball.transform.localPosition = startPoint.transform.localPosition;

        rb.velocity = new Vector2(0, vel_i);

        onMove = true;
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Debug.Log("Time pause");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Reset()
    {
        onMove = false;
        Time.timeScale = 0;

        tiempo = 0;
        ball.transform.localPosition = startPoint.transform.localPosition;
        vel_i = 0;
        rb.velocity = new Vector2(0, 0);
        desp = 0;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entro");

        // Time.timeScale = 0;




    }

    public float Round2Digits(float number)
    {
        return Mathf.Round(number * 1000f) / 1000f;
    }

    private void RestartBall()
    {
        rb.velocity = new Vector2(0, 0);
        rb.position = new Vector2(-13, 0);

    }
}
