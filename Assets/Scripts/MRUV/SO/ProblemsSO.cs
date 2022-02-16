using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProblemSO", menuName = "ScriptableObject/ ProblemSO")]

public class ProblemsSO : ScriptableObject
{
    public string title;
    public Sprite sprite;
    public MRUVFormulary.option Incognita;
    public void SelectProblem()
    {
        // HeaderManager.current.ActiveProblem = this;
    }

    // public void Calculate(int emptyVar)
    // {
    //     MRUVExerciseManager.current.searchFormula((MRUVFormulary.option)emptyVar);
    // }
    // public void Calculate()
    // {
    //     MRUVExerciseManager.current.searchFormula(Incognita);
    // }
}
