using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulary : MonoBehaviour
{

    public float DistFinal(float DistInicial, float vel, float acc, float tiempo)
    {
        float x = DistInicial + (vel * tiempo) - ((acc * Mathf.Pow(tiempo, 2)) / 2);
        return x;
    }
    public float time(float inicial, float actual, float vel)
    {
        float time = (actual - inicial) / vel;
        return time;
    }
    public float VelF(float Vo, float acc, float tiempo)
    {
        float velocity = Vo - (acc * tiempo);
        return velocity;
    }

}
