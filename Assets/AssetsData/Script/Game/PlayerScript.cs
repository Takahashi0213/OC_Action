using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //オブジェクト
    private GameObject GameCameraObject;
    private Rigidbody rb;
    private Animator animator;
    private GameObject emptyObject;
    private PlayerSound ps;
    private AudioSource Drop_as;
    private PauseScript pauseScript;

    //パラメータ
    [Header("移動速度")]
    public float MoveSpeed = 0.0f;
    [Header("ジャンプ力")]
    public float JumpPower = 5.0f;

    private bool PlayerStopFlag = false;   //プレイヤー停止フラグ

    private bool DeathFlag = false;
    private bool DeathAnimStopFlag = false;

    private bool JumpFlag = false;
    private bool IsGround = false;
    private bool IsJump = false;

    [Header("無敵時間")]
    public float Invincible = 2.0f;
    private float invincibleTime = 2.0f;
    private bool InvincibleFlag = false;

    [Header("ミス判定になる低さ")]
    public float MissBorder_Y = -50.0f;
    private Vector3 RestartPosition = Vector3.zero;

    //無敵演出
    private GameObject PlayerModelObject;
    private float mutekiEffectTimer = 0.0f;
    private float mutekiEffectLimit = 0.1f; //無敵演出切り替えの間隔
    private bool mutekiEffectFlag = false;        //無敵演出

    private int PlayerHP = 3;
    private int MaxHP = -1;

    private GameObject HitObject;

    //ダメージエフェクト
    private GameObject DamageEffectObject;
    private ParticleSystem Damage_particleSystem;

    //アニメーター
    int moveParamHash = -1;
    int jumpParamHash = -1;
    int deathParamHash = -1;
    int clearParamHash = -1;

    //定数
    private const float MoveDiv = 100.0f;   //移動べクトルをこれで割る

	void Start()
    {
        RestartPosition = transform.position;
        MaxHP = PlayerHP;

        GameCameraObject = GameObject.Find("MainCamera");
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        ps = this.GetComponent<PlayerSound>();
        PlayerModelObject = transform.GetChild(0).gameObject;   //自分の子オブジェクトをモデル用取得
        DamageEffectObject = transform.GetChild(1).gameObject;  //自分の子オブジェクトをダメージエフェクトとして取得
        Damage_particleSystem = DamageEffectObject.GetComponent<ParticleSystem>();
        Drop_as = GetComponent<AudioSource>();
        pauseScript = GameObject.Find("GodObject").GetComponent<PauseScript>();

        //ハッシュ
        moveParamHash = Animator.StringToHash("MovePower");
        jumpParamHash = Animator.StringToHash("JumpFlag");
        deathParamHash = Animator.StringToHash("MissFlag");
        clearParamHash = Animator.StringToHash("ClearFlag");
    }

    void LateUpdate()
    {
        if (pauseScript.IsPause())
        {
            return;
        }
        if (DeathFlag)
        {
            PlayerDeath();
            return;
        }

        if (PlayerStopFlag)
        {
            return;
        }

        //カメラを考慮した移動
        Vector3 PlayerMove = Vector3.zero;
        Vector3 stickL = Vector3.zero;

        stickL.z= Input.GetAxis("Vertical");
        stickL.x= Input.GetAxis("Horizontal");

        Vector3 forward = GameCameraObject.transform.forward;
        Vector3 right = GameCameraObject.transform.right;
        forward.y = 0.0f;
        right.y = 0.0f;

        right *= stickL.x;
        forward *= stickL.z;

        //移動速度に上記で計算したベクトルを加算する。
        PlayerMove += right + forward;

        //移動させる
        transform.position += PlayerMove * 4.0f * MoveSpeed * Time.deltaTime;

        animator.SetFloat(moveParamHash, Mathf.Abs(PlayerMove.sqrMagnitude * 1000000.0f));

        //回転
        if (PlayerMove.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(PlayerMove.normalized);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }

        //接地チェック
        IsGround = CheckGround();

        //ジャンプ
        if ((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space)) && IsGround == true)
        {
            Jump(JumpPower);
            //ジャンプ効果音
            ps.SE_Play_Jump(PlayerSound.PlaySE.Jump, GameCameraObject.transform.position);
        }

        if (IsGround == true && JumpFlag == true)
        {
            animator.SetBool(jumpParamHash, false);
            IsJump = false;
            JumpFlag = false;
        }
        if(IsGround == false)
        {
            JumpFlag = true;
        }

        //ジャンプアニメーション強制終了
        if (IsJump && IsGround == true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

            if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName("Jump"))
            {
                //強制終了！
                animator.SetBool(jumpParamHash, false);
                IsJump = false;
                JumpFlag = false;
            }
        }

        //判定
        if (HitObject != null)
        {
            if (transform.parent == null && HitObject.CompareTag("MoveGround"))
            {
                emptyObject = new GameObject();
                emptyObject.transform.parent = HitObject.gameObject.transform;
                transform.parent = emptyObject.transform;
            }
        }
        else
        {
            if (transform.parent != null)
            {
                transform.parent = null;
                Destroy(emptyObject);
            }
        }

        //無敵処理
        PlayerInvincible();

        //落下チェック
        if (MissBorder_Y > transform.position.y)
        {
            Damage();
            if (PlayerHP > 0)
            {
                transform.position = RestartPosition;
                transform.rotation = Quaternion.identity;
                Drop_as.Play(); //落下SEは特例
            }
        }

        //死亡チェック
        if (PlayerHP <= 0 && DeathFlag == false)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            PlayerModelObject.SetActive(true);
            animator.SetBool(deathParamHash, true); //やられアニメーション再生
            animator.Play("Miss", 0, 0);
            animator.Update(0);
            GameObject.Find("GodObject").GetComponent<GameOverScript>().SetGameOver();
            DeathFlag = true;
        }

    }

    bool CheckGround()
    {
        var ray = new Ray(transform.position + Vector3.up * 0.01f, Vector3.down);
        var distance = 0.2f;

        RaycastHit raycastHit;  //レイがヒットしたオブジェクトの情報を格納
        bool HitFlag = Physics.Raycast(ray, out raycastHit, distance);

        if (HitFlag)
        {
            HitObject = raycastHit.collider.gameObject;

            if(HitObject.CompareTag("MoveGround") || HitObject.CompareTag("Ground") || HitObject.CompareTag("CannonGround"))
            {
                HitFlag = true;
            }
            else
            {
                HitFlag = false;
            }
        }
        else
        {
            HitObject = null;
        }

        return HitFlag;
    }

    //無敵処理
    private void PlayerInvincible()
    {
        if (InvincibleFlag == false)
        {
            return; //無敵時間でないなら中断
        }

        //演出
        mutekiEffectTimer -= Time.deltaTime;
        if (mutekiEffectTimer <= 0.0f)
        {
            mutekiEffectTimer = mutekiEffectLimit;
            mutekiEffectFlag = !mutekiEffectFlag;   //フラグを逆にする
            if (mutekiEffectFlag)
            {
                PlayerModelObject.SetActive(true);
            }
            else
            {
                PlayerModelObject.SetActive(false); //プレイヤーを透明にする
            }
        }

        invincibleTime -= Time.deltaTime;
        if (invincibleTime <= 0.0f)
        {
            PlayerModelObject.SetActive(true);
            rb.velocity = Vector3.zero;
            InvincibleFlag = false;
        }

    }

    private void PlayerDeath()
    {
        if (DeathAnimStopFlag == false)
        {
            //アニメーションの再生位置取得（もっといい書き方ないの？）
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
            float myTime = myAnimatorClip[0].clip.length * info.normalizedTime;
            //途中で止める
            if (myTime >= 1.0f)
            {
                DeathAnimStopFlag = true;
                animator.enabled = false;
            }
        }
    }

    public void Jump(float Y_power)
    {
        animator.Update(0);
        rb.AddForce(new Vector3(0.0f, Y_power, 0.0f), ForceMode.VelocityChange);
        animator.SetBool(jumpParamHash, true);
        JumpFlag = false;
        IsJump = true;
    }

    //ジャンプアニメーションを最初からにする
    public void PlayJumpAnim()
    {
        animator.Play("Jump", 0, 0);
    }

    public void Damage()
    {
        if (InvincibleFlag)
        {
            return; //無敵時間中なら中断
        }
        PlayerHP--;
        PlayerHP = Mathf.Max(PlayerHP, 0);  //最低値は0
        invincibleTime = Invincible;        //無敵時間設定
        mutekiEffectTimer = mutekiEffectLimit;
        mutekiEffectFlag = false;
        PlayerModelObject.SetActive(false); //プレイヤーを透明にする
        InvincibleFlag = true;
        Damage_particleSystem.Play();  //ダメージエフェクトを再生
        if (PlayerHP > 0)
        {
            //まだ体力あるならダメージ効果音
            ps.SE_Play_Jump(PlayerSound.PlaySE.Damage, GameCameraObject.transform.position);
        }
    }

    public void PlayerRecovery()
    {
        PlayerHP++;
        PlayerHP = Mathf.Min(PlayerHP, MaxHP);
    }

    public void PlayerClear()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        PlayerModelObject.SetActive(true);
        animator.SetBool(clearParamHash, true); //クリアアニメーション再生
        animator.Play("Clear", 0, 0);
        animator.Update(0);
        PlayerStopFlag = true; //プレイヤーを停止する
    }

    //取得

    public int GetPlayerHP()
    {
        return PlayerHP;
    }

    public bool GetDeathFlag()
    {
        return DeathFlag;
    }

}
