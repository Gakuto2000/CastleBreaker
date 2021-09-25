using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockon : MonoBehaviour
{
    private float RotateSpeed = 100f;
    private float Radian = 0f;
    private float HeadPoint = 1.2f;

    private SpriteRenderer AttentionSprite;
    private Enemy parentEnemy;


    // Use this for initialization
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Radian += Time.deltaTime;
        transform.Rotate(Vector3.right * Time.deltaTime * RotateSpeed);
        transform.localPosition = Vector3.up * HeadPoint + Vector3.up * (Mathf.Sin(Radian) / 5f);
        // print(Mathf.Sin(Radian));
    }
}