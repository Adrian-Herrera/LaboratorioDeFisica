using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ExerciseManager : MonoBehaviour
{
    public static ExerciseManager current;
    [SerializeField] protected BasePointSO[] BasePointSO;
    public SegmentField SelectedSegment;
    protected virtual void Awake()
    {
        current = this;
    }
    protected virtual void Start()
    {
        Debug.Log("ExerciseManager active");
    }
    protected abstract void getFieldData(int segment);
    public abstract void searchFormula(int Variable);
    public abstract void CheckEveryTime(BasePointSO BasePointSO);
    public abstract void PreFormula();
}
