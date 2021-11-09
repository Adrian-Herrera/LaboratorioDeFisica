using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProblemSO", menuName = "ScriptableObject/ ProblemSO")]

public class ProblemsSO : ScriptableObject
{
    public string title;
    public Sprite sprite;
    public int func;

    [Range(1, 4)]
    public int[] SegmentRange;

    //Define Enum
    public enum Variables { Vo, Vf, a, x, t };

    //This is what you need to show in the inspector.
    public Variables[] Incognitas;

    public void SelectProblem()
    {
        HeaderManager.current.ActiveProblem = this;
    }

    public void Calculate()
    {
        ExerciseManager.current.equation[func]();
    }


}
