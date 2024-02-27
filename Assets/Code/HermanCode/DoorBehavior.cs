using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public bool _isDoorOpen = false;
    Vector3 _doorClosePos;
    Vector3 _doorOpenPos;
    float _doorSpeed = 10f;
    // Start is called before the first frame update
    void Awake()
    {
     _doorClosePos = transform.position;
        _doorOpenPos = new Vector3(transform.position.x,
            transform.position.y + 3f, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (_isDoorOpen)
        {
            OpenDoor();

        }
    else if (!_isDoorOpen) 
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (transform.position != _doorOpenPos)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
        }
    }
    void CloseDoor()
    {
        if (transform.position != _doorClosePos)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, _doorClosePos, _doorSpeed * Time.deltaTime);
        }
    }
}
