using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsulecollider;
    BoxCollider2D boxcol;

    public int nextmove;
    public bool TutleSpikein = false;  // 한대 맞았나 안맞았나 표시
  
    //  public int TutleStateCount = 1;   //1 가시 0 가시 없 

    //기본움직임, 맞으면 가시벗겨짐
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
        boxcol = GetComponent<BoxCollider2D>();

     
        Think();

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

    void Think()
    {
        //다음동작
        nextmove = Random.Range(-1, 2);

        //애니메이션

        //플립x
        if (nextmove != 0)
            spriteRenderer.flipX = nextmove == 1;

        //재귀
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }


    void Turn()
    {
        nextmove *= -1;
        spriteRenderer.flipX = nextmove == 1;
        CancelInvoke();
         Invoke("Think", 5);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
     
       //죽음조건2
       if(col.gameObject.tag=="Fire"&&TutleSpikein)
        {
            OnDamagedforDie();
        }
    }

    //죽음조건2
    public void OnDamagedforDie()
    {
        if (TutleSpikein)
        {
            //Sprite Alpha
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //Sprite Flip Y
            spriteRenderer.flipY = true;
            //Colider Disable
            capsulecollider.enabled = false;
            //Die Effect Jump
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            //Destroy 
            Invoke("DeActive", 5);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
     if(col.gameObject.tag=="Fire")
        {
            boxcol.enabled = false;
            TutleSpikein = true;
            gameObject.tag = "TurtleEnemy";
            anim.SetBool("SpikeIn", true);
        }
    }
    void DeActive()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
