using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //切った時のアニメーションを取得。（蹴りとかの判定にも使う）
    //アニメーションによってダメージを変更

    //Playerから呼び出すので…
    public static Enemy instance;
    public Animator animator;
    GameObject target;

    //　敵のMaxHP
    [SerializeField]
    public int maxHp = 100;
    //　敵のHP
    [SerializeField]
    public int hp;
    //　敵の攻撃力
    //[SerializeField]
    //private int attackPower = 1;
    // private Enemy enemy;
    //　HP表示用UI
    [SerializeField]
    private GameObject HPUI;
    //　HP表示用スライダー
    private Slider hpSlider;
    //蘇生可能画像
    [SerializeField]
    private GameObject Reborn;
    /*  //ターゲティング
      [SerializeField]
      private GameObject Point;*/

    private bool Can_cure;

    //よくわからないけど、消してつけると治るので、つける。
    [SerializeField]
    private Canvas HPUI_ON;

    private bool Is_Reborning = false;
    public float countX = 0f;
    //[SerializeField]
    private Image RebornCounter;

    ////////////////////////追加・ポイント
    // private readonly float AttentionDistance = 5f;



    //無敵時間
    float Count = 0f;
    float No_damege_Time = 0.8f;

    //Playerからダメージ量を受け取る時に使う
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int damage;


    void Start()
    {
        // enemy = GetComponent<Enemy>();
        hp = maxHp;
        hpSlider = HPUI.transform.Find("HPBar").GetComponent<Slider>();
        hpSlider.value = 1f;

        target = GameObject.Find("Knight");

        //HPUIを表示する。なぜか一度消してからつけると治る。
        HPUI_ON.gameObject.SetActive(true);

        Image[] spriters = gameObject.GetComponentsInChildren<Image>();
        foreach (Image curSprite in spriters)
        {
            if (curSprite.name == "RebornCounter")
            {
                Debug.Log("みつけた");
                this.RebornCounter = curSprite;
                break;
            }
        }
        Reborn.gameObject.SetActive(false);

    }

    void Update()
    {
        //無敵時間計算
        Count += Time.deltaTime;

        if (Is_Reborning)
        {
            countX += Time.deltaTime;
            //RebornCounter.fillAmount = countX / 5;

            if (countX >= 5)
            {
                DisplayStatusUI();
            }
        }
        else
        {
            countX = 0;
        }

        //Debug.Log(RebornCounter.fillAmount + "," + countX);
        RebornCounter.fillAmount = countX / 5;


        //CountXが０にならないよ～
    }

    public void SetHp(int hp)
    {
        this.hp = hp;

        //　HP表示用UIのアップデート
        UpdateHPValue();

        if (hp <= 0)
        {
            //　HP表示用UIを非表示にする
            HideStatusUI();
        }
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    //　死んだらHPUIを非表示にする
    public void HideStatusUI()
    {
        Reborn.SetActive(true);
        HPUI.SetActive(false);
    }

    //　蘇生されたらHPUIを表示にする
    public void DisplayStatusUI()
    {
        Debug.Log("選ばれた" + this.gameObject.name);
        //Xマークが出ているとき
        if (Reborn.activeInHierarchy)
        {
            Reborn.SetActive(false);
            HPUI.SetActive(true);
            this.tag = "Player";
            Debug.Log("回復！");
            hp = maxHp;
            UpdateHPValue();
            Is_Reborning = false;
        }
    }

    public void UpdateHPValue()
    {
        hpSlider.value = (float)GetHp() / (float)GetMaxHp();
    }

    void OnTriggerEnter(Collider col)
    {
        animator = target.GetComponent<Animator>();
        // 引数はLayer番号、配列の0番目
        var clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];

        if (clipInfo.clip.name == "Attack")
            damage = 10;

        if (clipInfo.clip.name == "2ndAttack")
            damage = 20;

        //無敵時間中でなければ…
        if (Count > No_damege_Time)
        {
            hp = hp - damage;//モーションによってダメージを変更する。
            SetHp(hp);
            Count = 0;
        }
    }

    public void CountStart()
    {
        Is_Reborning = true;
    }

    public void CountReset()
    {
        //countX = 0;
        Is_Reborning = false;
    }

    /* public bool isDrawingAttention = false;

     public void Attention()
     {
         target.transform.LookAt(transform.position);
         isDrawingAttention = true;
     }

     public void UnAttention()
     {
         isDrawingAttention = false;
     }

     private void OnWillRenderObject()
     {
         // あとで記載と書いていた場所
         if (getDistance() < AttentionDistance)
         {
             Point.gameObject.SetActive(true);
         }
         else
         {
             Point.gameObject.SetActive(false);
         }
     }*/
}
