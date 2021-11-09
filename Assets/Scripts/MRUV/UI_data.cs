using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_data : MonoBehaviour
{
    [SerializeField] private soCar soCar;
        
    enum DataType
    {
        Vo, Vf, Acc, Time, Xo, Xf
    }

    public GameObject image, segmentData, totalData;
    public TMP_InputField Dist_total, Time_total;
    public TMP_InputField.SubmitEvent onEndEdit { get; set; }
    private TMP_InputField[] UIData;

    
    void Awake()
    {
        // UIData = DataGrid.GetComponentsInChildren<TMP_InputField>();
        image.GetComponent<Image>().sprite = soCar.carSprite;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showData()
    {
        // 0.Vo 1.Vf 2.Acc 3.Time 4.Xo 5.Xf
        for (int i = 0; i < UIData.Length; i++)
        {
            UIData[i].text = soCar.dataTable[soCar.selectedSegment, i].ToString();
        }
    }

    private void reloadData()
    {
        soCar.Dist_Total();
        Dist_total.text = soCar.x_total.ToString();

    }

    public void changeData(string data){
        
    }

    public void changeTextVo(string data)
    {

        if (data == "?")
        {
            soCar.varToResolve.Add(0);
        }
        else
        {
            soCar.dataTable[soCar.selectedSegment, 0] = float.Parse(data);
            reloadData();
        }
    }
    public void changeTextVf(string data)
    {
        if (data == "?")
        {
            soCar.varToResolve.Add(1);
        }
        else
        {
            soCar.dataTable[soCar.selectedSegment, 1] = float.Parse(data);
            reloadData();
        }
    }
    public void changeTextAcc(string data)
    {
        if (data == "?")
        {
            soCar.varToResolve.Add(2);
        }
        else
        {
            soCar.dataTable[soCar.selectedSegment, 2] = float.Parse(data);
            reloadData();
        }
    }
    public void changeTextTime(string data)
    {
        if (data == "?")
        {
            soCar.varToResolve.Add(3);
        }
        else
        {
            soCar.dataTable[soCar.selectedSegment, 3] = float.Parse(data);
            reloadData();
        }
    }
    public void changeTextXo(string data)
    {
        if (data == "?")
        {
            soCar.varToResolve.Add(4);
        }
        else
        {
            soCar.dataTable[soCar.selectedSegment, 4] = float.Parse(data);
            reloadData();
        }
    }
    public void changeTextXf(string data)
    {
        if (data == "?")
        {
            soCar.varToResolve.Add(5);
        }
        else
        {
            soCar.dataTable[soCar.selectedSegment, 5] = float.Parse(data);
            reloadData();
        }
    }

    public void changeTextXTotal(string data)
    {
        soCar.x_total = float.Parse(data);
    }
    public void changeTextTimeTotal(string data)
    {
        soCar.time_total = float.Parse(data);
    }
}
