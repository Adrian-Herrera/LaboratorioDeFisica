using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ExerciseManager : MonoBehaviour
{
    public static ExerciseManager current;
    [SerializeField] protected BasePointSO[] BasePointSO;
    public SegmentField SelectedSegment;
    public List<string> Units = new List<string>() {
        "Metro/Segundo", "Kilometro/hora"
    };
    public short SelectedUnit;
    protected virtual void Awake()
    {
        current = this;
    }
    protected virtual void Start()
    {
        SelectedUnit = 0;
        Debug.Log("ExerciseManager active");
    }
    protected abstract void GetFieldData(int segment);
    public abstract void SearchFormula(int Variable);
    public abstract void CheckEveryTime(BasePointSO BasePointSO);
    public abstract void PreFormula();
    protected float MetersToKilometers(float value, bool normal = true)
    {
        return normal ? value / 1000 : value * 1000;
    }
    protected float MtsToKmh(float value, bool normal = true)
    {
        return normal ? (value / 1000 * 3600) : (value * 1000 / 3600);
    }
    protected float Mts2ToKmh2(float value, bool normal = true)
    {
        return normal ? (value / 1000 * 12960000) : (value * 1000 / 12960000);
    }
    protected float SecondsToHours(float value, bool normal = true)
    {
        return normal ? (value / 3600) : (value * 3600);
    }
    public virtual void SetUnit(int unit)
    {
        SelectedUnit = (short)unit;
    }
    public virtual void ChangeFieldValue(int segmentId, int fieldId, float value)
    {
        BasePointSO[0].ChangeFieldValue(segmentId, fieldId, value);
    }
    public virtual void ResetValues()
    {
        foreach (BasePointSO item in BasePointSO)
        {
            item.ResetValues();
        }
    }
    public Field GetBasePointField(int i, int j)
    {
        return BasePointSO[0].GetField(i, j);
    }
}
