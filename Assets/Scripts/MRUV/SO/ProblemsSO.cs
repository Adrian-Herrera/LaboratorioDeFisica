using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProblemSO", menuName = "ScriptableObject/ ProblemSO")]

public class ProblemsSO : ScriptableObject
{
    public string title;
    public Sprite sprite;

    [Range(1, 4)]
    public int[] SegmentRange;

    //Define Enum
    public enum Variables { Vo, Vf, a, x, t };

    //This is what you need to show in the inspector.
    public Variables Incognita;

    public void SelectProblem()
    {
        HeaderManager.current.ActiveProblem = this;
    }

    public void Calculate(string emptyVar)
    {
        // Debug.Log(Incognita.ToString());
        // Debug.Log(emptyVar);
        ExerciseManager.current.equations[Incognita.ToString()](emptyVar);
    }


}
