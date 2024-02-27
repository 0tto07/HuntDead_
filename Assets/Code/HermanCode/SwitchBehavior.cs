using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{
    [SerializeField] DoorBehavior _doorBehavior;

    [SerializeField] bool _isDoorOpenSwitch;
    [SerializeField] bool _isDoorCloseSwitch;

    float _switchSizeY;
    Vector3 _switchDownPos;
    Vector3 _switchUpPos;
    float _switchSpeed;
    float _switchDealy = 0.2f;
    bool _IsPressingSwitch = false;

    // Start is called before the first frame update
    void Awake()
    {
        _switchSizeY = transform.localScale.y / 2;

        _switchUpPos = transform.position;
        _switchDownPos = new Vector3(transform.position.x,
        transform.position.y + 3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsPressingSwitch)
        {
            MoveSwitchDown();

        }
        else if (!_IsPressingSwitch)
        {
            MoveSwitchUp();
        }

        void MoveSwitchDown() {
            if (transform.position != _switchDownPos)
            {
                transform.position = Vector3.MoveTowards
                    (transform.position, _switchDownPos, _switchSpeed * Time.deltaTime);
            }
        }

        void MoveSwitchUp()
        {
            if (transform.position != _switchUpPos)
            {
                transform.position = Vector3.MoveTowards
                    (transform.position, _switchUpPos, _switchSpeed * Time.deltaTime);
            }
        }
   }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
        _IsPressingSwitch = ! _IsPressingSwitch;

            if (_isDoorOpenSwitch && _doorBehavior._isDoorOpen)
            {
                _doorBehavior._isDoorOpen = !_doorBehavior._isDoorOpen;
            }
            else if (_isDoorCloseSwitch && _doorBehavior._isDoorOpen)
            {
                _doorBehavior._isDoorOpen = !_doorBehavior._isDoorOpen;
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchUpDealy(_switchDealy));
        }
    }
    IEnumerator SwitchUpDealy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _IsPressingSwitch = false;

    }
}

