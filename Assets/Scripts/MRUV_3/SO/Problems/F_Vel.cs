using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Vel : ParentProblem
{
    public override void excercise()
    {
        Debug.Log("Llamar formula 1");
        foreach (var item in Incognitas)
        {
            Debug.Log(item);
        }

    }
}
