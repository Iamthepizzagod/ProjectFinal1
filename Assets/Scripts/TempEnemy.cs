using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rb;
    
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "projectile")
        {
            Destroy(this.gameObject);
        }
    }
}
