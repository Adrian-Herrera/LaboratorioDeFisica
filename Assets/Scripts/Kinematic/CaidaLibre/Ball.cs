using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BasePoint
{
    [SerializeField] private GameObject arrow;
    protected override void getInfo(int segment)
    {
        if (actualSegment >= BasePointSO.numberOfSegments) return;
        Debug.Log("getInfo segment: " + segment);
        Timer = 0;
        Vel = BasePointSO.Datos[segment, 0].value;
        Velf = BasePointSO.Datos[segment, 1].value;
        Acc = BasePointSO.Datos[segment, 2].value;
        Distance = BasePointSO.Datos[segment, 3].value;
        TimeUntilStop = BasePointSO.Datos[segment, 4].value;

        SegmentDistance = _rb.position.y;
    }
    protected override void PreMove()
    {
        if (StateManager.Current.ActualState != StateManager.states.Playing)
        {
            transform.position = new Vector3(0, BasePointSO.ExtraFields["Ho"].value);
        }
    }

    protected override void move()
    {
        TimerText.text = Timer.ToString();
        if (StateManager.Current.ActualState == StateManager.states.Playing)
        {
            if (actualSegment < BasePointSO.numberOfSegments)
            {
                if (_rb.position.y >= 0)
                {
                    Vel += (Acc * Time.deltaTime);
                    // _rb.velocity = new Vector2(0f, Vel);
                    _rb.MovePosition(_rb.position + new Vector2(0f, Vel) * Time.fixedDeltaTime);
                    Timer += Time.deltaTime;
                }
                else
                {
                    _rb.velocity = new Vector2(0f, 0f);
                }
            }
            else
            {
                _rb.velocity = new Vector2(0f, 0f);
            }
        }
        else
        {
            _rb.velocity = new Vector2(0f, 0f);
        }
    }
}
