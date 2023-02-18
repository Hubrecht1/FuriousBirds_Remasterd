using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool followX, followY, enableOffset;
    Vector3 offsetX, offsetY = new Vector3(0, 0, 0);


    public void NewBird()
    {
        target = BirdManager.Instance.activeBird.transform;
    }

    private void Start()
    {
        if (enableOffset)
        {
            offsetX = transform.position - target.transform.position;
            offsetY = transform.position - target.transform.position;
        }


    }


    void Update()
    {
        if (followX)
        {
            transform.position = new Vector3(target.position.x + offsetX.x, transform.position.y, transform.position.z);
        }
        if (followY)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y + offsetY.y, transform.position.z);
        }

    }
}
