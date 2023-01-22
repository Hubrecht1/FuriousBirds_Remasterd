using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    Rigidbody2D Rb2D;
    [SerializeField] float maxForce;
    [SerializeField] float maxWeightFromAbove;
    [SerializeField] float maxForceFromBird;
    [SerializeField] float maxForceFromPig;

    float momentum;

    void Awake()
    {
        Rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

    public float GetMomentum(Rigidbody2D _rb)
    {
        momentum = _rb.mass * _rb.velocity.magnitude;
        return momentum;
    }

    private bool BirdTouched(Collision collision)
    {
        return true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            Die();
        }

    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        Rigidbody2D colliderRB2D = collision.collider.GetComponent<Rigidbody2D>();

        if (colliderRB2D == null)
        {
            if (GetMomentum(Rb2D) > maxForceFromPig)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (collision.collider.gameObject.tag == "Bird" && GetMomentum(colliderRB2D) > maxForceFromBird)
        {
            return true;

        }
        else if (collision.contacts[0].normal.y < -0.5 && colliderRB2D.mass > maxWeightFromAbove)
        {
            return true;

        }
        else if (GetMomentum(colliderRB2D) > maxForce)
        {
            return true;

        }



        return false;

    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

}
