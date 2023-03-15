using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireState_Gun : Istate_Gun
{
    BaseWeapon GunFSM;
    Attribute_Gun Attribute_Gun;
    GameObject Bullet;
    protected int count = 1;
    protected float Angle = 30f;

    public FireState_Gun(BaseWeapon gunFSM, Attribute_Gun attribute_Gun)
    {
        GunFSM = gunFSM;
        Attribute_Gun = attribute_Gun;
    }

    public virtual void OnEnter()
    {
        int mid = count/2;
        Attribute_Gun.Ani.Play(Attribute_Gun.transform.name);
        for(int i =0;i<count;i++)
        {
            Bullet = ObjectPool.Instance.GetObject(Attribute_Gun.BulletType);
            Bullet.transform.position = Attribute_Gun.Muzzle.position;
            Bullet.GetComponent<BaseBullet>().Distance = Attribute_Gun.AttackRange;
            Bullet.GetComponent<BaseBullet>().MoveSpeed = Attribute_Gun.BulletMoveSpeed;
            if(count %2 ==0)
            {
                Bullet.GetComponent<BaseBullet>().SetDir(Quaternion.AngleAxis((i-mid)*Angle+Angle/2,Vector3.forward)*(Attribute_Gun.Target.position - Attribute_Gun.transform.position).normalized);
            }else
            {
                Bullet.GetComponent<BaseBullet>().SetDir(Quaternion.AngleAxis((i-mid)*Angle,Vector3.forward)*(Attribute_Gun.Target.position - Attribute_Gun.transform.position).normalized);
            }
        }
    }

    public virtual void OnUpdate()
    {
        if(Attribute_Gun.Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95)
        {
            GunFSM.ChangeCurrenState(State_Gun.Aim);
        }
        if(Attribute_Gun.Target == null)
        {
            GunFSM.ChangeCurrenState(State_Gun.Idel);
        }
    }
    public virtual void OnExit()
    {
        Attribute_Gun.FireTimer = Attribute_Gun.Coolingtime;//重置cd
    }
}
