using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TypesSO", menuName = "ScriptableObject/ TypesSO")]

public class TypesSO : ScriptableObject
{
    public Sprite _sprite;
    public string title;
    public ProblemsSO[] problems;

    public void SelectType(){
        HeaderManager.current.ActiveType = this;
    }
}
