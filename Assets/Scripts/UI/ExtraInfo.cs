using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraInfo : MonoBehaviour
{
    [SerializeField] private GameObject InputWithName;
    public void InstanceInputs(BasePointSO BasePointSO)
    {
        foreach (string name in BasePointSO.getNames())
        {
            GameObject go = Instantiate(InputWithName, transform);
            go.GetComponentInChildren<TMP_Text>().text = name;
            BasePointSO.AddExtraFields(name, go.GetComponentInChildren<Field>());
        }
    }
}
