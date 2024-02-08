using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothPaddles : MonoBehaviour
{
    // Start is called before the first frame update
    public float unitsPerSecond = 3f;
    public GameObject leftPaddle;
    public GameObject rightPaddle;

    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("LeftPaddle");
        Vector3 force = Vector3.right * horizontalValue;
        Rigidbody rb = leftPaddle.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Force);

        float verticalValue = Input.GetAxis("RightPaddle");
        Vector3 force1 = Vector3.right * verticalValue;
        Rigidbody rb1 = rightPaddle.GetComponent<Rigidbody>();
        rb1.AddForce(force1, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log($"We hit {other.gameObject.name}");
        if(other.gameObject.CompareTag("PingPong")){
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            BoxCollider bc = GetComponent<BoxCollider>();
            Bounds bounds = bc.bounds;
            float maxX = bounds.max.x;
            float minX = bounds.min.y;
            float d = maxX-minX;
            float b = other.transform.position.x;
            float pos = b/d;
            pos = pos/-0.5f;
            pos = pos/0.5f;
            pos = pos * 60;
            Quaternion rotation = Quaternion.Euler(0f, 0f, pos);
            if(this.gameObject.CompareTag("RightPaddle")){
                Vector3 bounceDirection = rotation * Vector3.up;
                Rigidbody rb = other.rigidbody;
                rb.velocity = Vector3.zero;
                rb.AddForce(bounceDirection, ForceMode.Force);
            } else if (this.gameObject.CompareTag("Paddle")) {
                Vector3 bounceDirection = rotation * Vector3.down;
                Rigidbody rb = other.rigidbody;
                rb.velocity = Vector3.zero;
                rb.AddForce(bounceDirection, ForceMode.Force);
            }
        }
    }
}
