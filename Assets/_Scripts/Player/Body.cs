using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Body next;//身體的參考引用

    //促使我們目前的身體跟著前一個移動
    public void Move(Vector3 pos)
    {
        //紀錄移動前之位置
        Vector3 nextPos = transform.position;

        //先位移
        transform.position = pos;//移動目前身體
                                 //再檢查後面還有沒有身體
        if (next != null)
        {//若後面還跟著有身體
            next.Move(nextPos);//讓身體下一節移動
        }

        if(!boxCollider.enabled)
        {
            boxCollider.enabled = true;
        }
    }
}
