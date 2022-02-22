using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car : BasePoint
{
    protected override void getInfo(int segment)
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
    protected override void move()
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
}
