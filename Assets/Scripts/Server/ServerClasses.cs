using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerClasses
{
    static QuestionData Data1 = new QuestionData("Vo", 0);
    static QuestionData Data2 = new QuestionData("a", 2);
    static QuestionData Data3 = new QuestionData("t", 5);
    static QuestionData Data4 = new QuestionData("Vf", 40);

    static QuestionData QuestionData1 = new QuestionData("Vf", 10);
    static QuestionData QuestionData2 = new QuestionData("x", 25);
    static QuestionData QuestionData3 = new QuestionData("t", 20);

    static string text = "Un trineo  <color=\"blue\">parte del reposo</color> con una <color=\"red\">aceleración constante de 2m/s<sup>2</sup></color> Calcular: a)La velocidad que alcanza al cabo de<color=\"green\">5s.</color>";


    static QuestionData[] SData1 = { Data1, Data2, Data3 };
    static QuestionData[] SData2 = { Data1, Data2, Data4 };

    static QuestionData[] QData1 = { QuestionData1, QuestionData2 };
    static QuestionData[] QData2 = { QuestionData2 };
    static QuestionData[] QData3 = { QuestionData3 };
    static Question Question1 = new Question("Ejercicio 1", text, SData1, QData1);
    static Question Question2 = new Question("Ejercicio 2", text, SData1, QData2);
    static Question Question3 = new Question("Ejercicio 3", text, SData1, QData3);

    static Question[] Aquestions = { Question1, Question2, Question3 };

    public static Quiz Quiz1 = new Quiz("MRUV - Basico", 120, Aquestions);

}
public class Quiz
{
    public string Title;
    public float Time;
    public Question[] Questions;
    public Quiz(string title, float time, Question[] questions)
    {
        Title = title;
        Time = time;
        Questions = questions;
    }
}
public class Question
{
    public string Title;
    public string Content;
    public QuestionData[] Data;
    public QuestionData[] Questions;
    public Question(string title, string content, QuestionData[] data, QuestionData[] questions)
    {
        Title = title;
        Content = content;
        Data = data;
        Questions = questions;
    }
}
public class QuestionData
{
    public string VarName;
    public float Answer;
    public float Value;
    public bool IsAnswered;
    public QuestionData(string name, float answer)
    {
        VarName = name;
        Answer = answer;
        IsAnswered = false;
    }
}
