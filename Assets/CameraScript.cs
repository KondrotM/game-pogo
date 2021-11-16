using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float followSpeed = 0.1f; // 0.01

    [SerializeField] private bool follow = true;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MouseMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + offset, followSpeed);
        }
        else
        {
            // Snappy camera script goes here.
            /*
            if (player.position.x > transform.position.x + 13)
            {
                transform.position =
                    new Vector3(transform.position.x + 26f, transform.position.y, transform.position.z + offset.z);
            }
            
            if (player.position.x < transform.position.x - 13)
                        {
                            transform.position =
                                new Vector3(transform.position.x - 26f, transform.position.y, transform.position.z + offset.z);
                        }
                        */
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.position.x > transform.position.x)
        {
            transform.position =
                new Vector3(transform.position.x + 25f, transform.position.y, transform.position.z + offset.z);
        }
        else
        {
            transform.position =
                new Vector3(transform.position.x - 25f, transform.position.y, transform.position.z + offset.z);
        }
    }
}
