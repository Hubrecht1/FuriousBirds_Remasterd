using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Bird")]
    [SerializeField] float force;
    [SerializeField] float maxdragDistance = 2;

    Rigidbody2D Rb2D;
    Collider2D C2D;
    public bool abilityActivated = false;

    [HideInInspector] public Vector2 _startPosition;
    [HideInInspector] public bool fired = false;

    public bool firstCollision = false;



    private void Awake()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        C2D = GetComponent<Collider2D>();

    }


    void Start()
    {
        _startPosition = Rb2D.position;
        Rb2D.isKinematic = true;

    }



    public float GetMomentum(Collision2D collision)
    {

        return collision.relativeVelocity.magnitude * collision.rigidbody.mass;
    }




    private void OnMouseUp()
    {


        if (!fired)
        {

            Vector2 currentPosition = Rb2D.position;
            float distance = Vector2.Distance(currentPosition, _startPosition);
            Vector2 direction = _startPosition - currentPosition;
            direction.Normalize();
            Rb2D.isKinematic = false;
            Rb2D.AddForce(direction * force * distance);

            fired = true;


        }


    }


    private void OnMouseDrag()
    {
        if (fired == false)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 desiredPosition = mousePosition;

            float angle = Mathf.Atan2(_startPosition.y - transform.position.y, _startPosition.x - transform.position.x) * Mathf.Rad2Deg;

            if (transform.position.x > _startPosition.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, -angle + 180);

            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            float distance = Vector2.Distance(desiredPosition, _startPosition);

            if (distance > maxdragDistance)
            {
                Vector2 direction = desiredPosition - _startPosition;
                direction.Normalize();
                desiredPosition = _startPosition + (direction * maxdragDistance);


            }



            Rb2D.position = desiredPosition;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (fired)
        {
            firstCollision = true;
            this.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        if (!firstCollision && fired)
        {
            Vector2 direction = Rb2D.velocity;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (Rb2D.velocity != Vector2.zero)
            {
                if (Rb2D.velocity.x > 0)
                {
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, -angle + 180);
                }

            }
        }
    }

    bool IsBetween(float value, float bound1, float bound2)
    {
        return value >= bound1 && value <= bound2;
    }

}
