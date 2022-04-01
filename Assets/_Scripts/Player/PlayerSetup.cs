using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject Camera;
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<CameraCtr>().enabled = true;
            transform.GetComponent<SnakeHead>().enabled = true;
            transform.GetComponent<PlayerSkill>().enabled = true;

            UI.SetActive(true);
            Camera.GetComponent<Camera>().enabled = true;
            Camera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            transform.GetComponent<CameraCtr>().enabled = false;
            transform.GetComponent<SnakeHead>().enabled = false;
            transform.GetComponent<PlayerSkill>().enabled = false;

            UI.SetActive(false);
            Camera.GetComponent<Camera>().enabled = false;
            Camera.GetComponent<AudioListener>().enabled = false;

        }
        SetPlayerName();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SetPlayerName()
    {
        gameObject.name = photonView.Owner.NickName;
        //GameController.instance.登入名單(gameObject);
    }

    [PunRPC]
    public void 更改位置(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
