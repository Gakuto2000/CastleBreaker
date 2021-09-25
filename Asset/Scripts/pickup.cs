using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private RaycastHit hitInfo;
    public AudioSource playerAS;

    public AudioClip BreakShoji;
    public AudioClip OpenShoji;
    public AudioClip CloseShoji;
    public bool IsClose = true;

    public AudioClip BreakCup;
    public AudioClip Item;
    public AudioClip Stair;
    public Player player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //outで中身は空だけど、後で返しますよという宣言になる。
        //まっすぐに当たり判定を飛ばす.距離は3
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 3))
        {
            //レーザーに当たったgameobjectがEnemyというコンポーネントを持っていたら…
            if (hitInfo.collider.gameObject.tag == "Door")
            {
                playerAS.clip = BreakShoji;//ブレイク障子が邪魔でオープンできない。
                if (Input.GetKeyDown(KeyCode.O))
                {
                    Opendoor(hitInfo);
                }
            }
            if (hitInfo.collider.gameObject.tag == "Cup")
            {
                playerAS.clip = BreakCup;
            }
            if (hitInfo.collider.gameObject.tag == "Item")
            {
                player.CanAttack = false;
                playerAS.clip = Item;
            }
            if (hitInfo.collider.gameObject.tag == "Stair")
            {
                player.CanAttack = false;
                playerAS.clip = Stair;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Invoke("attack", 0.4f);
            }
        }
    }

    void attack()
    {
        player.CanAttack = true;
        playerAS.Play();
    }

    void Opendoor(RaycastHit parent)
    {
        //子オブジェクトのアニメーションを管理

        Animator[] animators = parent.collider.gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator curAnimation in animators)
        {
            if ((curAnimation.gameObject.name == "Ldoor") || (curAnimation.gameObject.name == "Rdoor"))
            {
                if (IsClose)
                {
                    curAnimation.SetBool("open", true);
                }
                else
                {
                    curAnimation.SetBool("open", false);

                    //逆再生のためにスピードを-1、スタートを1に設定。
                    curAnimation.SetFloat(Animator.StringToHash("speed"), -1);
                    if (curAnimation.name == "Rdoor")
                        curAnimation.Play("Rdoor 0", 0, 1f);
                    if (curAnimation.name == "Ldoor")
                        curAnimation.Play("Ldoor 0", 0, 1f);
                }
            }
        }
        IsClose = !IsClose; //開閉の状態を変更
    }
}
