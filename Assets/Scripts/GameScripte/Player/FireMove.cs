using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMove : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Platform")
        {
            rigid.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
            if(rigid.velocity.y>5)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 5);
            }
        }
        /*
        else if(collision.gameObject.tag=="Enemy")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag=="TutleSpike")
        {
            Destroy(gameObject);

        }
        else if(collision.gameObject.tag=="TurtleEnemy")
        {
            Destroy(gameObject);
        }
        */
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Border")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "TutleSpike")
        {
            Destroy(gameObject);
            
        }
        

    }
}
