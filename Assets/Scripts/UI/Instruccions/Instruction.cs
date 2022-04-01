using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Instruction", menuName = "ScriptableObject/NewInstruction")]
public class Instruction : ScriptableObject
{
    public string title;
    [TextArea(3, 10)]
    public string text;
    public GameObject Formulas;
    public Variable[] Data;
    public Variable[] Questions;
    public Field[] QuestionsFields;

}
