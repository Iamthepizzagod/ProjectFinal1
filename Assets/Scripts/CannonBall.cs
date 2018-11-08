using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    Rigidbody2D ball;
    public float speed;
    public float sinkSpeed;
    public bool isOnBall = false;

    // Use this for initialization
    void Start () {
        ball = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!isOnBall)
        {
            float translation = Time.deltaTime * 10;
            transform.Translate(Vector2.left * speed * translation);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float translation = Time.deltaTime * 10;
            transform.Translate(Vector2.down * sinkSpeed * translation);
            isOnBall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnBall = false;
    }

}
