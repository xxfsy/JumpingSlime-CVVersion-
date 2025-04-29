using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : MonoBehaviour
{
    [SerializeField] private float _speedOfMoving = 0.1f;
    private Vector3 _lowerLeftCornerPosition, _platformSize;
    private bool _isMovingToRight;

    private void Start()
    {
        _lowerLeftCornerPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
        _platformSize = this.GetComponent<Renderer>().bounds.size;

        _isMovingToRight = Random.Range(0, 2) == 1; 
    }

    private void Update()
    {
        if(_isMovingToRight)
        {
            if (transform.position.x <= -_lowerLeftCornerPosition.x - _platformSize.x/2)
                transform.position += new Vector3(_speedOfMoving * Time.deltaTime, 0, 0);
            else
                _isMovingToRight = false;
        }
        else
        {
            if (transform.position.x >= _lowerLeftCornerPosition.x + _platformSize.x/2)
                transform.position -= new Vector3(_speedOfMoving * Time.deltaTime, 0, 0);
            else
                _isMovingToRight = true;
        }
    }
}
