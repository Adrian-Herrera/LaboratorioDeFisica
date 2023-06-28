using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTwoObjects : MonoBehaviour
{
    [SerializeField] private Transform transform1, transform2;
    private LineRenderer line;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] points = new Vector3[2];
        points[0] = transform1.position;
        points[1] = transform2.position;
        line.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
