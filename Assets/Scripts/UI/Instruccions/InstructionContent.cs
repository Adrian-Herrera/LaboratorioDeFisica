using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InstructionContent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _instructionTitle, _instruction, _data;
    private readonly List<Field> AnswerFields = new List<Field>();
    // private Dictionary<Field, bool> AnswerFields = new Dictionary<Field, bool>();
    private Variable[] Questions;
    public void SetData(Instruction inst)
    {
        Reset();
        Questions = inst.Questions;
        _instruction.text = inst.text;

        _data.text = "Datos: \n";
        foreach (Variable item in inst.Data)
        {
            _data.text += AddVariableText(item);
            ExerciseManager.current.ChangeFieldValue(0, (int)item.name, item.value);
        }
        ExerciseManager.current.SelectedSegment.setInteractableAll(false);
        _data.text += "\n Incógnitas: \n";
        foreach (Variable item in Questions)
        {
            _data.text += AddVariableText(item, false);
            AnswerFields.Add(ExerciseManager.current.GetBasePointField(0, (int)item.name));
            ExerciseManager.current.SelectedSegment.setInteractable((int)item.name, true);
        }

    }
    public string AddVariableText(Variable variable, bool value = true)
    {
        string Color = ColorUtility.ToHtmlStringRGB(variable.color);
        return $" <color=#{Color}>{variable.name}={(value ? variable.value.ToString() : " ? ")}</color>";
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
    public void Congratulation(int remaining)
    {
        _instructionTitle.text = "Felicidades!!!";
        if (remaining > 0)
        {
            _instruction.text = $"Te {(remaining == 1 ? "falta" : "faltan")} {remaining} {(remaining == 1 ? "ejercicio" : "ejercicios")} mas";
        }
        else
        {
            _instruction.text = "Completaste todos los ejercicios";
        }
        _data.text = "";

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _instructionTitle.transform.gameObject)
        {
            _instruction.gameObject.SetActive(!_instruction.gameObject.activeSelf);
            _instructionTitle.text = _instruction.gameObject.activeSelf ? "Enunciado" : "Enunciado...";
            LayoutRebuilder.ForceRebuildLayoutImmediate(_instruction.transform.parent.GetComponent<RectTransform>());
        }
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
    public Color color;
};
