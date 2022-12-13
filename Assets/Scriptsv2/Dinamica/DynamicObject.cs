using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DynamicObject : MonoBehaviour
{
    [Header("Datos Panel")]
    public int Masa;
    public float Acc;
    public float Distance;
    public float Velocidad = 0;
    public int angle;
    public float Timer;
    public float NormalTimer;
    public bool _hasFloor;
    public bool _hasRope;
    public float testTimer = 0;
    public float RozamientoDinamico = 0;
    [Header("Datos Debug")]
    [SerializeField] private TMP_Text _masaTxt;
    public float TimeScale = 1;
    public float SumLeftForces;
    public float SumRightForces;
    public float SumUpForces;
    public float SumDownForces;
    public List<GameObject> VectorsUp;
    public List<GameObject> VectorsDown;
    public List<GameObject> VectorsLeft;
    public List<GameObject> VectorsRight;
    public Vector3 InitialPoint;
    public ForceVector _vectorGO;
    public List<Force> Forces = new();
    // public ForceList ForcesUp;
    // public ForceList ForcesDown;
    // public ForceList ForcesLeft;
    // public ForceList ForcesRight;
    public Force Peso, Normal, Rozamiento, Tension;
    public bool isMoving = false;
    public bool firstMove = true;
    private IEnumerator _moveEnumerator;
    private DragObject _dragObject;
    private void Awake()
    {
        _dragObject = GetComponent<DragObject>();
        VectorsUp = new();
        VectorsDown = new();
        VectorsLeft = new();
        VectorsRight = new();
    }
    private void Start()
    {
        InitialPoint = transform.position;
        Timer = 0;
        NormalTimer = 0;
        Peso = new Force("Peso", Force.Direction.down, 0, Masa * 10);
        Normal = new Force("Normal", Force.Direction.up, 0, 10);
        Rozamiento = new Force("Rozamiento", Force.Direction.left, 0, RozamientoDinamico * Normal.Size);
        Tension = new Force("Tension", Force.Direction.up, 0, 0);

        Forces.Add(Peso);
        Forces.Add(Normal);
        Forces.Add(Rozamiento);
        Forces.Add(Tension);
        ForceVector fv = Instantiate(_vectorGO, transform);
        ForceVector fvn = Instantiate(_vectorGO, transform);
        ForceVector fvr = Instantiate(_vectorGO, transform);
        ForceVector fvt = Instantiate(_vectorGO, transform);
        fv.force = Peso;
        fvn.force = Normal;
        fvr.force = Rozamiento;
        fvt.force = Tension;
        _moveEnumerator = CalculateData();
    }
    private void Update()
    {
        if (Timer >= 3000)
        {
            Pause();
        }
        _masaTxt.text = Masa.ToString() + " kg";
        if (isMoving)
        {
            testTimer += Time.deltaTime;
        }
        if (_dragObject.AttachedObject != null && _dragObject.AttachedObject.CompareTag("Floor"))
        {
            _hasFloor = true;
            angle = _dragObject.AttachedObject.GetComponent<Floor>().Degree;
            RozamientoDinamico = _dragObject.AttachedObject.GetComponent<Floor>().RozamientoDinamico;
            Tension.CurrentDirection = Force.Direction.rigth;
        }
        else
        {
            _hasFloor = false;
            angle = 0;
            RozamientoDinamico = 0;
            Tension.CurrentDirection = Force.Direction.up;
        }
        SumUpForces = 0;
        SumDownForces = 0;
        SumLeftForces = 0;
        SumRightForces = 0;
        foreach (Force force in Forces)
        {
            if (force == Rozamiento && _hasFloor)
            {
                continue;
            }
            switch (force.CurrentDirection)
            {
                case Force.Direction.up:
                    SumUpForces += force.Size;
                    break;
                case Force.Direction.down:
                    if (force.Degree == 0)
                    {

                        SumDownForces += force.Size;
                    }
                    else
                    {
                        SumDownForces += force.CosForce();
                        if (force.Degree > 0)
                        {
                            SumRightForces += force.SinForce();
                        }
                        else
                        {
                            SumLeftForces += force.SinForce();
                        }
                    }
                    break;
                case Force.Direction.left:
                    SumLeftForces += force.Size;
                    break;
                case Force.Direction.rigth:
                    SumRightForces += force.Size;
                    break;
                default:
                    break;
            }
        }

        Peso.Size = Masa * 10;
        Peso.Degree = -Mathf.RoundToInt(transform.rotation.eulerAngles.z);
        // Debug.Log((int)transform.rotation.eulerAngles.z);
        if (_hasFloor)
        {
            Normal.Size = Mathf.Abs(SumDownForces - SumUpForces + Normal.Size);
            Rozamiento.Size = RozamientoDinamico * Normal.Size;
            if (SumLeftForces > SumRightForces)
            {
                Rozamiento.CurrentDirection = Force.Direction.rigth;
                if (Rozamiento.Size > SumLeftForces)
                {
                    Acc = 0;
                }
                else
                {
                    SumRightForces += Rozamiento.Size;
                    Acc = Mathf.Round((SumRightForces - SumLeftForces) / Masa * 1000f) / 1000f;
                }
            }
            else
            {
                Rozamiento.CurrentDirection = Force.Direction.left;
                if (Rozamiento.Size > SumRightForces)
                {
                    Acc = 0;
                }
                else
                {
                    SumLeftForces += Rozamiento.Size;
                    Acc = Mathf.Round((SumRightForces - SumLeftForces) / Masa * 1000f) / 1000f;
                }
            }
            // Acc = Mathf.Round((SumRightForces - SumLeftForces) / Masa * 1000f) / 1000f;
        }
        else
        {
            Normal.Size = 0;
            Rozamiento.Size = 0;
            Acc = Mathf.Round((SumUpForces - SumDownForces) / Masa * 1000f) / 1000f;
        }

    }
    public IEnumerator CalculateData()
    {
        Debug.Log("Coroutine start");

        while (isMoving)
        {
            Timer += 10 / TimeScale;
            NormalTimer += Mathf.Round(Time.fixedDeltaTime * 1000f);
            Velocidad = Acc * (Timer / 1000);
            Distance = Acc * Mathf.Pow(Timer / 1000, 2) / 2;
            if (_hasFloor)
            {

                transform.localPosition = InitialPoint + new Vector3(Distance, 0);
            }
            else
            {
                transform.localPosition = InitialPoint + new Vector3(0, Distance);
            }
            yield return null;
        }
        Debug.Log("Coroutine end");

    }
    public void CreateVectors()
    {

    }
    public void Reset()
    {
        Velocidad = 0;
        transform.localPosition = InitialPoint;
        Timer = 0;
        NormalTimer = 0;
        firstMove = true;
    }
    public void Play()
    {
        if (firstMove)
        {
            InitialPoint = transform.localPosition;
            firstMove = false;
        }
        isMoving = true;
        StartCoroutine(_moveEnumerator);
    }
    public void Pause()
    {
        isMoving = false;
        StopCoroutine(_moveEnumerator);
    }

}
[Serializable]
public class Force
{
    public enum Direction
    {
        up, down, left, rigth
    }
    public string Name;
    public Direction CurrentDirection;
    public int Degree;
    public float Size;
    public Force(string name, Direction direction, int degree, float size)
    {
        Name = name;
        CurrentDirection = direction;
        Degree = degree;
        Size = size;
    }
    public float CosForce()
    {
        float radian = MathF.Abs(Degree) * Mathf.PI / 180;
        return Mathf.Cos(radian) * Size;
    }
    public float SinForce()
    {
        float radian = MathF.Abs(Degree) * Mathf.PI / 180;
        return Mathf.Sin(radian) * Size;
    }
}
[Serializable]
public class ForceList : MonoBehaviour
{
    public List<Force> Forces = new();
    public List<ForceVector> Vectors = new();
    public float Total;
    private void CalculateTotal()
    {
        Total = 0;
        foreach (Force force in Forces)
        {
            Total += force.Size;
        }
    }
    public void AddForce(Force newForce)
    {
        Forces.Add(newForce);
        CalculateTotal();
    }
    public void RemoveForce(Force force)
    {
        Forces.Remove(force);
        CalculateTotal();
    }
    public void CreateVectors(ForceVector prefab, Transform parentTransform)
    {
        int index = 0;
        foreach (ForceVector vector in Vectors)
        {
            int res = Forces.FindIndex(f => f == vector.force);
            if (res < 0)
            {
                Destroy(vector.gameObject);
            }
            index++;
        }
        if (Forces.Count > index)
        {

        }
    }

}
