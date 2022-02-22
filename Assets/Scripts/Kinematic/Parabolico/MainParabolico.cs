using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainParabolico : MonoBehaviour
{
    public TMP_InputField[] UIData;
    public GameObject Panel, startPoint, ball;
    private float vfx, vox, vfy, voy, x, y, grav = 9.81f, tiempo;
    private float altura_base, tiempo_subida, altura_max;

    public TMP_Text Info_time, info_altura, info_vel_f;

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

        rb = ball.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (onMove && tiempo < 6)
        {

            tiempo += Time.deltaTime;
            tiempo = Round2Digits(tiempo);

            rb.velocity = new Vector2(vox, (voy - (grav * tiempo)));
            // vel_f = Round2Digits(vel_f);

            y = altura_base + (voy * tiempo) - ((grav * Mathf.Pow(tiempo, 2)) / 2);
            x = vox * tiempo;
            ball.transform.localPosition = new Vector3(x, y, 0);
            // desp_i = Round2Digits(desp_i);

            altura_max = (Mathf.Pow(voy, 2) / 2 * grav);

            // info_vel_f.text = "Vel:" + rb.velocity.y.ToString();
            // info_altura.text = "Altura: " + desp.ToString();

            // info_tiempo.text = "Tiempo: " + tiempo.ToString();

            Info_time.text = "Tiempo: " + tiempo;
        }
        else
        {
            if (onMove)
            {

                var vel = Mathf.Sqrt(2 * grav * (altura_base + altura_max));
                info_altura.text = "Altura: 0";
                info_vel_f.text = "Vel: " + Round2Digits(vel);  // + altura maxima???
                UIData[2].text = altura_max.ToString("F3");
                rb.velocity = new Vector2(0, 0);
                // ball.transform.localPosition = new Vector3(0, 0, 0);

                altura_max = (Mathf.Pow(voy, 2) / (2 * grav));
                Debug.Log("vo^2 " + Mathf.Pow(voy, 2) + " (2 * acc) " + (2 * grav) + " acc " + grav);
                tiempo_subida = voy / grav;
                Debug.Log("Altura max: " + altura_max + " en segundos: " + tiempo_subida);

                Info_time.text = "Tiempo: " + (Round2Digits(vel / grav) + tiempo_subida);
                Time.timeScale = 0;
                Pause();
                onMove = false;
            }
        }
    }

    public void getValues()
    {
        vox = UIData[0].text != "" ? float.Parse(UIData[0].text) : 0;
        voy = UIData[1].text != "" ? float.Parse(UIData[1].text) : 0;
        altura_base = UIData[2].text != "" ? float.Parse(UIData[2].text) : 0;
    }
    public void Play()
    {
        getValues();


        startPoint.transform.localPosition = new Vector3(0, altura_base, 0);
        ball.transform.localPosition = startPoint.transform.localPosition;

        rb.velocity = new Vector2(vox, voy);

        onMove = true;
        Time.timeScale = 0.5f;
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

    }

    public float Round2Digits(float number)
    {
        return Mathf.Round(number * 1000f) / 1000f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        Pause();
    }
}
