using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car : MonoBehaviour
{
    [SerializeField] private CarSO carSO;
    [SerializeField] private TMP_Text TimerText;
    private Rigidbody2D _rb;
    private float Vel, Velf, Acc, Timer, Distance, SegmentDistance;
    private float TimeUntilStop;
    private int actualSegment;
    private bool playing;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        actualSegment = 0;
        SegmentDistance = 0;
        playing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimerText.text = Timer.ToString();
        if (StateManager.Current.ActualState == StateManager.states.Playing)
        {
            if (actualSegment < carSO.numberOfSegments)
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
                    }
                }
                else if (carSO.Datos[actualSegment, 3].status)
                {
                    if (_rb.position.x - SegmentDistance <= Distance)
                    {
                        Vel += (Acc * Time.deltaTime);
                        _rb.velocity = new Vector2(Vel, 0f);
                    }
                    else
                    {
                        actualSegment++;
                        getInfo(actualSegment);
                    }
                }
            }
            else
            {
                _rb.velocity = new Vector2(0f, 0f);

            }
        }
    }

    private void getInfo(int segment) 
    {
        if (!(actualSegment < carSO.numberOfSegments)) return;
        Debug.Log("getInfo segment: " + segment);
        Timer = 0;
        Vel = carSO.Datos[segment, 0].value;
        Velf = carSO.Datos[segment, 1].value;
        Acc = carSO.Datos[segment, 2].value;
        Distance = carSO.Datos[segment, 3].value;
        TimeUntilStop = carSO.Datos[segment, 4].value;

        SegmentDistance = _rb.position.x;
    }
    public void PlayCar()
    {
        StateManager.Current.ActualState = StateManager.states.Playing;
        getInfo(actualSegment);
    }
    public void Reset()
    {
        StateManager.Current.ActualState = StateManager.states.Stop;
        transform.position = Vector3.zero;
        actualSegment = 0;
    }
}
