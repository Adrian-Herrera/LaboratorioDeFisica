using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulary : MonoBehaviour
{

    public float distance(float inicial, float vel, float time)
    {
        float distance = inicial + (vel * time);
        return distance;
    }
    public float time(float inicial, float actual, float vel)
    {
        float time = (actual - inicial) / vel;
        return time;
    }
    public float velocity(float inicial, float actual, float time)
    {
        float velocity = (actual - inicial) / time;
        return velocity;
    }

}
