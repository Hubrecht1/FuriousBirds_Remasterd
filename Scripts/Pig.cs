using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    Rigidbody2D Rb2D;
    [SerializeField] float maxForce;

    float momentum;

    void Awake()
    {
        Rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

    bool ShouldDieFromCollision(Collision2D collision, float _maxForce)
    {
        if (collision.rigidbody == null)
        {
#if DEBUG
        Debug.Log("No rigidbody: "+(collision.relativeVelocity.magnitude * collision.otherRigidbody.mass));
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


    private bool BirdTouched(Collision collision)
    {
        return true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision, maxForce))
        {
            Die();
        }

    }



    public void Die()
    {
        Destroy(this.gameObject);
    }

}
