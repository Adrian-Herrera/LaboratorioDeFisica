﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soCar", menuName = "ScriptableObject/ Car")]
public class soCar : ScriptableObject
{
    public float[,] dataTable = new float[3, 6]; // 0.Vo 1.Vf 2.Acc 3.Time 4.Xo 5.Xf
    public float x_total, time_total;

    public float vel, acc, dist;
    public int numberSegments;

    public void Dist_Total()
    {
        x_total = 0;
        for (int i = 0; i < numberSegments; i++)
        {
            x_total += dataTable[i, 5];

        }
    }

    public void Play()
    {
        for (int i = 0; i < numberSegments; i++)
        {
            CalculateData(i);
        }

        setVelocity(0);
    }

    private float VelFinalDist(float vel_inicial, float acceleration, float dist_inicial)
    {
        return Mathf.Pow(vel_inicial, 2) + 2 * acceleration * dist_inicial; // vel_final
    }

    private float VelFinalTime(float vel_inicial, float acceleration, float tiempo)
    {
        return vel_inicial + acceleration * tiempo; // vel_final
    }

    private float DistFinal(float vel_inicial, float tiempo, float acceleration)
    {
        return vel_inicial * tiempo + ((acceleration * Mathf.Pow(tiempo, 2)) / 2); //dist_final
    }


    private void CalculateData(int SegmentNumber)
    {
        int Vo = 0, Vf = 1, Acc = 2, Time = 3, Xo = 4, Xf = 5;
        if (dataTable[SegmentNumber, Time] == 0)
        {
            dataTable[SegmentNumber, Vf] = VelFinalDist(dataTable[SegmentNumber, Vo], dataTable[SegmentNumber, Acc], dataTable[SegmentNumber, Xo]);
        }
        else
        {
            dataTable[SegmentNumber, Xf] = DistFinal(dataTable[SegmentNumber, Vo], dataTable[SegmentNumber, Time], dataTable[SegmentNumber, Acc]);
            dataTable[SegmentNumber, Vf] = VelFinalTime(dataTable[SegmentNumber, Vo], dataTable[SegmentNumber, Acc], dataTable[SegmentNumber, Time]);
        }


    }

    private void setVelocity(int SegmentNumber)
    {
        var percent = 0f;
        if (dataTable[SegmentNumber, 5] != 0)
        {
            percent = ((dist * 100f) / dataTable[SegmentNumber, 5]) / 100f;
        }

        vel = dataTable[SegmentNumber, 0] * percent;
        acc = dataTable[SegmentNumber, 2] * percent;

    }
}
