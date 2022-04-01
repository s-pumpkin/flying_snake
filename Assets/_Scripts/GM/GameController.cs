using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviourPunCallbacks
{
    private static GameController _instance;

    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }

    public GameObject 預載座標;

    public GameObject[] Player;
    //public List<GameObject> Player = new List<GameObject>();
    public GameObject[] 出生點;
    public GameObject playerPrefab;
    [HideInInspector]
    public bool _GameControllerStarGame = false;

    //private List<PhotonView> pv = new List<PhotonView>();
    public PhotonView pv;
    public Text 倒數text;
    public int _倒數數值 = 5;
    public string 遊戲開始字;

    [HideInInspector]
    public bool 縮圈 = false;
    public GameObject 電網;
    public Vector3 最後收縮的範圍大小;
    public float Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            {
                Vector3 pos = 預載座標.transform.position;
                PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
            }
        }


        //IsMasterClient 主端處理邏輯
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("---------------------------------------主端處理邏輯---------------------------------------");
            Invoke("分隊", 1.5f);
            Invoke("遊戲倒數開始", 3f);
        }
    }

    /*
    public void 登入名單(GameObject PlayerGameObject)
    {
        Player.Add(PlayerGameObject);
        Debug.Log(PlayerGameObject.name);

        int i = Player.IndexOf(PlayerGameObject);
        Debug.Log("第幾位:  " + i);
    }
    public void 分隊()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            if (Player[i] != null)
            {
                PhotonView PlayerPV = Player[i].gameObject.GetComponent<PhotonView>();
                Debug.Log(gameObject.name + "+" + "分隊");
                PlayerPV.RPC("更改位置", RpcTarget.All, 出生點[i].transform.position);
            }
        }
    }
    */
    public void 分隊()
    {
        Player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Player.Length; i++)
        {
            if (Player[i] != null)
            {
                PhotonView PlayerPV = Player[i].gameObject.GetComponent<PhotonView>();
                Debug.Log(gameObject.name + "+" + "分隊");
                PlayerPV.RPC("更改位置", RpcTarget.All, 出生點[i].transform.position);
            }
        }
    }

    public void 遊戲倒數開始()
    {
        pv.RPC("倒數", RpcTarget.All);
        if (_倒數數值 < 0)
        {
            縮圈 = true;
            return;
        }
        Invoke("遊戲倒數開始", 1f);
    }

    [PunRPC]
    public void 倒數()
    {
        if (_倒數數值 == 0)
        {
            倒數text.text = 遊戲開始字;
            _GameControllerStarGame = true;
            Invoke("空白字", 1f);
            _倒數數值 -= 1;
            return;
        }
        倒數text.text = _倒數數值.ToString();
        _倒數數值 -= 1;
    }

    public void 空白字()
    {
        倒數text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        開始縮圈();
        Player = GameObject.FindGameObjectsWithTag("Player");
        if (Player.Length == 1 && _GameControllerStarGame)
        {
            倒數text.text = "You Win!";
            _GameControllerStarGame = false;
        }
    }

    public void 開始縮圈()
    {
        if (縮圈)
        {
            if (電網.transform.localScale != 最後收縮的範圍大小)
            {
                電網.transform.localScale = Vector3.MoveTowards(電網.transform.localScale, 最後收縮的範圍大小, Speed * Time.deltaTime);
            }
            else
            {
                縮圈 = false;
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name +
            " 人數：" + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Main");
    }


}
