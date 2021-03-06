﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCircle : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private int _vertexNumber = 120;
    private float _radius = 3;
    private Vector3 centerPos = new Vector3(3f, 0f, 0f);

    void Start()
    {
        Application.targetFrameRate = 30;

        // Draw();
        lineRenderer = GetComponent<LineRenderer>();
        DrawPolygon(_vertexNumber, _radius, centerPos, 0.15f, 0.15f);

    }

    void Update()
    {

    }

    public void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }

    public void Draw()
    {
        DrawPolygon(_vertexNumber, _radius, centerPos, 0.2f, 0.2f);
    }

}
