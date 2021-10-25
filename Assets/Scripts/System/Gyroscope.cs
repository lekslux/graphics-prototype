using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    [SerializeField] private int depth = 1;

    private const int _maxDelta = 5; // px

    private float _defaultAxisValue;
    private float _lastUpdate;

    // Start is called before the first frame update
    void Start()
    {
        _defaultAxisValue = Input.gyro.attitude.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
