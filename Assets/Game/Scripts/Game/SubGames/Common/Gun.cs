using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using System;

/// <summary>
/// 枪
/// </summary>
public class Gun
{
    public GameObject gunObj { get; }   //枪模型
    public GameObject bullet { get; }    //子弹
    private Queue<GameObject> _bulletQue;
    private Transform bulletP;
    public Gun(GameObject gunobj, GameObject bullet)
    {
        gunObj = gunobj;
        this.bullet = bullet;
        if (bulletP == null)
            bulletP = new GameObject("bulletP").transform;
        bulletP.SetParent(bullet.transform.parent);
        bulletP.localScale = Vector3.one;
        bulletP.localPosition = Vector3.zero;
        this.bullet.transform.SetParent(bulletP);
        _bulletQue = new Queue<GameObject>(10);
        InitBulletQue(10);
    }
    void InitBulletQue(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject oo = GameObject.Instantiate<GameObject>(bullet);
            oo.transform.position = bullet.transform.position;
            oo.transform.rotation = bullet.transform.rotation;
            oo.transform.SetParent(bulletP);
            oo.transform.localScale = bullet.transform.localScale;
            oo.SetActive(false);
            _bulletQue.Enqueue(oo);
        }
    }
    GameObject getBullet()
    {
        bool flag = true;
        GameObject b = null;
        if (_bulletQue.Count != 0)
            b = _bulletQue.Dequeue();
        else
        {
            b = GameObject.Instantiate(bullet) as GameObject;
            b.transform.SetParent(bulletP);
            b.transform.localScale = bullet.transform.localScale;
            flag = false;
        }
        b.SetActive(true);
        b.transform.position = bullet.transform.position;
    //    b.transform.eulerAngles = bullet.transform.eulerAngles;
        b.transform.localEulerAngles = bullet.transform.localEulerAngles;
        //子弹飞完后消失 重新排队
        AppFacade.Ins.gMono.StartCoroutine(GlobalMono.DelaySometimeExcute(5, oo =>
        {
            if (oo == null) return;
            GameObject obj = oo as GameObject;
            if (obj.GetComponent<Rigidbody>() != null) GameObject.Destroy(obj.GetComponent<Rigidbody>());
            obj.transform.position = bullet.transform.position;
            obj.SetActive(false);
            if (flag)
                _bulletQue.Enqueue(obj);
            else
                GameObject.Destroy(obj);
        }, b));
        return b;
    }
    public void Shut()
    {
        Rigidbody rig = getBullet().GetComponentIfNoExist<Rigidbody>();
        rig.useGravity = false;
        rig.AddForce(gunObj.transform.forward * 60f * Time.deltaTime, ForceMode.Impulse);
      //  rig.AddForce()
    }
}
