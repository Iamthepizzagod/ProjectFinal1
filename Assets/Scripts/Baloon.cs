using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour {

    Rigidbody2D baloon;
    public float speed;
    public float riseSpeed;
    public bool isOnBaloon = false;


    // Use this for initialization
    void Start () {
        baloon = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!isOnBaloon)
        {
            float translation = Time.deltaTime * 10;
            //transform.Translate(Vector2.left * speed * translation);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float translation = Time.deltaTime * 10;
            transform.Translate(Vector2.up* riseSpeed * translation);
                isOnBaloon = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnBaloon = false;
    }



}
