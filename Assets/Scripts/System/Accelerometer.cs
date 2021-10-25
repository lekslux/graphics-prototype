using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    [SerializeField] private int maxShift = 0; // px
    [SerializeField] private int maxAngle = 0;

    private float _halfMaxShift => maxShift / 2;

    private float _shiftX => Mathf.Abs(transform.position.x - _defaultX);
    private float _shiftZ => Mathf.Abs(transform.position.z - _defaultZ);

    private const int sideSpeed = 20;

    private float _lastUpdate;

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

            // force
            float forceX = _forceX - (Input.acceleration.x / 10);
            _forceX = forceX < 0 ? Mathf.Max(-1, forceX) : Mathf.Min(1, forceX);

            float forceZ = _forceZ - (Input.acceleration.z / 10);
            _forceZ = forceZ < 0 ? Mathf.Max(-1, forceZ) : Mathf.Min(1, forceZ);

            // delta
            float deltaX = maxShift * _forceX;
            float deltaZ = maxShift / 4 * _forceZ;

            // speed curve
            float speedX;
            float speedZ;

            if (forceX > 0)
            {
                speedX = transform.position.x > _defaultX
                    ? sideSpeed * (maxShift - _shiftX) / maxShift
                    : sideSpeed;
            } else
            {
                speedX = transform.position.x < _defaultX
                    ? sideSpeed * (maxShift - _shiftX) / maxShift
                    : sideSpeed;
            }

            if (forceZ > 0)
            {
                speedZ = transform.position.z > _defaultZ
                    ? sideSpeed * (maxShift - _shiftZ) / maxShift
                    : sideSpeed;
            }
            else
            {
                speedZ = transform.position.z < _defaultZ
                    ? sideSpeed * (maxShift - _shiftZ) / maxShift
                    : sideSpeed;
            }

            // move
            newPos.x = Mathf.Lerp(newPos.x, _defaultX + deltaX, Time.deltaTime * Mathf.Max(speedX, 0.1f));
            newPos.z = Mathf.Lerp(newPos.z, _defaultZ + deltaZ, Time.deltaTime * Mathf.Max(speedZ, 0.1f));

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
