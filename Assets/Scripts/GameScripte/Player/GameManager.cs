using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int Life;
    public int stageIndex = 0;
    public PlayerMove Player;

    public GameObject[] Stage;
    public GameObject rebirth;
    public GameObject gameOver;

    public Image[] UILife;  //ui-image�� ���� 
    public Text UIattackmode;
    void Update()
    {
      //UIattackmode.text = ~.Tostring()   
    }
    public void NextStage()
    {
        if (stageIndex < Stage.Length - 1)
        {
            Stage[stageIndex].SetActive(false);
            stageIndex++;
            Stage[stageIndex].SetActive(true);
            Destroy(Stage[stageIndex - 1]);
            PlayerReposition();
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("���� Ŭ����");
        }

    }
    void Start()
    {
        
    }

   
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            LifeDown();
            col.attachedRigidbody.velocity = Vector2.zero;
            col.transform.position = new Vector3(0, 0, -1);
        }
    }
    public void PlayerReposition()
    {
        Player.transform.position = new Vector3(0, 1, -1);
        Player.VelocityZero();
    }

    public void LifeDown()
    {
        Debug.Log("LifeDown ����");
        if(Player.AttackMode==true )
        {
            Player.AttackMode = false;
            UIattackmode.text = "ATTACTMod False";
        }
        else if (Player.AttackMode==false && Life > 1)
        {
            Life--;
            UILife[Life].color = new Color(1, 0, 0, 0.4f);//����ȭ �������
          
        }
       else if(Life==1)
        {
            Life--;
            UILife[Life].color = new Color(1, 0, 0, 0.4f);
            Player.Ondie();
        }
        else if(Life==0)
        {
            Player.Ondie();
        }
    }
}
