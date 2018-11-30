using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public playerController player;
    private bool rightOrLeft;

	// Use this for initialization
	void Start () {
       rb = GetComponent<Rigidbody2D>();
       rightOrLeft = player.isFacingRight();
        
}

// Update is called once per frame
void FixedUpdate () {
        if (rightOrLeft)
        {
            rb.AddForce(Vector2.right*speed);
        }
        else
        {
            rb.AddForce(Vector2.left*speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "ground")
        {
            Destroy(this.gameObject);
        }
    }

}
