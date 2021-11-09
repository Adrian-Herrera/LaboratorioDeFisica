using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderManager : MonoBehaviour
{
    public static HeaderManager current;
    private void Awake() {
        current = this;
    }
    
    public TypesSO ActiveType;
    public ProblemsSO ActiveProblem;
}
