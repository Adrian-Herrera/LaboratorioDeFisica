using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationStatus
{
    Waiting, Running
}
public class Station : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private InstructionSO _instructions;
    [SerializeField] private StationStatus _status;
    [SerializeField] private StationUI _stationUI;
    [SerializeField] private int _temaId;
    public string Name => _name;
    public InstructionSO Instructions => _instructions;
    public StationStatus Status => _status;
    public StationUI StationUI => _stationUI;
    public int TemaId => _temaId;
    // [SerializeField] private 
    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Algo ingreso");
            player.SetNearStation(this);
            InstructionUI.OnStartExercise += Activate;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Algo salio");
            player.SetNearStation(null);
            _status = StationStatus.Waiting;
            InstructionUI.OnStartExercise -= Activate;
        }
    }
    private void Activate()
    {
        _status = StationStatus.Running;
    }
}
