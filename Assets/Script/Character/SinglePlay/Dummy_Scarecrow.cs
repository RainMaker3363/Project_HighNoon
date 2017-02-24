using UnityEngine;
using System.Collections;

public class Dummy_Scarecrow : MonoBehaviour {

    private GameState NowGameState;
    private GameModeState NowGameModeState;

    private IEnumerator ResurectCoroutine;
    private IEnumerator DeadEyeCoroutine;
    private IEnumerator DamgeCoroutine;

    private SphereCollider SpColl;
    public GameObject ScareCrow_Object;
    private GameObject m_Player;
    public GameObject DeadEyeMarkObject;


    private bool RespawnCheck;
    private int HP;
    
	// Use this for initialization
	void Start () {
        
        HP = 100;

        if (SpColl == null)
        {
            SpColl = GetComponent<SphereCollider>();
            SpColl.enabled = true;
        }

        if(DeadEyeMarkObject == null)
        {
            DeadEyeMarkObject = GameObject.Find("EnemyDeadMark");
            DeadEyeMarkObject.SetActive(false);
        }
        else
        {
            DeadEyeMarkObject.SetActive(false);
        }
        

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

        switch (NowGameModeState)
        {
            case GameModeState.Multi:
                {
                    if(m_Player == null)
                    {
                        m_Player = GameObject.FindWithTag("Player");
                    }

                }
                break;

            case GameModeState.Single:
                {
                    if (m_Player == null)
                    {
                        m_Player = GameObject.FindWithTag("Player");
                    }
                }
                break;

            case GameModeState.NotSelect:
                {
                    if (m_Player == null)
                    {
                        m_Player = GameObject.FindWithTag("Player");
                    }
                }
                break;
        }
        //ActiveCoroutine = ResurectionProtocol(true);
        //StartCoroutine(ActiveCoroutine);
        //StopCoroutine(ActiveCoroutine);

	}

    void OnEnable()
    {
        HP = 100;

        if (SpColl == null)
        {
            SpColl = GetComponent<SphereCollider>();
            SpColl.enabled = true;
        }

        if (DeadEyeMarkObject == null)
        {
            DeadEyeMarkObject = GameObject.Find("EnemyDeadMark");
            DeadEyeMarkObject.SetActive(false);
        }
        else
        {
            DeadEyeMarkObject.SetActive(false);
        }
        
        RespawnCheck = false;

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

        switch (NowGameModeState)
        {
            case GameModeState.Multi:
                {
                    if (m_Player == null)
                    {
                        m_Player = GameObject.FindWithTag("Player");
                    }

                }
                break;

            case GameModeState.Single:
                {
                    if (m_Player == null)
                    {
                        m_Player = GameObject.FindWithTag("Player");
                    }
                }
                break;

            case GameModeState.NotSelect:
                {
                    if (m_Player == null)
                    {
                        m_Player = GameObject.FindWithTag("Player");
                    }
                }
                break;
        }
        //ActiveCoroutine = ResurectionProtocol(true);
        //StartCoroutine(ActiveCoroutine);
        //StopCoroutine(ActiveCoroutine);
    }

    IEnumerator DeadEyeResructionProtocol(bool On = true)
    {
        

        yield return new WaitForSeconds(1.5f);

        SpColl.enabled = false;
        ScareCrow_Object.gameObject.SetActive(false);

        yield return new WaitForSeconds(5.5f);

        RespawnCheck = false;

        HP = 100;
        SpColl.enabled = true;
        ScareCrow_Object.gameObject.SetActive(true);

    }

    IEnumerator ResurectionProtocol(bool On = true)
    {
        SpColl.enabled = false;
        ScareCrow_Object.gameObject.SetActive(false);

        yield return new WaitForSeconds(4.0f);

        HP = 100;
        SpColl.enabled = true;
        ScareCrow_Object.gameObject.SetActive(true);
    }

    IEnumerator DamgeProtocol(bool On = true)
    {
        //ScareCrow_Object.GetComponent<Material>().color = Color.red;
        ScareCrow_Object.GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        ScareCrow_Object.GetComponent<MeshRenderer>().material.color = Color.white;
        //ScareCrow_Object.GetComponent<Material>().color = Color.white;
    }

	// Update is called once per frame
	void Update () {

        NowGameModeState = GameManager.NowGameModeState;
        NowGameState = GameManager.NowGameState;

        switch(NowGameModeState)
        {
            case GameModeState.Multi:
                {
                    MultiPlayerState MulPlayerState = m_Player.GetComponent<MulPlayer>().GetPlayerState();

                    switch (NowGameState)
                    {
                        case GameState.PLAY:
                            {
                                switch(MulPlayerState)
                                {
                                    case MultiPlayerState.LIVE:
                                        {
                                            DeadEyeMarkObject.SetActive(false);
                                        }
                                        break;

                                    case MultiPlayerState.DEADEYEING:
                                        {
                                            DeadEyeMarkObject.SetActive(false);
                                        }
                                        break;

                                    case MultiPlayerState.DEAD:
                                        {
                                            DeadEyeMarkObject.SetActive(false);
                                        }
                                        break;
                                }
                            }
                            break;

                        case GameState.START:
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

            case GameModeState.Single:
                {
                    PlayerState playerState = m_Player.GetComponent<Player>().GetPlayerState();

                    switch (NowGameState)
                    {
                        case GameState.PLAY:
                            {
                                switch(playerState)
                                {
                                    case PlayerState.NORMAL:
                                        {
                                            DeadEyeMarkObject.SetActive(false);

                                            if(HP <= 0)
                                            {
                                                HP = 0;

                                                ResurectCoroutine = null;
                                                ResurectCoroutine = ResurectionProtocol(true);

                                                StopCoroutine(ResurectCoroutine);
                                                StartCoroutine(ResurectCoroutine);
                                            }
                                        }
                                        break;

                                    case PlayerState.DEADEYE:
                                        {

                                            if(GameManager.DeadEyeActiveOn == false)
                                            {
                                                if (RespawnCheck == false)
                                                {
                                                    RespawnCheck = true;

                                                    DeadEyeMarkObject.SetActive(false);

                                                    DeadEyeCoroutine = null;
                                                    DeadEyeCoroutine = DeadEyeResructionProtocol(true);

                                                    StopCoroutine(DeadEyeCoroutine);
                                                    StartCoroutine(DeadEyeCoroutine);
                                                }


                                            }
                                            else
                                            {
                                                DeadEyeMarkObject.SetActive(false);
                                            }
                                        }
                                        break;

                                    case PlayerState.REALBATTLE:
                                        {
                                            DeadEyeMarkObject.SetActive(false);

                                            if (HP <= 0)
                                            {
                                                HP = 0;

                                                ResurectCoroutine = null;
                                                ResurectCoroutine = ResurectionProtocol(true);

                                                StopCoroutine(ResurectCoroutine);
                                                StartCoroutine(ResurectCoroutine);
                                            }
      
                                        }
                                        break;

                                    case PlayerState.DEAD:
                                        {
                                            DeadEyeMarkObject.SetActive(false);
                                        }
                                        break;
                                }

                            }
                            break;

                        case GameState.START:
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

                }
                break;
        }
      
	}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag.Equals("PlayerBullet") == true)
    //    {
    //        // HP를 깍이게 한다.

    //        print("Hit the Dummy");

    //        //this.gameObject.SetActive(false);

    //        if (HP <= 0)
    //        {
    //            HP = 0;

    //            ActiveCoroutine = ResurectionProtocol(true);
    //            StartCoroutine(ActiveCoroutine);

    //            //if(this.gameObject.activeSelf == true)
    //            //{
    //            //    if (GameManager.NowStageEnemies <= 0)
    //            //    {
    //            //        GameManager.NowStageEnemies = 0;
    //            //    }
    //            //    else
    //            //    {
    //            //        GameManager.NowStageEnemies -= 1;
    //            //    }

    //            //    this.gameObject.SetActive(false);
    //            //}
    //        }
    //        else
    //        {
    //            HP -= 50;
    //        }


    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("PlayerBullet") == true)
        {
            // HP를 깍이게 한다.

            print("Hit the Dummy");

            //this.gameObject.SetActive(false);

            if (HP <= 0)
            {
                HP = 0;

                ResurectCoroutine = null;
                ResurectCoroutine = ResurectionProtocol(true);

                StopCoroutine(ResurectCoroutine);
                StartCoroutine(ResurectCoroutine);

                //if(this.gameObject.activeSelf == true)
                //{
                //    if (GameManager.NowStageEnemies <= 0)
                //    {
                //        GameManager.NowStageEnemies = 0;
                //    }
                //    else
                //    {
                //        GameManager.NowStageEnemies -= 1;
                //    }

                //    this.gameObject.SetActive(false);
                //}
            }
            else
            {
                DamgeCoroutine = null;
                DamgeCoroutine = DamgeProtocol(true);

                StopCoroutine(DamgeCoroutine);
                StartCoroutine(DamgeCoroutine);

                HP -= 50;
            }


        }

    }
}
