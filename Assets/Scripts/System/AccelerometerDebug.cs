using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerometerDebug : MonoBehaviour
{
    [SerializeField] private Text x;
    [SerializeField] private Text y;
    [SerializeField] private Text z;

    // Start is called before the first frame update
    void Start()
    {
        x.text = "0";
        y.text = "0";
        x.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        x.text = $"x: {Input.acceleration.x}";
        y.text = $"y: {Input.acceleration.y}";
        z.text = $"z: {Input.acceleration.z}";
    }
}
