using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float minDistance = 0.4f;
    public float speed = 1f;

    private float _currentSpeed;
    private bool _active = false;

    void Start()
    {
        _currentSpeed = speed;
    }

    void Update()
    {
        if(_active && Vector3.Distance(transform.position, Player.Instance.transform.position) > minDistance)
        {
            _currentSpeed *= 1.1f;
            transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position, Time.deltaTime * _currentSpeed);
        }
    }

    public void SetActive()
    {
        _active = true;
    }
}
