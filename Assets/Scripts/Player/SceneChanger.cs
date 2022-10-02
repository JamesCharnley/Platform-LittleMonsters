using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip winSound;
    [SerializeField]
    int scene;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if(collision != null)
        {
            if(collision.transform.GetComponent<Player>() != null)
            {
                audioSource.clip = winSound;
                audioSource.Play();
                GameManager.instance.EndOfLevel();
            }
        }
    }
}
