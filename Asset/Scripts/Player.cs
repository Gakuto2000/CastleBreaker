using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float x, z;

    public static Player instance;
    public Animator animator;
    public bool CanAttack = true;
    //private Sprite Reborn_time;
    private float time = 0;
    //  private Sprite RebornCounter;
    //private float RebornTime;
    private Enemy hitEnemy;


    //Playerからダメージ量を受け取る時に使う
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //コンボに繋げられる時間を計測
        time += Time.deltaTime;

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        //アタック。1.2秒以内にもう一度アタックすると斬り返す。
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack == true)
            {
                animator.SetBool("Attack", true);
                time = 0;
            }
        }
        else if (animator.GetBool("Attack") && (time > 1.1f))
        {
            animator.SetBool("Attack", false);
        }

        //マイナスに移動してる時でも検知できるように絶対値
        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);

                BasicCharCont.instance.runSpeed = 4.0f;

                //PlayerWalkFootStep(WalkFootStepSE);
            }
        }
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);

            //StopFootStep();
        }

        //走る
        if (z > 0 && Input.GetKey(KeyCode.Space)) //前を向いている時だけは走る
        {
            if (!animator.GetBool("Run"))
            {
                animator.SetBool("Run", true);

                BasicCharCont.instance.runSpeed = 8.0f;
                //PlayerRunFootStep(RunFootStepSE);
            }
        }
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            BasicCharCont.instance.runSpeed = 4.0f;
            //StopFootStep();
        }

        //ガード
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Guard", true);
        }
        else
        {
            animator.SetBool("Guard", false);
        }

        //キック
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Kick", true);
        }
        else
        {
            animator.SetBool("Kick", false);
        }

        if (Input.GetKey(KeyCode.X))
        {
            Choice();
        }
        else
        {
            if (hitEnemy != null)
            {
                //hitEnemy.instance.countX = 0;
                hitEnemy.CountReset();
                hitEnemy = null;
            }
        }
    }

    public void Choice()
    {

        //当たった相手の情報を得る
        RaycastHit hitInfo;
        //outで中身は空だけど、後で返しますよという宣言になる。
        //まっすぐに当たり判定を飛ばす.距離は3
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 3))
        {
            //レーザーに当たったgameobjectがEnemyというコンポーネントを持っていたら…
            if (hitInfo.collider.gameObject.GetComponent<Enemy>() != null)
            {
                Debug.Log("Choice?");
                //正面の敵の、Enemyスクリプトの、復活関数を起動する。
                hitEnemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
                hitEnemy.CountStart();
            }

        }
        else
        {
            if (hitEnemy != null)
            {
                //なにかにヒットしないと反応しない
                hitEnemy.CountReset();
                hitEnemy = null;
            }
        }
    }
}

