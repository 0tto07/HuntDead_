using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float LaunchForce;
    public GameObject Arrow;

    [SerializeField] Transform theRotationOfTheBow;
    [SerializeField] Transform thePLayerPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = thePLayerPosition.position;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        void Shoot()
        {

            GameObject ArrowIns = Instantiate(Arrow, transform.position, theRotationOfTheBow.rotation);
            ArrowIns.GetComponent<Rigidbody2D>().AddForce(-transform.right * LaunchForce);
        }
    }
}
