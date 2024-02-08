using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // public GameObject ballPrefab;
    public float speed = 5f;
    private Vector3 direction;
    public Vector3 spawnTransform;
    int LeftGoal = 0;
    int rightGoal = 0;

    public CameraShake cameraShake;


    // Start is called before the first frame update
    void Start()
    {
        spawnTransform = transform.position;
        direction = new Vector3(1f, 0.5f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Wall")){
            StartCoroutine(cameraShake.Shake(0.2f, 0.2f));
            direction.x *= -1;
        }
        if(other.gameObject.CompareTag("Paddle")){
            direction.y *= -1f;
            speed += 1f;
        }
        if(other.gameObject.CompareTag("RightPaddle")){
            direction.y *= -1f;
            speed += 1f;
        }
    }

    void OnTriggerEnter(Collider other){
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        int rand = Random.Range(0,3);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        if(other.gameObject.CompareTag("LeftGoal")){
            rightGoal++;
            if(rightGoal == 11){
                Debug.Log($"Game Over, Left Paddle Wins");
                rightGoal = 0;
                LeftGoal = 0;
            } else {
                Debug.Log($"Left Player Scored:\nScore (Left {rightGoal} : Right {LeftGoal})");
            }
            transform.position = spawnTransform;
            if(rand == 1){
                direction = new Vector3(1f, 0.5f, 0f);
            } else {
                direction = new Vector3(-1f, 0.5f, 0f);
            }
            speed = 5f;

        } else if(other.gameObject.CompareTag("RightGoal")){
            LeftGoal++;
            if(LeftGoal == 11){
                Debug.Log($"Game Over, Left Paddle Wins");
                rightGoal = 0;
                LeftGoal = 0;
            } else {
                Debug.Log($"Right Player Scored:\nScore (Left - {rightGoal} : Right - {LeftGoal})");
            }
            transform.position = spawnTransform;
            if(rand == 1){
                direction = new Vector3(1f, -0.5f, 0f);
            } else {
                direction = new Vector3(-1f, -0.5f, 0f);
            }
            speed = 5f;
        }
    }
}

