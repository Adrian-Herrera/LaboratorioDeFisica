using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{

    public List<TMP_InputField> TableData;
    public soCar dataCar;

    void Awake()
    {
        var listData = GetComponentsInChildren<TMP_InputField>();
        foreach (var item in listData)
        {
            TableData.Add(item);
        }
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Change(TMP_InputField inputData, string Text)
    {
        var indice = 0;
        if (Text != "")
        {
            for (int i = 0; i < dataCar.dataTable.GetLength(0); i++)
            {
                for (int j = 0; j < dataCar.dataTable.GetLength(1); j++)
                {
                    if (indice == TableData.IndexOf(inputData))
                    {
                        // Debug.Log("[" + i + "," + j + "] = " + Text);
                        dataCar.dataTable[i, j] = float.Parse(Text);
                    }
                    indice++;
                }
            }
        }
    }

    public void getData()
    {
        var index = 0;
        for (int i = 0; i < dataCar.dataTable.GetLength(0); i++)
        {
            for (int j = 0; j < dataCar.dataTable.GetLength(1); j++)
            {
                TableData[index].text = dataCar.dataTable[i, j].ToString();
                index++;
            }
        }
    }




}
