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
    [SerializeField] private List<MainObjectOld> _objects = new List<MainObjectOld>();

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        Debug.Log("Exercise Manager 2 Init");
        foreach (MainObjectOld item in _objects)
        {
            item.Init();
            _footer.Init(item);
        }


        // _footer.CreateObject(_objects[0]);
    }
}
