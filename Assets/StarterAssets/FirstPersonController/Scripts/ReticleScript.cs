using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleScript : MonoBehaviour
{
    [SerializeField] private Image _reticle;
    // Start is called before the first frame update
    void Start()
    {
        _reticle.color = new Color(1, 1, 1, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
