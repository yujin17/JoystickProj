using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsulecol;
    Animator anim;
    SpriteRenderer spriterd;


    public GameObject babyrock;
    public int nextmove;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsulecol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        spriterd = GetComponent<SpriteRenderer>();
        Think();
    }

    // Update is called once per frame
    
    private void Update()
    {
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //움직이기
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);

        //낭떠러지 있을시 돌기
        Vector2 frontVec = new Vector2(rigid.position.x + nextmove * 1.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            OnDamagedforDie();
        }
    }

    void Think()
    {
        //다음동작
        nextmove = Random.Range(-1, 2);

        //애니메이션

        //플립x
        if (nextmove != 0)
            spriterd.flipX = nextmove == 1;

        //재귀
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    //턴
    void Turn()
    {
        nextmove *= -1;
        spriterd.flipX = nextmove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }
    //죽음모션 
    public void OnDamagedforDie()
    {
        //Sprite Alpha
        spriterd.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriterd.flipY = true;
        //Colider Disable
        capsulecol.enabled = false;
        //create babyrock
        CreateBabyrock();
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Destroy 
        Invoke("DeActive", 5);

    }
    void DeActive()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    void CreateBabyrock()
    {
        GameObject Babyrock = Instantiate(babyrock, transform.position, transform.rotation);
        Rigidbody2D rigid = Babyrock.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);

    }
}
