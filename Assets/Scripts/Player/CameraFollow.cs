using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    Transform target;

    [SerializeField]
    float xOffset;
    [SerializeField]
    bool freezeYaxis;

    [SerializeField]
    float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Vector3.zero;
        if(freezeYaxis)
        {
            targetPosition = new Vector3(target.position.x + xOffset, transform.position.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(target.position.x + xOffset, target.transform.position.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
    }
}
