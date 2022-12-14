using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea : MonoBehaviour
{
    public Cuerda _cuerda;
    public float Tension;
    public float AccNum;
    public float AccDen;
    public float Acc;
    public float AccForces1;
    public float AccForces2;
    // public List<DynamicObject> AttachedObjects = new();
    public DynamicObject Object1;
    public DynamicObject Object2;
    public float masa1, masa2;
    public Cuerda Cuerda1;
    public Cuerda Cuerda2;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(_cuerda, transform).SetObjects(gameObject, Object1.gameObject);
        // Object1._hasRope = true;
        // Instantiate(_cuerda, transform).SetObjects(gameObject, Object2.gameObject);
        // Object2._hasRope = true;

        // masa1 = Object1.Masa;
        // masa2 = Object2.Masa;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cuerda1.AttachedObject != null && Cuerda1.AttachedObject.CompareTag("DynamicObject"))
        {
            Object1 = Cuerda1.AttachedObject.GetComponent<DynamicObject>();
        }
        else
        {
            Object1 = null;
        }
        if (Cuerda2.AttachedObject != null && Cuerda2.AttachedObject.CompareTag("DynamicObject"))
        {
            Object2 = Cuerda2.AttachedObject.GetComponent<DynamicObject>();
        }
        else
        {
            Object2 = null;
        }
        if (Object1 == null || Object2 == null) return;
        masa1 = Object1.Masa;
        masa2 = Object2.Masa;

        AccForces1 = Object1._hasFloor ? Object1.Peso.SinForce() + Object1.Rozamiento.Size : Object1.Peso.Size;

        float temp = masa2 * 10 - AccForces1;
        if (temp < 0)
        {
            AccForces1 = Object1._hasFloor ? Object1.Peso.SinForce() - Object1.Rozamiento.Size : Object1.Peso.Size;
            temp = AccForces1 - masa2 * 10;
            AccNum = temp;
            AccDen = masa1 + masa2;
            Acc = AccNum / AccDen;
            Tension = Object1._hasFloor ? Object1.Peso.SinForce() - Object1.Rozamiento.Size - (masa1 * Acc) : Object1.Peso.Size - (masa1 * Acc);
        }
        else
        {
            AccNum = temp;
            AccDen = masa1 + masa2;
            Acc = AccNum / AccDen;
            Tension = Object1._hasFloor ? (masa1 * Acc) + Object1.Peso.SinForce() + Object1.Rozamiento.Size : (masa1 * Acc) + Object1.Peso.Size;
        }

        Object1.Tension.Size = Tension;
        Object2.Tension.Size = Tension;
    }
}
