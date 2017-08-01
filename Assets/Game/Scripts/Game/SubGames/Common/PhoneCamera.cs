using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCamera : MonoBehaviour
{
    private Transform myTransform; 
//Update（毎フレーム呼ばれるfunction）の直前に呼ばれる
    void Start()
    {
        //Transformコンポーネントを取得 
        myTransform = transform;
    }
    //毎フレーム呼ばれる
    void Update()
    {
        //デバイスの倾きを取得 
        Quaternion gyro = Input.gyro.attitude;
        //回転の向きの调整 
        gyro.x *= -1.0f;
        gyro.y *= -1.0f;
        //自分の倾きとして适用 
        myTransform.localRotation = gyro;
    }


}
