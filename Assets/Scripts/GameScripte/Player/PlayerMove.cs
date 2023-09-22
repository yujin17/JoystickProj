using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsulecol;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public GameManager gameManager;
    //public GameObject AttackItem;
    public GameObject Fire;
    public bool AttackMode=false;
    public int AttackCnt=0;
    public int Speed;
    public int JumPow;
    public int JumpCnt = 0;

    //for UI
    public bool move = true;
    //for sound
    SoundManager gameSound;
    public int audioJump;
    public int audioAttack;
    public int audioDamaged;
    public int audioItem;
    public int audioFinish;
    public int audioDie;

    public bool DoRebirth = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameSound = FindObjectOfType<SoundManager>();
        capsulecol = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //공격
        if(Input.GetKeyDown(KeyCode.X)&&AttackMode)
        {
            FireShot();
        }
        //점프
        if (Input.GetButtonDown("Jump"))
        {
            if (move == true)
            {
                PlayerJum();
                PlaySound("JUMP");
            }
        }
        //이동
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //방향전환 
        if (Input.GetButton("Horizontal"))
        {
            if (move == true)
            {
                PlayerFilpX();
            }
        }
        //애나메이션
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    void FixedUpdate()
    {
        Playermove();

        PlayerJumFall();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //총알
        if (col.gameObject.tag == "Enemy")
            PlayerOnDamaged(col.transform.position);
        //몬스터 에너미 공격 
        else if (col.gameObject.tag == "TurtleEnemy" || col.gameObject.tag == "RinoEnemy" || col.gameObject.tag == "RockEnemy" || col.gameObject.tag == "BabyRockEnemy" || col.gameObject.tag == "BatEnemy" || col.gameObject.tag == "WPlant")
        {
            if (rigid.velocity.y < 0 && transform.position.y > col.transform.position.y)
            {
                OnAttack(col.transform);
            }
            else //damaged
            {
                PlayerOnDamaged(col.transform.position);//충돌시 플레이어 포지션값넘김 
            }
        }
        if(col.gameObject.tag=="TutleSpike")
        {
            PlayerOnDamaged(col.transform.position);
        }

        if (col.gameObject.tag=="AttackItem")
        {
            gameManager.UIattackmode.text = "ATTACKMOD TRUE";
            AttackMode = true;
            Destroy(col.gameObject);
            PlaySound("ITEM");
        }

       
       if(col.gameObject.tag=="Life")
        {
            gameManager.UILife[gameManager.Life].color= new Color(1, 1, 1, 1f);
            gameManager.Life++;
        }
       if(col.gameObject.tag=="Rebirth")
        {
            Debug.Log("스테이지 복귀");
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            gameManager.rebirth.SetActive(false);
            gameManager.Stage[gameManager.stageIndex].SetActive(true);
            gameManager.PlayerReposition();


        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

      if(collision.gameObject.tag=="ItemBox")
        {
            //collision.gameObject.SetActive(true);
        }
      else if(collision.gameObject.tag=="Finish")
        {
            gameManager.NextStage();
        }
    }

  
    void Playermove()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > Speed)
        {
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < Speed * (-1))
        {
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
        }
    }
    void PlayerFilpX()
    {
        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        
    }
    void PlayerJum()
    {
        if (JumpCnt < 1)
            rigid.AddForce(Vector2.up * JumPow, ForceMode2D.Impulse);
        JumpCnt++;
        anim.SetBool("isJump", true);

    }

    void PlayerJumFall()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform", "ItemBox","SPlant"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                    anim.SetBool("isJump", false);
                JumpCnt = 0;
            }
        }
    }
    void FireShot()
    {
        GameObject fire = Instantiate(Fire, transform.position, Quaternion.Euler(0, 180, 0));
        Rigidbody2D rigid = fire.GetComponent<Rigidbody2D>();
        if (spriteRenderer.flipX==false )
            rigid.AddForce(Vector2.right * 15f, ForceMode2D.Impulse);
        else 
            rigid.AddForce(Vector2.left * 15f, ForceMode2D.Impulse);

        Destroy(fire, 5f);
    }

    void PlayerOnDamaged(Vector2 PlayerPos)
    {
        //목숨감소
        gameManager.LifeDown(); // 감소부터 해야  attackmode 체크한 차감으로
        //공격모드풀림
        AttackMode = false;

        //change layer 
        gameObject.layer = 3;
        //view alpa
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //보는 방향에 따른 피격시 튕기는 힘 방향조절 
        if (spriteRenderer.flipX == false)
        {
            int dirc = transform.position.x - PlayerPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        }
        else
        {
            int dirc = transform.position.x + PlayerPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        }
        Invoke("PlayerOffDamaged", 1);

    }
    void PlayerOffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }

    void OnAttack(Transform enemy)
    {
        RinoMove rinoMove = enemy.GetComponent<RinoMove>();
        TurtleMove turtleMove = enemy.GetComponent<TurtleMove>();//다른클래스
        RockMove rockmove = enemy.GetComponent<RockMove>();
        BabyRock babyRock = enemy.GetComponent<BabyRock>();
        Bat bat = enemy.GetComponent<Bat>();
        WPlant wplant = enemy.GetComponent<WPlant>();
        //Reaction Force 
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        //Enemy Die
        if (enemy.tag == "TurtleEnemy")
        {
            turtleMove.OnDamagedforDie();
        }
        else if (enemy.tag == "RinoEnemy")
        {
            rinoMove.OnDamagedforDie();
        }
        else if (enemy.tag == "RockEnemy")
        {
            rockmove.OnDamagedforDie();
        }
        else if (enemy.tag == "BabyRockEnemy")
        {
            babyRock.OnDamagedforDie();
        }
        else if (enemy.tag == "BatEnemy")
        {
            bat.OnDamagedforDie();
        }
        else if(enemy.tag=="WPlant")
        {
            wplant.OnDamagedforDie();
        }
      
        PlaySound("ATTACK");
        
       
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "JUMP":
                gameSound.Play(audioJump); //효과음
                break;
            case "ATTACK":
                gameSound.Play(audioAttack);
                break;
            case "DAMAGED":
                gameSound.Play(audioDamaged);
                break;
            case "ITEM":
                gameSound.Play(audioItem);
                break;
            case "DIE":
                gameSound.Play(audioDie);
                break;
            case "FINISH":
                gameSound.Play(audioFinish);
                break;
        }
        //audioSource.Play();

    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
    public void Ondie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //spriteRenderer.flipY = true;
        rigid.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        if (DoRebirth == false)
        {
            DoRebirth = true;
            gameManager.Stage[gameManager.stageIndex].SetActive(false);
            gameManager.PlayerReposition();
            gameManager.rebirth.SetActive(true);
        }
        else
        {
            spriteRenderer.flipY = true;
            capsulecol.enabled = false;
            
            
        }

        
    }
 
}
