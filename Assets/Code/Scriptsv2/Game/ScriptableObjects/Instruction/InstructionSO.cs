using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Instructions", menuName = "ScriptableObject/InstructionsScriptableObject")]
public class InstructionSO : ScriptableObject
{
    public TemplateInstruction[] Instructions;
}
[Serializable]
public struct TemplateInstruction
{
    public string Title;
    [TextArea(15, 20)]
    public string Description;

}
