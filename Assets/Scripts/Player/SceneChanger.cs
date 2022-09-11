using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    int scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if(collision != null)
        {
            if(collision.transform.GetComponent<Player>() != null)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
