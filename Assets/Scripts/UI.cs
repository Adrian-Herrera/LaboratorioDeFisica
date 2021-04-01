using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject carData1 = null, carData2 = null, carData3 = null;

    public soCar dataCar1, dataCar2, dataCar3;
    private TMP_InputField[] _carData1, _carData2, _carData3;

    // Start is called before the first frame update
    void Start()
    {
        _carData1 = carData1.GetComponentsInChildren<TMP_InputField>();
        _carData2 = carData2.GetComponentsInChildren<TMP_InputField>();
        _carData3 = carData3.GetComponentsInChildren<TMP_InputField>();

        // 0. Vel_input
        // 1. Acc_input
        // 2. DistIni_input
        // 3. Dist_input
        // 4. Time_input 

        setData(_carData1, dataCar1);
        setData(_carData2, dataCar2);
        setData(_carData3, dataCar3);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (dataCar1.onMove) setData(_carData1, dataCar1);
        // if (dataCar2.onMove) setData(_carData2, dataCar2);
        // if (dataCar3.onMove) setData(_carData3, dataCar3);

        if (Time.timeScale != 0)
        {
            setData(_carData1, dataCar1);
            setData(_carData2, dataCar2);
            setData(_carData3, dataCar3);
        }
    }

    private void setData(TMP_InputField[] data, soCar car)
    {
        // data[0].text = car.speed.ToString("F3");
        // data[1].text = car.acceleration.ToString("F3");
        // data[2].text = car.initialPosition.ToString("F3");
        // data[3].text = car.finalPosition.ToString("F3");
        // data[4].text = car.timeMov.ToString("F2");
    }

    public void updateData(TMP_InputField[] data, soCar car)
    {
        // car.speed = float.Parse(data[0].text);
        // car.acceleration = float.Parse(data[1].text);
        // car.initialPosition = float.Parse(data[2].text);
        // car.finalPosition = float.Parse(data[3].text);

    }

    public void Save(int op)
    {
        switch (op)
        {
            case 1:
                updateData(_carData1, dataCar1);
                break;
            case 2:
                updateData(_carData2, dataCar2);
                break;
            case 3:
                updateData(_carData3, dataCar3);
                break;

            default:
                break;
        }

    }

}
