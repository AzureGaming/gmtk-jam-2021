using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Vector3 newPos = transform.position;

        newPos.x = playerPos.x;
        newPos.y = playerPos.y;

        transform.position = newPos;
    }
}
