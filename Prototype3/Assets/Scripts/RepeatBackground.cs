/*
* Chris Smith
* Prototype 3
* Manages background animation
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        //save start position as Vector3
        startPos = transform.position;

        //set repeatWidth to half width of background
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if background is further left than repeatWidth, reset it to startPos
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
