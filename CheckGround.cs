using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    // 这个类就是判断前方是否在地面上

    private int groundCount = 0;
    private int characterCount = 0;
    private Transform ground;
    void OnTriggerEnter2D(Collider2D collider)//判断是否落地
    {
        if (collider.gameObject.tag == "ground")
        {
            groundCount++;
            ground = collider.gameObject.transform;
        }
        if (collider.gameObject.tag == "character")
        {
            characterCount++;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "ground")
        {
            groundCount--;
        }
        if (collider.gameObject.tag == "character")
        {
            characterCount--;
        }

    }

    public bool isground()
    {
        return groundCount > 0;
    }
    public bool isgroundcharater()//判断是否落在角色头上
    {
        return characterCount > 0;
    }

    public Transform groundnow()
    {
        return ground;
    }
}
