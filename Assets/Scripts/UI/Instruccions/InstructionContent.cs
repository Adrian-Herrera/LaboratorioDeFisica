using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionContent : MonoBehaviour
{
    [SerializeField] private TMP_Text _instruction;
    [SerializeField] private GameObject _formulas;
    private readonly List<Field> AnswerFields = new List<Field>();
    // private Dictionary<Field, bool> AnswerFields = new Dictionary<Field, bool>();
    private Variable[] Questions;
    public void SetData(Instruction inst)
    {
        Reset();
        Questions = inst.Questions;
        _instruction.text = inst.text;

        _instruction.text += "\n Datos: \n";
        foreach (Variable item in inst.Data)
        {
            CreateVariable(item);
            ExerciseManager.current.ChangeFieldValue(0, (int)item.name, item.value);
        }
        ExerciseManager.current.SelectedSegment.setInteractableAll(false);
        _instruction.text += "\n Incógnitas: \n";
        foreach (Variable item in Questions)
        {
            CreateVariable(item, false);
            AnswerFields.Add(ExerciseManager.current.GetBasePointField(0, (int)item.name));
            ExerciseManager.current.SelectedSegment.setInteractable((int)item.name, true);
        }

    }
    public void CreateVariable(Variable variable, bool value = true)
    {
        _instruction.text += $" {variable.name}={(value ? variable.value.ToString() : "?")}";
    }
    public bool CheckAnswer()
    {
        foreach (var f in AnswerFields)
        {
            foreach (Variable item in Questions)
            {
                if (f.column == (int)item.name)
                {
                    if (f.value == item.value)
                    {
                        f.answer = true;
                        f.ChangeColor(Color.green);
                    }
                }
            }
        }

        foreach (Field f in AnswerFields)
        {
            Debug.Log($"{f.column}, {f.value}");
            if (f.answer == false)
            {
                return false;
            }
        }
        return true;
    }
    private void Reset()
    {
        foreach (Field f in AnswerFields)
        {
            f.answer = false;
        }
        AnswerFields.Clear();
    }
}
[System.Serializable]
public struct Variable
{
    public enum Names
    {
        Vo, Vf, a, x, t
    }
    public Names name;
    public int value;
};
