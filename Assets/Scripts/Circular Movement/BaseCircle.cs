using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseCircle : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private int _vertexNumber = 120;
    private float _radius = 3;

    public GameObject Panel;
    public GameObject Center;
    public GameObject Ball;
    public TMP_InputField[] UIData;

    private float desp_i, desp_f;
    private float vel_i, vel_f;
    private float acc_i, acc_f;
    private float tiempo_i, tiempo_f;

    private bool onMove = false;

    private Vector3 centerPos = new Vector3(3f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        Pause();
        // Draw();
        lineRenderer = GetComponent<LineRenderer>();
        DrawPolygon(_vertexNumber, _radius, centerPos, 0.15f, 0.15f);

        UIData = Panel.GetComponentsInChildren<TMP_InputField>();

        /*

        [0] = desp_i [1] = desp_f
        [2] = vel_i [3] = vel_f
        [4] = acc_i [5] = acc_f
        [6] = Tiempo_i [7] = Tiempo_f

        */

    }

    void Update()
    {
        if (onMove && tiempo_i < 3 - 0.02)
        {

            
        }
        else
        {

            // Debug.Log(tiempo_i);
            // Debug.Log(vel_f);
            onMove = false;
            Pause();


        }


    }
    void FixedUpdate()
    {
        if (onMove && tiempo_i < 3 - 0.02)
        {
            Vel();
            TimeCounter();

        }

    }

    public void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }

    public void Draw()
    {
        DrawPolygon(_vertexNumber, _radius, centerPos, 0.2f, 0.2f);
    }


    public void Play()
    {
        getValues();


        onMove = true;
        Time.timeScale = 1;

    }

    public void Pause()
    {
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
        Center.transform.rotation = new Quaternion(0, 0, 0, 1);
        vel_f = vel_i;
        Center.GetComponent<Rigidbody2D>().angularVelocity = vel_i;
        tiempo_i = 0;
        UIData[3].text = "";
        UIData[6].text = "";

    }

    public void getValues()
    {

        desp_i = UIData[0].text != "" ? float.Parse(UIData[0].text) : 0;
        desp_f = UIData[1].text != "" ? float.Parse(UIData[1].text) : 0;
        vel_i = UIData[2].text != "" ? float.Parse(UIData[2].text) : 0;
        vel_f = UIData[3].text != "" ? float.Parse(UIData[3].text) : vel_i;
        acc_i = UIData[4].text != "" ? float.Parse(UIData[4].text) : 0;
        acc_f = UIData[5].text != "" ? float.Parse(UIData[5].text) : 0;
        tiempo_i = UIData[6].text != "" ? float.Parse(UIData[6].text) : 0;
        tiempo_f = UIData[7].text != "" ? float.Parse(UIData[7].text) : 0;

    }

    public void Vel()
    {

        // Center.transform.Rotate(new Vector3(0, 0, 1), vel_f * Time.deltaTime);

        Center.GetComponent<Rigidbody2D>().angularVelocity = vel_f;
        Acc();
        UIData[3].text = vel_f.ToString();

    }

    public void Acc()
    {
        if (acc_i != 0)
        {
            vel_f += acc_i * Time.deltaTime;
            // UIData[3].text = vel_f.ToString();
        }
    }

    public void TimeCounter()
    {

        tiempo_i += Time.deltaTime;
        Debug.Log(tiempo_i);
        UIData[6].text = tiempo_i.ToString();

    }
}
