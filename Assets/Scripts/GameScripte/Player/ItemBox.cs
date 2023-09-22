using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite ChangeImage;
    public GameObject atitem;
   // public AttackItem attackItem;
    private void Awake()
    {
        //attackItem = GetComponent<AttackItem>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            atitem.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sprite = ChangeImage;
        }
    }
}
