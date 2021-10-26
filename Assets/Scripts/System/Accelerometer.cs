using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    [SerializeField] private int maxShift = 0; // px
    [SerializeField] private int maxAngle = 0;

    private float _doubleShift => maxShift * 2;

    private const int sideSpeed = 20;

    private float _defaultX;
    private float _defaultY;
    private float _defaultZ;

    private float _forceX;
    private float _forceZ;

    private float _angleX;
    private float _angleY;

    // Start is called before the first frame update
    void Start()
    {
        _defaultX = transform.position.x;
        _defaultY = transform.position.y;
        _defaultZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // shift block
        if (maxShift > 0)
        {
            Vector3 newPos = transform.position;

            // target positions
            float targetX = maxShift * Input.acceleration.x;
            float targetZ = maxShift / 4 * Input.acceleration.z;

            // force
            float forceX = _forceX + ((targetX - (transform.position.x - _defaultX)) / _doubleShift);
            _forceX = forceX < 0 ? Mathf.Max(-1, Input.acceleration.x) : Mathf.Min(1, Input.acceleration.x);

            float forceZ = _forceZ + ((targetZ - (transform.position.z - _defaultZ)) / _doubleShift);
            _forceZ = forceZ < 0 ? Mathf.Max(-1, Input.acceleration.z) : Mathf.Min(1, Input.acceleration.z);

            // delta
            float deltaX = maxShift * forceX;
            float deltaZ = maxShift / 4 * forceZ;

            // move
            newPos.x = Mathf.Lerp(newPos.x, _defaultX + deltaX, Time.deltaTime * Mathf.Max(sideSpeed * forceX, 1));
            newPos.z = Mathf.Lerp(newPos.z, _defaultZ + deltaZ, Time.deltaTime * Mathf.Max(sideSpeed * forceZ, 1));

            transform.position = newPos;
        }
        // end shift block

        // rotation
        if (maxAngle != 0)
        {
            float deltaAngleX = maxAngle * Input.acceleration.x;
            float _rotateX = deltaAngleX - _angleX;
            _angleX = deltaAngleX;

            //float deltaAngleY = maxAngle * Mathf.Abs((Input.acceleration.y + Input.acceleration.z) / 2);
            //float _rotateY = deltaAngleY - _angleY;
            //_angleY = deltaAngleY;

            transform.Rotate(Vector3.up, _rotateX);
            // transform.Rotate(Vector3.left, _rotateY);
        }
    }
}
