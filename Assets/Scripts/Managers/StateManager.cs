using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Current;
    private void Awake()
    {
        Current = this;
    }

    public enum states
    {
        Playing, Pause, Stop
    }
    public states ActualState;
    private void Start()
    {
        ActualState = states.Stop;
    }

    public void ChangeState(states newState)
    {
        ActualState = newState;
    }

    public void PlayButton()
    {
        ActualState = states.Playing;
        Time.timeScale = 1;
    }
    public void PauseButton()
    {
        ActualState = states.Pause;
        Time.timeScale = 0;
    }
    public void StopButton()
    {
        ActualState = states.Stop;
    }



}
