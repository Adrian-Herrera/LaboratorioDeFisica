using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceVector : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _xArrow;
    [SerializeField] private SpriteRenderer _yArrow;
    [SerializeField] private SpriteRenderer _totalArrow;
    [SerializeField] private TMP_Text _anglesText, _totalText, _xText, _yText;
    [SerializeField] private float _size;
    [SerializeField] private int _angle;
    public Force force;
    private int degreeOffset;

    // Update is called once per frame
    void Update()
    {
        degreeOffset = CalculateDegreeOffset(force.CurrentDirection);
        _angle = force.Degree + degreeOffset;
        _size = force.Size;
        if (_size == 0)
        {
            _xArrow.gameObject.SetActive(false);
            _yArrow.gameObject.SetActive(false);
            _totalArrow.gameObject.SetActive(false);
            _anglesText.gameObject.SetActive(false);
        }
        else
        {
            _totalArrow.gameObject.SetActive(true);
            _totalArrow.size = new Vector2(_size / 10, .5f);
            _totalArrow.transform.localEulerAngles = new Vector3(0, 0, _angle);
            _totalText.text = _size.ToString("F2");
            _totalText.transform.eulerAngles = Vector3.zero;

            _xArrow.gameObject.SetActive(_angle % 90 != 0);
            _yArrow.gameObject.SetActive(_angle % 90 != 0);
            _anglesText.gameObject.SetActive(_angle % 90 != 0);
            if (_angle % 90 != 0)
            {
                float radian = _angle * Mathf.PI / 180;

                float xSize = _size * Mathf.Cos(radian);
                _xArrow.size = new Vector2(Mathf.Abs(xSize) / 10, .5f);
                _xArrow.transform.localEulerAngles = new Vector3(0, 0, xSize < 0 ? 180 : 0);
                _xText.text = xSize.ToString("F2");
                _xText.transform.eulerAngles = Vector3.zero;

                float ySize = _size * Mathf.Sin(radian);
                _yArrow.size = new Vector2(Mathf.Abs(ySize) / 10, .5f);
                _yArrow.transform.localEulerAngles = new Vector3(0, 0, ySize < 0 ? -90 : 90);
                _yText.text = ySize.ToString("F2");
                _yText.transform.eulerAngles = Vector3.zero;

                _anglesText.SetText(Mathf.Abs(force.Degree).ToString() + "°");
            }
        }
    }
    private int CalculateDegreeOffset(Force.Direction direction)
    {
        return direction switch
        {
            Force.Direction.up => 90,
            Force.Direction.down => -90,
            Force.Direction.left => 180,
            Force.Direction.rigth => 0,
            _ => 0,
        };
    }

}
