using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDeath : MonoBehaviour
{
    public float timeinair = 0;
    public float deathtimer = 5;
    private bool dead = false;
    private PlayerData playerData;
    private PlayerMovement playerMovement;


    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {

        if (!playerMovement.IsGrounded())
        {
            timeinair += Time.deltaTime;
        }   else
        {
            timeinair = 0;
        }
        if (timeinair >= deathtimer)
        {
            if(!dead)
            {
                playerData.TakeDamage(3);
                dead = true;

            }
            }
        }

      
}


