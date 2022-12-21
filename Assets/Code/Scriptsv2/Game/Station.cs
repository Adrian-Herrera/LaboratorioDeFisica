using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private InstructionSO _instructions;
    // [SerializeField] private 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Algo ingreso");
            player.SetActiveStation(_name, _instructions);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Algo salio");
            player.ClearStation();
        }
    }
}
