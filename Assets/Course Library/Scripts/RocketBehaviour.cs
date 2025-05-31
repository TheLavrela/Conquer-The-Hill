using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    private float rocketStrenght = 15.0f;
    private float aliveTimer = 5.0f;


    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target.transform.position);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                if (targetRigidbody != null)
                {
                    Vector3 away = -col.contacts[0].normal;
                    targetRigidbody.AddForce(away * rocketStrenght, ForceMode.Impulse);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }
}
