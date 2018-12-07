using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rb;
    public Transform player;
    public float speed;
    public float projectileSpeed;
    public float followDistance;
    public float fireRate;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate () {
        float step = speed * Time.deltaTime;

        if(Vector2.Distance(player.position, transform.position) <= followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "projectile")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void Fire()
    {



    }



}
