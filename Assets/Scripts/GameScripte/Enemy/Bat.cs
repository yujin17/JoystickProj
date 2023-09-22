using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    SpriteRenderer spriterd;
    CapsuleCollider2D capsulecol;

    private PlayerMove player;
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public bool playerInRange;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        capsulecol = GetComponent<CapsuleCollider2D>();
        spriterd = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
        if (playerInRange)
        {
            anim.SetBool("isFly", true);  
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        if(player.transform.position.x>transform.position.x)
            spriterd.flipX = true;
        else
            spriterd.flipX = false;
        
            
    }

     void OnDrawGizmosSelected()
    {//
        //중심, 반지름 
        //Gizmos.DrawSphere(transform.position, playerRange);
    }

    public void OnDamagedforDie()
    {

        //Sprite Alpha
        spriterd.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriterd.flipY = true;
        capsulecol.enabled = false;
        Invoke("BatDie", 0.5f);

    }
    void BatDie()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            OnDamagedforDie();
        }
    }
}
