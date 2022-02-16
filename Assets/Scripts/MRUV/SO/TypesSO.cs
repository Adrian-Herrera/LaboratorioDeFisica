using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TypesSO", menuName = "ScriptableObject/ TypesSO")]

public class TypesSO : ScriptableObject
{
    public Sprite _sprite;
    public string title;
    public problem[] problems;
    public void SelectType()
    {
        HeaderManager.current.ActiveType = this;
    }
}
[System.Serializable]
public struct problem
{
    public string title;
    public Sprite sprite;
    public int Incognita;
    public void SelectProblem()
    {
        HeaderManager.current.ActiveProblem = this;
    }
}
