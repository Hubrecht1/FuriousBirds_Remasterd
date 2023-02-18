using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] float fieldOfImpact;
    [SerializeField] float force;
    [SerializeField] float maxForceToTrigger;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] AudioSource explosion;
    [SerializeField] LayerMask layerToHit;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D collider2D;

    private void Awake()
    {

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


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (ShouldDieFromCollision(collision, maxForceToTrigger))
        {
            explode();
        }


    }

    void explode()
    {
        CameraMovement.Instance.shakeCamera(2f, 0.6f);
        explosion.pitch = Random.Range(0.9f, 1.5f);
        explosionParticles.Play();
        explosion.Play();
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        LightManager.Instance.Flash(0.2f, 0.3f);



        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
        foreach (Collider2D obj in objects)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponentInParent<Rigidbody2D>().AddForce(direction * force * distance);

        }


        Destroy(this.gameObject, explosion.clip.length);

    }






    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }

}
