using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 30;

    [Range(1, 100)]
    [SerializeField] private float _lerpSpeed = 10;

    private Vector3 _initPosition;
    private Vector2 _initMousePosition;
    private Vector2 _currentMousePosition;
    private Vector2 _distance;
    void Start()
    {
        _initPosition = transform.position;
    }
    void Update()
    {
        MoveMap();
    }
    void MoveMap()
    {
        //if(GameManager._instance.c)

        if (Input.GetMouseButtonDown(1))
        {
            _initMousePosition = Input.mousePosition;
            _initPosition = transform.position;
        }
        if (Input.GetMouseButton(1))
        {
            _currentMousePosition = Input.mousePosition;
            //grab distance
            _distance = _currentMousePosition - _initMousePosition;
        }
        //Move camera
        transform.position = Vector3.Lerp(transform.position, _initPosition - (Vector3)_distance * _moveSpeed / 1000, _lerpSpeed / 1000);
    }
}
