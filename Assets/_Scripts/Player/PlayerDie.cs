using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDie : MonoBehaviourPunCallbacks
{
    SnakeHead SnakeHead;
    PlayerSkill PlayerSkill;
    // Start is called before the first frame update
    void Start()
    {
        SnakeHead = gameObject.GetComponent<SnakeHead>();
        PlayerSkill = gameObject.GetComponent<PlayerSkill>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (GameController.instance._GameControllerStarGame)
        {
            //若碰到邊界 遊戲結束
            if (other.CompareTag("Bound") && photonView.IsMine)
            {
                //SnakeHead._IsOver = true;
                GameController.instance.LeaveRoom();
                return;
            }

            if (other.CompareTag("Player") || other.CompareTag("Body") && !PlayerSkill.is無敵 && photonView.IsMine)
            {
                //SnakeHead._IsOver = true;
                GameController.instance.LeaveRoom();
                return;
            }
        }
    }
}
