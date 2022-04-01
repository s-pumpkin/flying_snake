using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SnakeHead : MonoBehaviourPunCallbacks
{
    public GameObject bodyPrefab;
    public List<GameObject> bodyList = new List<GameObject>();
    public GameObject 尾巴生成起點;
    //第一節身體的參考引用
    private Body _FirstBody;
    //最後一節身體之參考引用
    private Body _LastBody;

    //定義速度
    public float 玩家移動速度 = 10;
    public float 尾巴更新速度 = 20;//移動之速 (m/s)
    private float _Timer = 0f;

    [HideInInspector]
    public bool _IsOver = false;

    private PlayerSkill PlayerSkill;

    //身體增長一節
    private void Grow()
    {
        //先故意生成再螢幕看不到的位置 , 等觸碰到有食物Tag的物件 再位移
        GameObject obj = PhotonNetwork.Instantiate(bodyPrefab.name, new Vector3(1000f, 1000f, 1000f), Quaternion.identity) as GameObject;
        bodyList.Add(obj);

        Body b = obj.GetComponent<Body>();//取得身體 prefab上的腳本
                                          //  如果頭部後面還未有身體
        if (_FirstBody == null)
        {
            _FirstBody = b;//目前所生出的身體就是第一節身體部分
        }
        //若有最後一節身體
        if (_LastBody != null)
        {
            _LastBody.next = b;//就將新創見的身體掛後頭
        }
        _LastBody = b;//更新最後一節身體部分
    }

    void Start()
    {
        PlayerSkill = gameObject.GetComponent<PlayerSkill>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //只要遊戲還未結束 , 就繼續執行更新遊戲循環
        if (!_IsOver && GameController.instance._GameControllerStarGame)
        {
            Move();
        }
    }

    //每隔一段時間往前移動一個單位
    private void Move()
    {
        _Timer += Time.deltaTime;
        //判定當前的frame是否該移動
        if (_Timer >= (1.0f / 尾巴更新速度))
        {
            //紀錄頭部移動之前的位置
            Vector3 nextPos = 尾巴生成起點.transform.position;

            _Timer = 0f;//Reset 計時器
                        //如果有身體子部分 就讓它移動
            if (_FirstBody != null)
            {
                //讓第一節身體移動
                _FirstBody.Move(nextPos);
            }
        }
        transform.Translate(Vector3.forward * 玩家移動速度 * Time.deltaTime * PlayerSkill.更動衝刺Value);//每frame都會移動一單位
    }

    private void OnTriggerEnter(Collider other)
    {
        //若碰到食物 ,將該食物除去 ,蛇身體增加一節
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            Grow();
        }
    }
}

