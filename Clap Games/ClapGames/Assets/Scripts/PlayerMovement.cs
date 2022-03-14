using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] cubes;
    bool key = true;

    void Update()
    {

        if (key)
        {
            Movement();
        }
        else
        {
            Movement2();
        }


    }

    void Movement()
    {
        Transform targetPos = cubes[0].transform;

        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 3f * Time.deltaTime);
        
        if(transform.position == targetPos.position)
            key = false;
    }


    void Movement2()
    {
        transform.position = Vector3.MoveTowards(transform.position, cubes[1].transform.position, 3f * Time.deltaTime);

    }

}
