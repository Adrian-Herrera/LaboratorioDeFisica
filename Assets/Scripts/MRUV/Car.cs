using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private CarSO carSO;
    private float Vel, Velf, Acc, Timer, Distance;
    private float TimeUntilStop;
    private int actualSegment;

    private bool playing;

    [SerializeField]
    private TMP_Text TimerText;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        actualSegment = 0;
        playing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimerText.text = Timer.ToString();
        if (actualSegment < carSO.numberOfSegments && playing)
        {

            if (carSO.Datos[actualSegment, 4].status)
            {

                if (Timer <= TimeUntilStop)
                {
                    Vel += (Acc * Time.deltaTime);
                    _rb.velocity = new Vector2(Vel, 0f);
                    Timer += Time.deltaTime;
                }
                else
                {
                    actualSegment++;
                    getInfo(actualSegment);
                    // _rb.velocity = new Vector2(0f, 0f);

                }
            }
            else if (carSO.Datos[actualSegment, 1].status)
            {
                if (Vel <= Velf)
                {
                    Vel += (Acc * Time.deltaTime);
                    _rb.velocity = new Vector2(Vel, 0f);
                }
                else
                {
                    actualSegment++;
                    getInfo(actualSegment);
                    // _rb.velocity = new Vector2(0f, 0f);

                }
            }
            else if (carSO.Datos[actualSegment, 3].status)
            {
                if (_rb.position.x <= Distance)
                {
                    Vel += (Acc * Time.deltaTime);
                    _rb.velocity = new Vector2(Vel, 0f);
                }
                else
                {
                    actualSegment++;
                    getInfo(actualSegment);
                    // _rb.velocity = new Vector2(0f, 0f);

                }
            }
        }
        else
        {
            _rb.velocity = new Vector2(0f, 0f);

        }
    }

    private void getInfo(int segment) //play button
    {
        Timer = 0;
        Vel = carSO.Datos[segment, 0].value;
        Velf = carSO.Datos[segment, 1].value;
        Acc = carSO.Datos[segment, 2].value;
        Distance = carSO.Datos[segment, 3].value;
        TimeUntilStop = carSO.Datos[segment, 4].value;
    }
    public void PlayCar()
    {
        playing = true;
        getInfo(actualSegment);
    }

    private void conditionToStop(int segment)
    {
        for (int i = 0; i < carSO.Datos.GetLength(1); i++)
        {


        }
    }
}
