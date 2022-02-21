using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ExerciseManager : MonoBehaviour
{
    public static ExerciseManager current;
    [SerializeField] protected CarSO[] Cars;
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
    public abstract void CheckEveryTime(BasePointSO car);
}
