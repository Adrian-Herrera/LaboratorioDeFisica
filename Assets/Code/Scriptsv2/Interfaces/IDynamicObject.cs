using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDynamicObject
{
    public VariableUnity Masa { get; }
    public VariableUnity Tension { get; }
    public VariableUnity Peso { get; }
    public VariableUnity Normal { get; }
}
