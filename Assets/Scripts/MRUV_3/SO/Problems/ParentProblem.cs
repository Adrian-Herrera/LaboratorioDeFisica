using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParentProblem : MonoBehaviour
{
    public string title;
    public Sprite sprite;
    public int[] segmento;
    //Define Enum
    public enum Variables { Vo, Vf, a, x, t };

    //This is what you need to show in the inspector.
    public Variables[] Incognitas;

    public abstract void excercise();
}
