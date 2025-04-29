using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _targetForCamera;
    private DeadZone _deadZone;

    private void Start()
    {
        _deadZone = GetComponentInChildren<DeadZone>();
    }

    private void Update()
    {
        if (_targetForCamera.transform.position.y > transform.position.y + 2)
        {
            Vector3 newPosition = new Vector3(transform.position.x, _targetForCamera.transform.position.y - 2, transform.position.z);
            transform.position = newPosition;
        }
        else if(_deadZone.IsAnimOfDeadShowing) 
        {
            Vector3 newPosition = new Vector3(transform.position.x, _targetForCamera.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 1);
        }
    }
}
