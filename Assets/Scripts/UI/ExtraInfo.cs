using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraInfo : MonoBehaviour
{
    [SerializeField] private GameObject InputWithName;
    public void InstanceInputs(string[] names, BasePointSO carSO)
    {
        foreach (string name in names)
        {
            GameObject go = Instantiate(InputWithName, transform);
            go.GetComponentInChildren<TMP_Text>().text = name;
            carSO.AddExtraFields(name, go.GetComponentInChildren<Field>());
        }
    }
}
