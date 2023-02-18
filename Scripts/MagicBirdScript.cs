using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBirdScript : MonoBehaviour
{
    [SerializeField] Bird thisBird;
    [SerializeField] Rigidbody2D rigidbody2D;

    private void Awake()
    {
        thisBird = gameObject.GetComponent<Bird>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        thisBird.abilityActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && thisBird.fired && thisBird.abilityActivated == false)
        {
            rigidbody2D.gravityScale = 0;

            thisBird.abilityActivated = true;

        }
        else if (thisBird.firstCollision)
        {
            thisBird.abilityActivated = true;
        }




    }
}
