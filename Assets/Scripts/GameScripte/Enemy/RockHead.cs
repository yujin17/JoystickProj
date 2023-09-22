using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    Rigidbody2D rigid;
    Color color;
    // Start is called before the first frame update
    void Awake()
    {
       rigid  = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //아래방향으로 레이쏘고 지나가면 중력스케일 변경 
        RockHeadMove();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿으면 지형으로 변함
        if(collision.gameObject.tag=="Platform")
        {
            gameObject.layer = 6;
            gameObject.tag = "SideWall";
        }
    }

    void RockHeadMove()
    {
        Vector2 downVec = new Vector2(rigid.position.x, rigid.position.y - 3f);
        Vector3 DownRayLength = new Vector3(0, -7, 0);
        Debug.DrawRay(downVec, DownRayLength, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(downVec, Vector3.down, 5f, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            rigid.gravityScale = 3;
            Debug.Log("플레이어 감지");
        }
    }
}
