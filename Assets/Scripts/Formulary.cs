using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulary : MonoBehaviour
{
    public static Formulary current;
    private void Awake()
    {
        current = this;
    }
    public float VelF_1(float Vo, float acc, float tiempo)
    {
        float velocity = Vo + (acc * tiempo);
        return velocity;
    }

    public float Dist_2(float Vo, float Vf, float tiempo)
    {
        float dist = ((Vo + Vf) * tiempo) / 2;
        return dist;
    }

    public float Velf2_3(float Vo, float acc, float dist)
    {
        float vel = Mathf.Sqrt(Mathf.Pow(Vo, 2) + (2 * acc * dist));
        return vel;
    }

    public float DistFinal_4(float vel, float acc, float tiempo)
    {

        float x = (vel * tiempo) + ((acc * Mathf.Pow(tiempo, 2)) / 2);
        return x;
    }
    public float time(float inicial, float actual, float vel)
    {
        float time = (actual - inicial) / vel;
        return time;
    }

}
