using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GyroscopeDebug : MonoBehaviour
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
        x.text = $"x: {Input.gyro.attitude.x}";
        y.text = $"y: {Input.gyro.attitude.y}";
        z.text = $"z: {Input.gyro.attitude.z}";
    }
}
