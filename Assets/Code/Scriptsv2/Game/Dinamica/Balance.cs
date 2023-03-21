using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Balance : DynamicObject
{
    [SerializeField] private GameObject _weightTemplate;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _yellowMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private Transform _baseTransform;
    private List<BalanceWeight> _listWights = new();
    public void AddWeight(float mass)
    {
        GameObject weight = Instantiate(_weightTemplate, _baseTransform);
        weight.SetActive(true);
        switch (mass)
        {
            case 1:
                weight.GetComponent<MeshRenderer>().material = _greenMaterial;
                break;
            case 5:
                weight.GetComponent<MeshRenderer>().material = _yellowMaterial;
                break;
            case 10:
                weight.GetComponent<MeshRenderer>().material = _redMaterial;
                break;
            default:
                break;
        }

        BalanceWeight newWeight = new(mass, weight);
        _listWights.Add(newWeight);
        SortWeights();
        Masa += mass;
    }
    public void RemoveWeight(float mass)
    {
        BalanceWeight temp = _listWights.Find(e => e.mass == mass);
        Destroy(temp.go);
        Masa -= temp.mass;
        _listWights.Remove(temp);
        SortWeights();
    }
    private void SortWeights()
    {
        float initialHeigth = 0.05f;
        _listWights = _listWights.OrderByDescending(w => w.mass).ToList();
        for (int i = 0; i < _listWights.Count; i++)
        {
            _listWights[i].go.GetComponent<Transform>().localPosition = new Vector3(0, initialHeigth + initialHeigth * i, 0);
        }
    }
    private struct BalanceWeight
    {
        public float mass;
        public GameObject go;
        public BalanceWeight(float mass, GameObject go)
        {
            this.mass = mass;
            this.go = go;
        }
    }

}
