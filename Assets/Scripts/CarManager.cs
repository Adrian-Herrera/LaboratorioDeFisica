using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{

    
    public GameObject[] cars;
    // Start is called before the first frame update
    void Start()
    {
        Find();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Find()
    {
        cars = GameObject.FindGameObjectsWithTag("Car");
        
    }

    public void BeginAll()
    {
        Find();
        Time.timeScale = 1;
        foreach (var car in cars)
        {
            car.GetComponent<Car>().Begin();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void fastForward(){
        Time.timeScale = 2;
    }

    public void Restart(){
        Find();
        
        foreach (var car in cars)
        {
            car.GetComponent<Car>().Restart();
        }
    }

}
