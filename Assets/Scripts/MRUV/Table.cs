using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{

    public List<TMP_InputField> FieldList;
    public GameObject TableField;
    public GameObject _Table;
    public soCar soCar;

    // void Awake()
    // {
    //     var listData = GetComponentsInChildren<TMP_InputField>();
    //     foreach (var item in listData)
    //     {
    //         TableData.Add(item);
    //     }
    // }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("lengt 0" + soCar.dataTable.GetLength(0));
        Debug.Log("lengt 1" + soCar.dataTable.GetLength(1));
        for (int i = 0; i < soCar.dataTable.GetLength(0); i++)
        {
            for (int j = 0; j < soCar.dataTable.GetLength(1); j++)
            {
                GameObject go = Instantiate(TableField, _Table.transform);
                FieldList.Add(go.GetComponent<TMP_InputField>());
            }
        }

        int k = 0;
        foreach (var item in FieldList)
        {
            item.text = k.ToString();
            k++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void Change(TMP_InputField inputData, string Text)
    // {
    //     var indice = 0;
    //     if (Text != "")
    //     {
    //         for (int i = 0; i < dataCar.dataTable.GetLength(0); i++)
    //         {
    //             for (int j = 0; j < dataCar.dataTable.GetLength(1); j++)
    //             {
    //                 if (indice == TableData.IndexOf(inputData))
    //                 {
    //                     // Debug.Log("[" + i + "," + j + "] = " + Text);
    //                     dataCar.dataTable[i, j] = float.Parse(Text);
    //                 }
    //                 indice++;
    //             }
    //         }
    //     }
    // }

    // public void getData()
    // {
    //     var index = 0;
    //     for (int i = 0; i < dataCar.dataTable.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < dataCar.dataTable.GetLength(1); j++)
    //         {
    //             TableData[index].text = dataCar.dataTable[i, j].ToString();
    //             index++;
    //         }
    //     }
    // }




}
