using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public enum Variables
    {
        Vo, Vf, a, x, t
    }
    private void Start()
    {
        Type weekdays = typeof(Variables);
        Enum.GetNames(weekdays);
        foreach (string s in Enum.GetNames(weekdays))
        {
            // Console.WriteLine("{0,-11}= {1}", s, Enum.Format(weekdays, Enum.Parse(weekdays, s), "d"));
            Debug.Log($"{s,-11}= {Enum.Format(weekdays, Enum.Parse(weekdays, s), "d")} ");
        }
    }
}
