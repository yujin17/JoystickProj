using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoMove : MonoBehaviour
{
    //  public GameObject player;
    Transform target = null;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsulecollider;
    Animator anim;
  
    public int nextmove;
    float enemyMoveSpeed = 4f;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
       Invoke("Think", 5);
    }

    void Update()
    {
        //박스 콜라이더 내 플레이어 감지 된다면
        if (target != null)
        {
            Vector2 dir = target.position - transform.position; 
            if (dir.x > 0) //플레이어가 라이노보다 앞이면
            {
                spriteRenderer.flipX = true;
                //이동시킴 
                transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);

            }
            else //나머지 
            {
                spriteRenderer.flipX = false;
                transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);

            }
        }
        //걷거나 가만히잉ㅆ거나 애니메이션 
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
        Vector2 frontVec = new Vector2(rigid.position.x +nextmove*1.5f, rigid.position.y);
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
    //트리거에 플레어 들어오면 타겟에 플레이어 저장 
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag =="Player")
        {
            target=col.gameObject.transform;  //태그가 플레이어면 콜라이션의 게임오브젝트를 타겟에저장 
            Debug.Log("rino found target");
        }
    }

    //트리거에 나가면 타겟을 비움.
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
        CancelInvoke();
        Think();
        Debug.Log("rino lost target");
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
        //Invoke("Think", 5);
    }

    public void OnDamagedforDie()
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
    void DeActive()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }


    

     
       
    
}
