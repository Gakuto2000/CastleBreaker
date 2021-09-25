using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIRotate : MonoBehaviour
{
    void LateUpdate()
    {
        //　カメラと同じ向きに設定
        this.transform.rotation = Camera.main.transform.rotation;
    }
}



