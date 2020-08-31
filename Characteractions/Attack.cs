using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    protected weaponscript[] weapons;
    private Heathscrips health;
    private Animator animator;
    private Charactercontral contral;
    // Start is called before the first frame update

    private void Awake()
    {
        weapons = GetComponentsInChildren<weaponscript>();//从子物体中获取武器
        health = GetComponent<Heathscrips>();
        animator = GetComponent<Animator>();
        contral = GetComponent<Charactercontral>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doattack(string s,float time)//动作预设的时间，和预设的触发器，这个函数交给XXcontral来主动触发动作
    {
        if (contral.GetStatenum() <= 2)
        {
            print("doii");
            if (health.cando())
                contral.doit(s, canAttack(0), time);
        }
    }




    public void Attacks(int num)//角色启用武器,武器要有编号,方便角色调用,这个是由状态机调用的
    {
        foreach (weaponscript weapon in weapons)
            if (weapon != null)
            {
                if (weapon.weaponnum == num)//调用那个指定的武器
                    weapon.attack(false);
                health.consumeqi(3);

            }
    }



    public bool canAttack(int num)
    {
        foreach (weaponscript weapon in weapons)
            if (weapon != null)
            {
                if (weapon.cooldown())//调用那个指定的武器
                    return true;

            }
        return false;
    }
}
