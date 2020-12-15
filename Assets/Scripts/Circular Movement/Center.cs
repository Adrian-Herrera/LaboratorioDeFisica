using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Center : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Panel;
    public TMP_InputField[] UIData;

    public TMP_Text RPM;

    public TMP_Text Tiempo;

    private float desp;
    private float vel_i, vel_f;
    private float acc;
    private float tiempo_i, tiempo_f;

    private bool onMove = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Pause();

        UIData = Panel.GetComponentsInChildren<TMP_InputField>();

        /*

        [0] = desp 
        [1] = vel_i 
        [2] = vel_f
        [3] = acc_i 
        [4] = Tiempo_i 

        */

    }

    void Update()
    {

    }
    void FixedUpdate()
    {

        if (onMove && tiempo_i < tiempo_f)
        {

            tiempo_i += Time.deltaTime;
            tiempo_i = Round2Digits(tiempo_i);

            Formulary();


            UIData[0].text = desp.ToString("F2");
            UIData[2].text = vel_f.ToString("F2");

            Tiempo.text = tiempo_i.ToString("F2") + " S";
            RPM.text = (Round2Digits(vel_f * 60 / (2 * Mathf.PI))) + " RPM";


        }
        else
        {
            if (onMove)
            {

                Time.timeScale = 0;
                Pause();

                onMove = false;
            }
        }

    }

    public void Play()
    {
        getValues();



        onMove = true;
        Time.timeScale = 1;

    }

    public void Pause()
    {
        Debug.Log("Time pause");
        Time.timeScale = 0;
        // Debug.Log("Time Scale: 0");
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Reset()
    {
        onMove = false;
        Time.timeScale = 0;
        transform.rotation = new Quaternion(0, 0, 0, 1);
        // vel_f = vel_i;
        rb.angularVelocity = vel_i;
        tiempo_i = 0;
        UIData[0].text = "";
        // UIData[4].text = "";

    }

    public void getValues()
    {

        desp = UIData[0].text != "" ? float.Parse(UIData[0].text) : 0;
        vel_i = UIData[1].text != "" ? float.Parse(UIData[1].text) : 0;
        vel_f = UIData[2].text != "" ? float.Parse(UIData[2].text) : 0;
        acc = UIData[3].text != "" ? float.Parse(UIData[3].text) : 0;
        tiempo_f = UIData[4].text != "" ? float.Parse(UIData[4].text) : 0;

    }

    public void Formulary()
    {
        if (vel_i != 0 && acc != 0 && tiempo_f != 0)
        {
            Debug.Log("hola");
            vel_f = vel_i + (acc * tiempo_i);
            // vel_f = Round2Digits(vel_f);

            desp = (vel_i * tiempo_i) + ((acc * Mathf.Pow(tiempo_i, 2)) / 2);
            // desp_i = Round2Digits(desp_i);
            rb.rotation = desp;

        }
    }

    public float Round2Digits(float number)
    {
        return Mathf.Round(number * 1000f) / 1000f;
    }

}
