using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<Player>() != null)
        {
            GameManager.instance.PlayerDied();
        }
    }
}
