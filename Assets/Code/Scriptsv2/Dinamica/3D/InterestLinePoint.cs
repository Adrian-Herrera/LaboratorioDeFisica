using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterestLinePoint : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _point;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _velText;
    [SerializeField] private TMP_Text _xDistanceText;
    [SerializeField] private TMP_Text _yDistanceText;
    private float _XDistance;
    private float _YDistance;
    private Vector3 _velocity;
    public void CreatePoint(Vector3 pos, Vector3 vel, Vector3 dis)
    {
        transform.position = pos;
        _velocity = vel;
        _velText.text = "Vel-x: " + Mathf.Round(Mathf.Abs(vel.x) * 100) / 100 + "m/s<sup>2</sup> \nVel-y " + Mathf.Round(vel.y * 100) / 100 + " m/s<sup>2</sup>";
        _xDistanceText.text = "Distancia x:" + Mathf.Round(Mathf.Abs(dis.x) * 100) / 100 + " mts";
        _yDistanceText.text = "Distancia y:" + Mathf.Round(Mathf.Abs(dis.y) * 100) / 100 + " mts";
    }
    public void Interact()
    {
        Debug.Log("Hola");
        _panel.SetActive(!_panel.activeSelf);
    }

}
public interface IInteractable
{
    void Interact();
}
