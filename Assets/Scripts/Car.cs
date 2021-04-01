using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public soCar dataCar;

    // private float time;

    private Rigidbody2D rb;
    private LineRenderer lnr;
    private TextMesh sTxt, disTxt;

    private Vector3 startPoint, endPoint;
    private Vector2 carForce;

    private bool onMove = false, instance = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // cf.force = new Vector2(2,0);
        // startPoint = this.transform.position;
        addLine();
        speedTxt();
        // time = 0;
        // dataCar.timeMov = 0;
        // Debug.Log("Velocidad " + rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (onMove)
        {
            updateLine();
            updateSpeedText();

            // time += Time.deltaTime;

        }
    }
    void FixedUpdate()
    {
        if (onMove)
        {
            // dataCar.speed += dataCar.acceleration * Time.deltaTime;
            // rb.velocity = new Vector2(dataCar.speed, 0);

            // dataCar.timeMov += Time.deltaTime;
        }
    }



    public void Begin()
    {
        if (!instance)
        {

            startPoint = this.transform.position;
            addLine();
            speedTxt();
            instance = true;
        }

        // rb.velocity = new Vector2(dataCar.speed, 0);
        // carForce = new Vector2(dataCar.acceleration, 0);

        // dataCar.onMove = true;
        onMove = true;
    }
    public void Pause()
    {
        rb.velocity = new Vector2(0, 0);
        carForce = new Vector2(0f, 0f);

        // dataCar.onMove = false;
        onMove = false;
    }
    public void Restart()
    {
        this.transform.position = startPoint;
        rb.velocity = new Vector2(0, 0);
        carForce = new Vector2(0f, 0f);

        lnr.SetPosition(1, this.transform.position + new Vector3(0f, 1f, 0f));
        endPoint = this.transform.position;

        // dataCar.timeMov = 0;
        // dataCar.onMove = false;
        onMove = false;
    }

    private void addLine()
    {
        GameObject arrow = new GameObject("CarDistance");
        arrow.transform.SetParent(this.transform);
        lnr = arrow.AddComponent<LineRenderer>();
        lnr.positionCount = 2;
        lnr.startWidth = 0.2f;
        lnr.SetPosition(0, this.transform.position + new Vector3(0f, 1f, 0f));
        lnr.SetPosition(1, this.transform.position + new Vector3(0f, 1f, 0f));

        GameObject dist = new GameObject("CarDistText");
        dist.transform.SetParent(this.transform);
        disTxt = dist.AddComponent<TextMesh>();
        disTxt.anchor = TextAnchor.LowerCenter;
        disTxt.characterSize = 0.5f;


    }

    private void updateLine()
    {

        lnr.SetPosition(1, this.transform.position + new Vector3(0f, 1f, 0f));
        endPoint = this.transform.position;
        float distance = round(Vector3.Distance(startPoint, endPoint));
        // Debug.Log(Vector3.Distance(startPoint, endPoint) + " -> " + distance);
        disTxt.text = distance.ToString();
        disTxt.transform.position = ((startPoint + endPoint) / 2) + new Vector3(0f, 1f, 0f);

    }

    private void speedTxt()
    {
        GameObject speedTxt = new GameObject("Speed");
        speedTxt.transform.SetParent(this.transform);
        speedTxt.transform.localPosition = new Vector3(0f, 1f, 0f);
        sTxt = speedTxt.AddComponent<TextMesh>();
        sTxt.anchor = TextAnchor.LowerCenter;
        sTxt.characterSize = 0.5f;

    }

    private void updateSpeedText()
    {
        sTxt.text = rb.velocity.x.ToString("F2") + " mts";
    }

    //Devuelve solo dos decimales de un float
    private float round(float f)
    {

        float rounded = Mathf.Round(f * 100f) / 100f;
        //Debug.Log(rounded);
        return rounded;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "End")
        {
            Pause();
        }
    }


}
