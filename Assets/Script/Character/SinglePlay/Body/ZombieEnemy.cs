using UnityEngine;
using System.Collections;

public class ZombieEnemy : MonoBehaviour {

    private GameModeState ModeState;
    private GameState State;

    private AnimationState ZombieAniState;

    private AudioSource Audio;
    public AudioClip DeadSound;

    // 충돌체 정보
    //private SphereCollider sp_Col;
    private CapsuleCollider cap_Col;

    // 적의 움직임 여부
    private bool NowMoveOn;
    // 데미지 틱(Tick)
    private bool AttackChecker;
    // 데드 아이 체크
    private bool DeadEyeChecker;
    // 길찾기 체크
    private bool WalkPathChecker;
    private bool DeadEyeInSight;

    // 코루틴 정보들
    private IEnumerator DamegeCoroutine;
    private IEnumerator DeadCoroutine;
    private IEnumerator AttackCoroutine;
    private IEnumerator WalkPathCoroutine;

    public GameObject Zombie_Object;
    public GameObject Zombie_Navi;
    public GameObject Player_Ojbect;

    //private Vector3 NaviBackPos;
    public Transform ObjectBackPos;
    

    private int HP;

	// Use this for initialization
	void Start () {

        State = GameManager.NowGameState;
        ModeState = GameManager.NowGameModeState;

        //if (sp_Col == null)
        //{
        //    sp_Col = GetComponent<SphereCollider>();
        //    sp_Col.enabled = true;
        //}
        //else
        //{
        //    sp_Col.enabled = true;
        //}

        if (cap_Col == null)
        {
            cap_Col = GetComponent<CapsuleCollider>();
            cap_Col.enabled = true;
        }
        else
        {
            cap_Col.enabled = true;
        }

        if (Player_Ojbect == null)
        {
            Player_Ojbect = GameObject.FindWithTag("Player");
        }

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;

           //Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = DeadSound;
        }
        else
        {
            Audio.clip = DeadSound;
        }

        

        //NaviChecker = false;

        // 애니메이션 방향
        if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
        {
            ZombieAniState = AnimationState.LEFTSTAND;
        }
        else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
        {
            ZombieAniState = AnimationState.RIGHTSTAND;
        }

        HP = 100;

        Zombie_Object.SetActive(false);
        Zombie_Navi.SetActive(false);

        DeadCoroutine = null;
        DeadCoroutine = DeadProtocol(true);

        StopCoroutine(DeadCoroutine);

        DamegeCoroutine = null;
        DamegeCoroutine = DamegeProtocol(true);

        StopCoroutine(DamegeCoroutine);

        AttackCoroutine = null;
        AttackCoroutine = AttackProtocol(true);

        StopCoroutine(AttackCoroutine);

        WalkPathCoroutine = null;
        WalkPathCoroutine = WalkPathProtocol(true);

        StopCoroutine(WalkPathCoroutine);

        DeadEyeChecker = false;
        AttackChecker = false;
        NowMoveOn = true;
        WalkPathChecker = false;
        DeadEyeInSight = false;

        this.gameObject.SetActive(false);
	}
    
    void OnEnable()
    {
        State = GameManager.NowGameState;
        ModeState = GameManager.NowGameModeState;


        //if (sp_Col == null)
        //{
        //    sp_Col = GetComponent<SphereCollider>();
        //    sp_Col.enabled = true;
        //}
        //else
        //{
        //    sp_Col.enabled = true;
        //}

        if (cap_Col == null)
        {
            cap_Col = GetComponent<CapsuleCollider>();
            cap_Col.enabled = true;
        }
        else
        {
            cap_Col.enabled = true;
        }

        if (Player_Ojbect == null)
        {
            Player_Ojbect = GameObject.FindWithTag("Player");
        }

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
            Zombie_Navi.transform.position = ObjectBackPos.transform.position;
            //Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = DeadSound;
        }
        else
        {
            Audio.clip = DeadSound;
        }

        // 애니메이션 방향
        if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
        {
            ZombieAniState = AnimationState.LEFTSTAND;
        }
        else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
        {
            ZombieAniState = AnimationState.RIGHTSTAND;
        }

        //HP = 100;

        Zombie_Object.SetActive(true);
        Zombie_Navi.SetActive(true);

        DeadCoroutine = null;
        DeadCoroutine = DeadProtocol(true);

        StopCoroutine(DeadCoroutine);

        DamegeCoroutine = null;
        DamegeCoroutine = DamegeProtocol(true);

        StopCoroutine(DamegeCoroutine);

        AttackCoroutine = null;
        AttackCoroutine = AttackProtocol(true);

        StopCoroutine(AttackCoroutine);

        WalkPathCoroutine = null;
        WalkPathCoroutine = WalkPathProtocol(true);

        StopCoroutine(WalkPathCoroutine);

        DeadEyeChecker = false;
        AttackChecker = false;
        NowMoveOn = true;
        WalkPathChecker = false;
        DeadEyeInSight = false;

    }

    IEnumerator DeadProtocol(bool on = true)
    {


        //sp_Col.enabled = false;
        cap_Col.enabled = false;
        NowMoveOn = false;

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
        }


        //if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
        //{
        //    ZombieAniState = AnimationState.LEFTDEAD;
        //}
        //else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
        //{
        //    ZombieAniState = AnimationState.RIGHTDEAD;
        //}

        yield return new WaitForSeconds(0.5f);

        GameManager.EnemyKillCount += 1;
        GameManager.MiniGame_KillCount += 1;


        Zombie_Navi.SetActive(false);
        Zombie_Object.SetActive(false);
        this.gameObject.SetActive(false);
    }

    IEnumerator AttackProtocol(bool on = true)
    {
        AttackChecker = true;
        
        yield return new WaitForSeconds(0.5f);
        
        AttackChecker = false;
    }

    IEnumerator DamegeProtocol(bool on = true)
    {
        HP -= 100;

        if (Audio.isPlaying == false)
        {
            Audio.clip = DeadSound;
            Audio.Play();
        }

        if (Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
        }

        NowMoveOn = false;

        // 애니메이션 방향
        if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
        {
            ZombieAniState = AnimationState.LEFTSTAND;
        }
        else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
        {
            ZombieAniState = AnimationState.RIGHTSTAND;
        }

        //Zombie_Object.GetComponent<MeshRenderer>().material.color = Color.red;
        
        yield return new WaitForSeconds(1.0f);

        if(Zombie_Navi != null)
        {
            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;
        }

        NowMoveOn = true;
        
        //Zombie_Object.GetComponent<MeshRenderer>().material.color = Color.white;


    }

    public void SetZombieHP(int health)
    {
        HP = health;
    }

	// Update is called once per frame
	void Update () {

        ModeState = GameManager.NowGameModeState;

        switch(ModeState)
        {
            case GameModeState.Single:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;

                    switch (State)
                    {
                        case GameState.START:
                            {

                            }
                            break;

                        case GameState.PLAY:
                            {


                            }
                            break;

                        case GameState.PAUSE:
                            {

                            }
                            break;

                        case GameState.EVENT:
                            {

                            }
                            break;

                        case GameState.VICTORY:
                            {

                            }
                            break;

                        case GameState.GAMEOVER:
                            {

                            }
                            break;
                    }
                }
                break;

            case GameModeState.MiniGame:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;
                    //Zombie_Object.transform.position = this.gameObject.transform.position;

                    print("GameManager.MiniGame_DeadEye_Sight_Number : " + GameManager.MiniGame_DeadEye_Sight_Number);

                    switch (State)
                    {
                        case GameState.START:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.PLAY:
                            {
                                //print("HP : " + HP);

                                if (HP <= 0)
                                {
                                    // 애니메이션 방향
                                    //if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
                                    //{
                                    //    ZombieAniState = AnimationState.LEFTDEAD;
                                    //}
                                    //else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
                                    //{
                                    //    ZombieAniState = AnimationState.RIGHTDEAD;
                                    //}

                                    // 네비게이션 작동 여부
                                    if (Zombie_Navi != null)
                                    {
                                        Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                        //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);

                                        DeadCoroutine = null;
                                        DeadCoroutine = DeadProtocol(true);

                                        StopCoroutine(DeadCoroutine);
                                        StartCoroutine(DeadCoroutine);
                                    }
                                }
                                else
                                {
                                    if (GameManager.DeadEyeActiveOn == true)
                                    {
                                        // 네비게이션 작동 여부
                                        if (Zombie_Navi != null)
                                        {
                                            Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                            //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                        }

                                        // 애니메이션 방향
                                        if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
                                        {
                                            ZombieAniState = AnimationState.LEFTSTAND;
                                        }
                                        else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
                                        {
                                            ZombieAniState = AnimationState.RIGHTSTAND;
                                        }

                                        if (GameManager.DeadEyeFailOn == false)
                                        {
                                            // 데드 아이 일시 플레이어와의 거리를 체크해 없애줄지 없애지 않을지 체크한다.
                                            if ((Player_Ojbect.transform.position - this.transform.position).magnitude < 5.5f)
                                            {
                                                if(GameManager.MiniGame_DeadEye_Sight_Number < 6)
                                                {
                                                    if (DeadEyeInSight == false)
                                                    {
                                                        DeadEyeInSight = true;

                                                        DeadEyeChecker = true;
                                                        GameManager.MiniGame_DeadEye_Sight_Number++;
                                                    }

                                                }
                                                //NowMoveOn = false;
                                                

                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (DeadEyeChecker == true && GameManager.DeadEyeFailOn == false)
                                        {
                                            DeadEyeChecker = false;
                                            cap_Col.enabled = false;
                                            //sp_Col.enabled = false;

                                            // 애니메이션 방향
                                            //if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
                                            //{
                                            //    ZombieAniState = AnimationState.LEFTDEAD;
                                            //}
                                            //else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
                                            //{
                                            //    ZombieAniState = AnimationState.RIGHTDEAD;
                                            //}

                                            DeadCoroutine = null;
                                            DeadCoroutine = DeadProtocol(true);

                                            StopCoroutine(DeadCoroutine);
                                            StartCoroutine(DeadCoroutine);
                                        }
                                        else
                                        {


                                            if (NowMoveOn == true)
                                            {
                                                // 네비게이션 작동 여부
                                                if (Zombie_Navi != null)
                                                {
                                                    print("DeadManWalk");
                                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = true;

                                                    if (WalkPathChecker == false)
                                                    {
                                                        WalkPathChecker = true;

                                                        if (this.gameObject.activeSelf == true)
                                                        {
                                                            Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                                        }


                                                        WalkPathCoroutine = null;
                                                        WalkPathCoroutine = WalkPathProtocol(true);

                                                        StopCoroutine(WalkPathCoroutine);
                                                        StartCoroutine(WalkPathCoroutine);
                                                    }
                                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                                }

                                                if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
                                                {
                                                    ZombieAniState = AnimationState.LEFTWALK;
                                                }
                                                else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
                                                {
                                                    ZombieAniState = AnimationState.RIGHTWALK;
                                                }
                                            }
                                            else
                                            {
                                                // 네비게이션 작동 여부
                                                if (Zombie_Navi != null)
                                                {
                                                    if (this.gameObject.activeSelf == true)
                                                    {
                                                        Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                                        //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                                    }

                                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                                }

                                                if ((this.transform.position.x >= Player_Ojbect.transform.position.x))
                                                {
                                                    ZombieAniState = AnimationState.LEFTSTAND;
                                                }
                                                else if ((this.transform.position.x < Player_Ojbect.transform.position.x))
                                                {
                                                    ZombieAniState = AnimationState.RIGHTSTAND;
                                                }
                                            }
                                        }

                                    }
                                }
                         

                            }
                            break;

                        case GameState.PAUSE:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.EVENT:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.VICTORY:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;

                        case GameState.GAMEOVER:
                            {
                                // 네비게이션 작동 여부
                                if (Zombie_Navi != null)
                                {
                                    Zombie_Navi.GetComponent<NavMeshAgent>().enabled = false;
                                    //Zombie_Navi.GetComponent<NavMeshAgent>().SetDestination(Player_Ojbect.transform.position);
                                }
                            }
                            break;
                    }
                }
                break;

            case GameModeState.Multi:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;

                    switch (State)
                    {
                        case GameState.START:
                            {

                            }
                            break;

                        case GameState.PLAY:
                            {

                            }
                            break;

                        case GameState.PAUSE:
                            {

                            }
                            break;

                        case GameState.EVENT:
                            {

                            }
                            break;

                        case GameState.VICTORY:
                            {

                            }
                            break;

                        case GameState.GAMEOVER:
                            {

                            }
                            break;
                    }
                }
                break;

            case GameModeState.NotSelect:
                {
                    State = GameManager.NowGameState;

                    // 위치 갱신
                    this.gameObject.transform.position = Zombie_Navi.transform.position;

                    switch (State)
                    {
                        case GameState.START:
                            {

                            }
                            break;

                        case GameState.PLAY:
                            {

                            }
                            break;

                        case GameState.PAUSE:
                            {

                            }
                            break;

                        case GameState.EVENT:
                            {

                            }
                            break;

                        case GameState.VICTORY:
                            {

                            }
                            break;

                        case GameState.GAMEOVER:
                            {

                            }
                            break;
                    }
                }
                break;
        }

	}

    public AnimationState GetAniState()
    {
        return ZombieAniState;
    }

    //void OnCollisionEnter(Collision collision)
    //{

    //}

    IEnumerator WalkPathProtocol(bool on = true)
    {
        WalkPathChecker = true;

        yield return new WaitForSeconds(0.5f);

        WalkPathChecker = false;
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.transform.tag.Equals("PlayerBullet") == true)
        {
            print("Zombie Dead");

            // 사망 처리
            DamegeCoroutine = null;
            DamegeCoroutine = DamegeProtocol(true);

            StopCoroutine(DamegeCoroutine);
            StartCoroutine(DamegeCoroutine);
        }
        
        //if (other.transform.tag.Equals("Player") == true)
        //{
        //    // 데미지 처리
        //    if(AttackChecker == false)
        //    {
        //        AttackChecker = true;

        //        AttackCoroutine = null;
        //        AttackCoroutine = AttackProtocol(true);

                

        //        StopCoroutine(AttackCoroutine);
        //        StartCoroutine(AttackCoroutine);
        //    }
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Player") == true)
        {
            // 데미지 처리
            switch(State)
            {
                case GameState.START:
                    {

                    }
                    break;

                case GameState.PLAY:
                    {
                        switch(Player_Ojbect.GetComponent<Player>().GetPlayerState())
                        {
                            case PlayerState.NORMAL:
                                {
                                    if (AttackChecker == false)
                                    {
                                        AttackChecker = true;


                                        AttackCoroutine = null;
                                        AttackCoroutine = AttackProtocol(true);

                                        Player_Ojbect.GetComponent<Player>().DamegeToPlayer(3);

                                        StopCoroutine(AttackCoroutine);
                                        StartCoroutine(AttackCoroutine);


                                    }
                                }
                                break;

                            case PlayerState.REALBATTLE:
                                {
                                    if (AttackChecker == false)
                                    {
                                        AttackChecker = true;


                                        AttackCoroutine = null;
                                        AttackCoroutine = AttackProtocol(true);

                                        Player_Ojbect.GetComponent<Player>().DamegeToPlayer(3);

                                        StopCoroutine(AttackCoroutine);
                                        StartCoroutine(AttackCoroutine);


                                    }
                                }
                                break;
                        }

                    }
                    break;

                case GameState.PAUSE:
                    {

                    }
                    break;

                case GameState.EVENT:
                    {

                    }
                    break;

                case GameState.VICTORY:
                    {

                    }
                    break;

                case GameState.GAMEOVER:
                    {

                    }
                    break;
            }


            


        }
        
    }
}
