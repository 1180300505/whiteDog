using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySight : MonoBehaviour
{
    private bool insight;//是否看到了敌人
    private Transform target;//看到的目标

    // 这个函数将会返回是否看到敌人 和 是否踩在了地面上
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Heathscrips health = collision.gameObject.GetComponent<Heathscrips>();
        if (health != null)
        {
            if (health.isenemay != gameObject.GetComponentInParent<Heathscrips>().isenemay)
            {
                insight = true;
                target = collision.transform;
            }
        }
        else
        {
            insight = false;
            target = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Heathscrips health = collision.gameObject.GetComponent<Heathscrips>();
        if (health != null)
            if (health.isenemay != gameObject.GetComponentInParent<Heathscrips>().isenemay)
            {
                insight = true;
                target = null;
            }
    }

    public Transform targets()
    {
        return target;
    }

    public bool insights()
    {
        return insight;
    }

    public bool isground()
    {
        return GetComponentInChildren<CheckGround>().isground();//返回是否前方是中空
    }

}
