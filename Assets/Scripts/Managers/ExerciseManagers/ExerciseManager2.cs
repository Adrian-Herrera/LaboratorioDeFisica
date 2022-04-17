using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseManager2 : MonoBehaviour
{
    public static ExerciseManager2 current;
    private void Awake()
    {
        current = this;
    }

    [SerializeField] private HeaderManager _header;
    [SerializeField] private FooterManager _footer;
    [SerializeField] private List<MainObject> _objects = new List<MainObject>();

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        Debug.Log("Exercise Manager 2 Init");
        foreach (MainObject item in _objects)
        {
            item.Init();
            _footer.Init(item);
        }


        // _footer.CreateObject(_objects[0]);
    }
}
