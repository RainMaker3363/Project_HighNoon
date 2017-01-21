﻿using UnityEngine;
using System.Collections;



public class Player : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;
    
    public JoyStickCtrl JoyStickControl;

    [HideInInspector]
    public PlayerState playerState;
    private PlayerState PrevPlayerState;
    private AnimationState PlayerAniState;
    
    // 플레이어의 상태 값
    private float HP;
    private GameObject DeadEyeBox;
    private bool DeadEyeActive;
    private bool DeadEyeReady;

    // 플레이어의 움직임
    private float MoveSpeed;
    private Vector3 MoveVector;
    private Vector3 PlayerDir;
    private Vector3 PlayerPosBack;

    // 현재 컨트롤러가 플레이어를 조종할 수 있는지의 여부
    private bool NowMovePlayer;

    // 플레이어가 총을 쏠 수 있을지의 여부
    private bool ShootOn;
    private bool ReloadSuccessOn;
    private float ShootCoolTime;
    private int BulletStack;
    private int NowBulletIndex;

    // 플레이어와 연관된 오브젝트들
    public GameObject[] Bullets;

    // 카메라
    public GameObject CameraObject;
    private bool CameraMoveOn;
    private RaycastHit Wallhit;
    private int WalllayerMask;

    // 플레이어의 사선
    private Vector3[] Line_Tranjectory_Transform;
    public GameObject Player_Tranjectory_Object;
    private LineRenderer Player_Tranjectory;

    // 플레이어의 타일을 계산
    private RaycastHit hit;
    private int layerMask;
    private Vector3 PlayerPos;

	// Use this for initialization
	void Start () {
	
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;
        PlayerAniState = AnimationState.DOWNSTAND;
        
        if(JoyStickControl == null)
        {
            JoyStickControl = GameObject.Find("Joystick_Pad").GetComponent<JoyStickCtrl>();
        }

        if(CameraObject == null)
        {
            CameraObject = GameObject.Find("Camera");
        }

        if (Player_Tranjectory == null)
        {
            Player_Tranjectory = GetComponent<LineRenderer>();
            Player_Tranjectory.enabled = false;

            Line_Tranjectory_Transform = new Vector3[2];
            Line_Tranjectory_Transform[1] = this.gameObject.transform.position;
            Line_Tranjectory_Transform[0] = Player_Tranjectory_Object.transform.position;
        }
        else
        {
            Player_Tranjectory.enabled = false;

            Line_Tranjectory_Transform = new Vector3[2];
            Line_Tranjectory_Transform[1] = this.gameObject.transform.position;
            Line_Tranjectory_Transform[0] = Player_Tranjectory_Object.transform.position;
        }

        if (DeadEyeBox == null)
        {
            DeadEyeBox = GameObject.FindWithTag("DeadEyeBox");
        }


        // 플레이어의 상태 값
        HP = 100.0f;
        playerState = PlayerState.NORMAL;
        DeadEyeActive = false;
        DeadEyeReady = false;

        // 플레이어 이동 방향 초기화
        MoveVector = Vector3.zero;
        MoveSpeed = 2.0f;
        PlayerDir = Vector3.zero;
        PlayerPosBack = Vector3.zero;

        // 플레이어가 사격할 수 있는지의 여부
        ShootOn = true;
        ReloadSuccessOn = true;
        ShootCoolTime = 1.0f;
        BulletStack = 6;

        NowMovePlayer = false;
        CameraMoveOn = true;

        // 총알 오브젝트의 출력 순서를 제어하는 변수
        NowBulletIndex = 0;

        // 총알 오브젝트 설정
        for(int i = 0; i<Bullets.Length; i++)
        {
            Bullets[i].gameObject.SetActive(false);
        }

        PlayerPos = Vector3.zero;
        layerMask = (1 << LayerMask.NameToLayer("Ground"));
        WalllayerMask = ((1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.NameToLayer("DeadEyeBox")));
	}

    // Update is called once per frame
    void Update()
    {
        State = GameManager.NowGameState;

        MoveVector = PoolInput();

        if (MoveVector == Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(PlayerDir);
            NowMovePlayer = false;
        }
        else
        {
            PlayerDir = MoveVector;
            transform.rotation = Quaternion.LookRotation(PlayerDir);
            NowMovePlayer = true;
        }



        switch (State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
                {
                    switch (playerState)
                    {
                        case PlayerState.NORMAL:
                            {

                                Player_Tranjectory.enabled = true;

                                if (NowMovePlayer)
                                {
                                    if (CameraMoveOn)
                                    {
                                        this.transform.Translate((Vector3.forward * MoveSpeed) * Time.deltaTime);
                                        CameraObject.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
                                    }
                                }

                                

                                Line_Tranjectory_Transform[1] = this.gameObject.transform.position;
                                Line_Tranjectory_Transform[0] = Player_Tranjectory_Object.transform.position;
                                Player_Tranjectory.SetPositions(Line_Tranjectory_Transform);

                                Debug.DrawRay(this.transform.position, (Vector3.down) * 50.0f, Color.red);
                                Debug.DrawRay(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 0.7f, Color.yellow);

                                if (Physics.Raycast(this.transform.position, (Vector3.down), out hit, Mathf.Infinity, layerMask))
                                {
                                    //print("hit Name : " + hit.collider.name);
                                    //print("hit Point : " + hit.point);

                                    PlayerPos = hit.point;
                                }

                                if (Physics.Raycast(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 1.0f, out Wallhit, 0.7f, WalllayerMask))
                                {
                                    if (Wallhit.collider.transform.tag.Equals("Wall") == true)
                                    {
                                        CameraMoveOn = false;
                                    }

                                    if (Wallhit.collider.transform.tag.Equals("DeadEyeBox") == true)
                                    {
                                        CameraMoveOn = false;

                                        Debug.Log("DeadEye Ready");

                                        DeadEyeReady = true;
                                    }
                                    else
                                    {
                                        Debug.Log("DeadEye Not Ready");

                                        DeadEyeReady = false;
                                    }
                                }
                                else
                                {
                                    CameraMoveOn = true;

                                    DeadEyeReady = false;
                                }
                            }
                            break;

                        case PlayerState.REALBATTLE:
                            {
                                Player_Tranjectory.enabled = true;

                                if (NowMovePlayer)
                                {
                                    if (CameraMoveOn)
                                    {
                                        this.transform.Translate((Vector3.forward * MoveSpeed) * Time.deltaTime);
                                        CameraObject.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
                                    }
                                }



                                Line_Tranjectory_Transform[1] = this.gameObject.transform.position;
                                Line_Tranjectory_Transform[0] = Player_Tranjectory_Object.transform.position;
                                Player_Tranjectory.SetPositions(Line_Tranjectory_Transform);

                                Debug.DrawRay(this.transform.position, (Vector3.down) * 50.0f, Color.red);
                                Debug.DrawRay(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 0.7f, Color.yellow);

                                if (Physics.Raycast(this.transform.position, (Vector3.down), out hit, Mathf.Infinity, layerMask))
                                {
                                    //print("hit Name : " + hit.collider.name);
                                    //print("hit Point : " + hit.point);

                                    PlayerPos = hit.point;
                                }

                                if (Physics.Raycast(this.transform.position, (Player_Tranjectory_Object.transform.position - this.transform.position).normalized * 1.0f, out Wallhit, 0.7f, WalllayerMask))
                                {

                                    if (Wallhit.collider.transform.tag.Equals("Wall") == true)
                                    {

                                        CameraMoveOn = false;    
                                    }

                                    if(Wallhit.collider.transform.tag.Equals("DeadEyeBox") == true)
                                    {
                                        CameraMoveOn = false;

                                        Debug.Log("DeadEye Ready");

                                        DeadEyeReady = true;
                                    }
                                    else
                                    {
                                        Debug.Log("DeadEye Not Ready");

                                        DeadEyeReady = false;
                                    }

                                    
                                }
                                else
                                {
                                    CameraMoveOn = true;

                                    DeadEyeReady = false;
                                }
                            }
                            break;

                        case PlayerState.DEADEYE:
                            {
                                Player_Tranjectory.enabled = false;
                                PlayerAniState = AnimationState.DEADEYING;
                            }
                            break;

                        case PlayerState.DEAD:
                            {
                                PlayerAniState = AnimationState.DEAD;
                                GameManager.NowGameState = GameState.GAMEOVER;
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

            case GameState.GAMEOVER:
                {

                }
                break;

            case GameState.VICTORY:
                {

                }
                break;
        }
    }

    //void FixedUpdate()
    //{

    //    switch (State)
    //    {
    //        case GameState.START:
    //            {

    //            }
    //            break;

    //        case GameState.PLAY:
    //            {
    //                switch(playerState)
    //                {
    //                    case PlayerState.NORMAL:
    //                        {
    //                            this.transform.Translate((MoveVector * MoveSpeed) * Time.deltaTime);
    //                        }
    //                        break;

    //                    case PlayerState.DEADEYE:
    //                        {

    //                        }
    //                        break;

    //                    case PlayerState.DEAD:
    //                        {
    //                            GameManager.NowGameState = GameState.GAMEOVER;
    //                        }
    //                        break;
    //                }
                    
    //            }
    //            break;

    //        case GameState.PAUSE:
    //            {

    //            }
    //            break;

    //        case GameState.EVENT:
    //            {

    //            }
    //            break;

    //        case GameState.GAMEOVER:
    //            {

    //            }
    //            break;

    //        case GameState.VICTORY:
    //            {

    //            }
    //            break;
    //    }
    //}

    public Vector3 PoolInput()
    {
        Vector3 Direction = Vector3.zero;

        Direction.x = JoyStickControl.GetHorizontalValue();
        Direction.z = JoyStickControl.GetVerticalValue();

        if(Direction.magnitude > 1)
        {
            Direction.Normalize();
        }

        return Direction;
    }

    public void Shoot()
    {
        switch (playerState)
        {
            case PlayerState.NORMAL:
                {
                    // 총알의 개수 파악하기
                    if (BulletStack > 0)
                    {

                        // 재장전이 다 되어있다면 발사한다.
                        if (ReloadSuccessOn == true)
                        {
                            if (ShootOn == true)
                            {
                                StopCoroutine(ShootProtocol(true));
                                StartCoroutine(ShootProtocol(true));
                            }
                        }
                    }
                    else
                    {
                        StopCoroutine(ReloadProtocol(true));
                        StartCoroutine(ReloadProtocol(true));
                    }
                }
                break;

            case PlayerState.REALBATTLE:
                {
                    // 총알의 개수 파악하기
                    if (BulletStack > 0)
                    {

                        // 재장전이 다 되어있다면 발사한다.
                        if (ReloadSuccessOn == true)
                        {
                            if (ShootOn == true)
                            {
                                StopCoroutine(ShootProtocol(true));
                                StartCoroutine(ShootProtocol(true));
                            }
                        }
                    }
                    else
                    {
                        StopCoroutine(ReloadProtocol(true));
                        StartCoroutine(ReloadProtocol(true));
                    }
                }
                break;

            case PlayerState.DEADEYE:
                {
                    if (DeadEyeActive == false)
                    {
                        StopCoroutine(DeadEyeShootProtocol(true));
                        StartCoroutine(DeadEyeShootProtocol(true));
                    }

                }
                break;

            case PlayerState.DEAD:
                {
                    this.gameObject.SetActive(false);
                }
                break;
        }



        
    }

    IEnumerator ShootProtocol(bool On = true)
    {
        ShootOn = false;

        //Debug.Log("Bang!");

        // 총알 오브젝트 On
        if (NowBulletIndex < Bullets.Length)
        {
            if (Bullets[NowBulletIndex].gameObject.activeSelf == false)
            {
                Bullets[NowBulletIndex].transform.position = this.transform.position;
                //Bullets[NowBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                Bullets[NowBulletIndex].transform.parent = null;

                Bullets[NowBulletIndex].gameObject.SetActive(true);

                NowBulletIndex++;
            }
        }
        else
        {
            NowBulletIndex = 0;

            if (Bullets[NowBulletIndex].gameObject.activeSelf == false)
            {
                Bullets[NowBulletIndex].transform.position = this.transform.position;
                //Bullets[NowBulletIndex].transform.LookAt(Player_Tranjectory_Object.transform.position);
                Bullets[NowBulletIndex].transform.parent = null;

                Bullets[NowBulletIndex].gameObject.SetActive(true);

                NowBulletIndex++;
            }
        }

        BulletStack--;
        ReloadSuccessOn = true;


        yield return new WaitForSeconds(ShootCoolTime);

        ShootOn = true;
    }

    // 재장전 하기
    IEnumerator ReloadProtocol(bool On = true)
    {
        Debug.Log("Reload!");

        ReloadSuccessOn = false;

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Reload End!");

        ReloadSuccessOn = true;
        BulletStack = 6;
    }

    // 데드 아이 시전 로직
    IEnumerator DeadEyeShootProtocol(bool On = true)
    {

        // 데드 아이 시전

        print("DeadEye Start....");
        DeadEyeActive = true;

        playerState = PlayerState.DEADEYE;
        PlayerPosBack = this.transform.position;
        this.transform.position = new Vector3(DeadEyeBox.transform.position.x, this.transform.position.y + DeadEyeBox.transform.position.y, DeadEyeBox.transform.position.z);

        yield return new WaitForSeconds(3.5f);

        print("DeadEye End....");

        DeadEyeActive = false;

        playerState  = PlayerState.REALBATTLE;
        this.transform.position = PlayerPosBack;

        DeadEyeBox.SetActive(false);
    }

    // 사망 시 애니메이션 및 순서
    IEnumerator DeadProtocol(bool On = true)
    {

        


        yield return new WaitForSeconds(1.5f);

        playerState = PlayerState.DEAD;
    }

    // 발사 로직 쿨 타임
    //IEnumerator ShootCoolProtocol(bool On = true)
    //{
    //    Debug.Log("Shoot Cool!");

    //    yield return new WaitForSeconds(0.3f);

    //    BulletStack--;
    //    ReloadSuccessOn = true;

    //    Debug.Log("Shoot Cool End!");
    //}

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public Vector3 GetPlayerDirection()
    {
        return (Player_Tranjectory_Object.transform.position - this.transform.position).normalized;
    }

    public Vector3 GetPlayerPosition()
    {
        if(PlayerPos == Vector3.zero)
        {
            return this.transform.position;
        }
        else
        {
            return hit.point;
        }
        
    }

    // NORMAL = 0;
    // DEADEYE = 1;
    // REALBATTLE = 2;
    // DEAD = 3;
    public void SetPlayerState(int Set)
    {
        switch(Set)
        {
            case 0:
                {
                    playerState = PlayerState.NORMAL;
                }
                break;

            case 1:
                {
                    PrevPlayerState = playerState;
                    playerState = PlayerState.DEADEYE;
                }
                break;

            case 2:
                {
                    playerState = PlayerState.REALBATTLE;
                }
                break;

            case 3:
                {
                    playerState = PlayerState.DEAD;
                }
                break;
        }
    }

    public bool GetDeadEyeReady()
    {
        return DeadEyeReady;
    }

    public GameObject GetDeadEyeObject()
    {
        return DeadEyeBox;
    }

    public GameObject GetCameraObject()
    {
        return CameraObject;
    }

    public AnimationState GetPlayerAniState()
    {
        return PlayerAniState;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("EnemyBullet") == true)
        {
            // HP를 깍는다
            if (HP <= 0)
            {
                HP = 0;

                Debug.Log("The Game End...ㅠ");

                GameManager.NowGameState = GameState.GAMEOVER;
                playerState = PlayerState.DEAD;

                StopCoroutine(DeadProtocol(true));
                StartCoroutine(DeadProtocol(true));



            }
            else
            {
                //HP -= 10;
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("EnemyBullet") == true)
        {
            // HP를 깍는다
            if(HP <= 0)
            {
                HP = 0;

                Debug.Log("The Game End...ㅠ");

                GameManager.NowGameState = GameState.GAMEOVER;
                playerState = PlayerState.DEAD;

                StopCoroutine(DeadProtocol(true));
                StartCoroutine(DeadProtocol(true));
                
                

            }
            else
            {
                //HP -= 10;
            }
            
        }

        //if (other.transform.tag.Equals("DeadEyeBox") == true)
        //{
        //    // 데드아이를 준비한다.

        //    print("DeadEye Ready");

        //    DeadEyeReady = true;


        //}
        //else
        //{
        //    print("DeadEye Not Ready");

        //    DeadEyeReady = false;
        //}
    }
}
