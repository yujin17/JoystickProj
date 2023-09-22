using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlant : MonoBehaviour
{
    //��ũ��Ʈ 2������ - �浹�� ���Ͼ���.
    Rigidbody2D rigid;
    SpriteRenderer spriterd;
    public GameObject bullet;
    // Start is called before the first frame update
    void Awake()
    {
        spriterd = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Fire", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void Fire()
    {
        GameObject Bullet;
        
        Bullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigid = Bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer bspriterd = Bullet.GetComponent<SpriteRenderer>();
        if (spriterd.flipX == false)
        {
            rigid.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        }
        else
        {
            //�Ѿ� ������ ��缳�� 
            bspriterd.flipX = true;
            rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        }
        Destroy(Bullet, 2);
        Invoke("Fire", 2);
    }


}
