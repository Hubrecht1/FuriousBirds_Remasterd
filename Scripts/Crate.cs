using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] float maxForce;
    [SerializeField] Rigidbody2D RB2D;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (ShouldDieFromCollision(collision, maxForce))
        {
            DestroyThisObject();
        }




    }

    bool ShouldDieFromCollision(Collision2D collision, float _maxForce)
    {
        if (collision.rigidbody == null)
        {
#if DEBUG
        Debug.Log("No RigidBody" + (collision.relativeVelocity.magnitude * collision.otherRigidbody.mass));
#endif

            return collision.relativeVelocity.magnitude * collision.otherRigidbody.mass > _maxForce;

        }
        else
        {
            Vector2 direction = (collision.transform.position - transform.position);
#if DEBUG
            Debug.Log("new collision method:" + ((collision.rigidbody.velocity.magnitude * collision.rigidbody.mass) - (collision.relativeVelocity.magnitude * collision.otherRigidbody.mass)));
            Debug.Log("new collision method formula:" + ((collision.rigidbody.velocity.magnitude * collision.rigidbody.mass) + "-" + (collision.relativeVelocity.magnitude * collision.otherRigidbody.mass)));
            Debug.Log(direction.normalized);    
#endif
            direction.Normalize();
            return Mathf.Abs((collision.rigidbody.velocity.magnitude * collision.rigidbody.mass) - (collision.otherRigidbody.velocity.magnitude * collision.otherRigidbody.mass)) > (direction.magnitude * _maxForce);


        }

    }
    void DestroyThisObject()
    {


        gameObject.SetActive(false);

    }
}
