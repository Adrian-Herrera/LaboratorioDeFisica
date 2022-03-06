using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BasePoint : MonoBehaviour
{
    [SerializeField] protected BasePointSO BasePointSO;
    [SerializeField] protected TMP_Text TimerText;
    protected Rigidbody2D _rb;
    protected float Vel, Velf, Acc, Timer, Distance, SegmentDistance;
    protected float TimeUntilStop;
    protected int actualSegment;
    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        actualSegment = 0;
        SegmentDistance = 0;
    }
    private void Update() {
        PreMove();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    protected abstract void getInfo(int segment);
    public void Play()
    {
        StateManager.Current.ActualState = StateManager.states.Playing;
        getInfo(actualSegment);
    }
    public void Reset()
    {
        StateManager.Current.ActualState = StateManager.states.Stop;
        transform.position = Vector3.zero;
        actualSegment = 0;
    }

    protected abstract void move();
    protected abstract void PreMove();
}
