﻿using UnityEngine;
using System.Collections;

public class DeadEyeBullet : MonoBehaviour {

    private GameState NowGameState;
    private GameModeState NowGameModeState;

    private IEnumerator ActiveCoroutine;
    private SphereCollider spcol;
    private bool RespawnChecker;
    public GameObject DeadEyeBullet_Object;
    public Transform[] DeadEyeRespawnTrans;

    // Use this for initialization
    void Start()
    {

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

        if (ActiveCoroutine == null)
        {
            ActiveCoroutine = null;
            ActiveCoroutine = ResurectionProtocol();
        }

        if (spcol == null)
        {
            spcol = GetComponent<SphereCollider>();
        }

        // 게임 모드 별로 랜덤 값을 지정해준다.
        switch(NowGameModeState)
        {
            case GameModeState.Single:
                {

                }
                break;

            case GameModeState.MiniGame:
                {
                    
                    int RespawnNumber = Random.Range(0, DeadEyeRespawnTrans.Length);
                    this.transform.position = DeadEyeRespawnTrans[RespawnNumber].transform.position;
                }
                break;

            case GameModeState.Multi:
                {

                }
                break;

            case GameModeState.NotSelect:
                {

                }
                break;
        }


        RespawnChecker = false;


        //StartCoroutine(ResurectionProtocol(true));
        //StopCoroutine(ResurectionProtocol(true));
    }

    void OnEnable()
    {

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;


        if (ActiveCoroutine == null)
        {
            ActiveCoroutine = null;
            ActiveCoroutine = ResurectionProtocol();

            StopCoroutine(ActiveCoroutine);
        }
        else
        {
            ActiveCoroutine = null;
            ActiveCoroutine = ResurectionProtocol();

            StopCoroutine(ActiveCoroutine);
        }

        if (spcol == null)
        {
            spcol = GetComponent<SphereCollider>();
        }

        // 게임 모드 별로 랜덤 값을 지정해준다.
        switch (NowGameModeState)
        {
            case GameModeState.Single:
                {

                }
                break;

            case GameModeState.MiniGame:
                {

                    int RespawnNumber = Random.Range(0, DeadEyeRespawnTrans.Length);
                    this.transform.position = DeadEyeRespawnTrans[RespawnNumber].transform.position;
                }
                break;

            case GameModeState.Multi:
                {

                }
                break;

            case GameModeState.NotSelect:
                {

                }
                break;
        }

        RespawnChecker = false;

        //StartCoroutine(ResurectionProtocol(true));
        

    }

    IEnumerator ResurectionProtocol()
    {
        Debug.Log("Protocol Start!");

        RespawnChecker = true;

        spcol.enabled = false;
        DeadEyeBullet_Object.gameObject.SetActive(false);

        yield return new WaitForSeconds(7.0f);

        Debug.Log("Protocol End!");

        //RespawnChecker = true;
        RespawnChecker = false;

        spcol.enabled = true;
        DeadEyeBullet_Object.gameObject.SetActive(true);

    }

    IEnumerator RandomResurectionProtocol()
    {
        Debug.Log("Protocol Start!");

        RespawnChecker = true;

        spcol.enabled = false;
        DeadEyeBullet_Object.gameObject.SetActive(false);

        yield return new WaitForSeconds(20.0f);

        Debug.Log("Protocol End!");

        //RespawnChecker = true;
        RespawnChecker = false;

        int RespawnNumber = Random.Range(0, DeadEyeRespawnTrans.Length);
        this.transform.position = DeadEyeRespawnTrans[RespawnNumber].transform.position;

        spcol.enabled = true;
        DeadEyeBullet_Object.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        NowGameModeState = GameManager.NowGameModeState;
        NowGameState = GameManager.NowGameState;

        switch (NowGameModeState)
        {
            case GameModeState.Multi:
                {
                    switch (NowGameState)
                    {
                        case GameState.PLAY:
                            {

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
                    switch (NowGameState)
                    {
                        case GameState.PLAY:
                            {
                                //NormalBullet_Object.transform.Rotate(new Vector3(0.0f, 65.0f, 0.0f) * Time.deltaTime);

                                if (GameManager.DeadEyeActiveOn == false)
                                {
                                    if (DeadEyeBullet_Object.activeSelf == false)
                                    {
                                        if (RespawnChecker == false)
                                        {
                                            ActiveCoroutine = null;
                                            ActiveCoroutine = ResurectionProtocol();

                                            StopCoroutine(ActiveCoroutine);
                                            StartCoroutine(ActiveCoroutine);
                                        }


                                    }
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

            case GameModeState.MiniGame:
                {
                    switch (NowGameState)
                    {
                        case GameState.PLAY:
                            {
                                //NormalBullet_Object.transform.Rotate(new Vector3(0.0f, 65.0f, 0.0f) * Time.deltaTime);

                                if (GameManager.DeadEyeActiveOn == false)
                                {
                                    if (DeadEyeBullet_Object.activeSelf == false)
                                    {
                                        if (RespawnChecker == false)
                                        {
                                            ActiveCoroutine = null;
                                            ActiveCoroutine = RandomResurectionProtocol();

                                            StopCoroutine(ActiveCoroutine);
                                            StartCoroutine(ActiveCoroutine);
                                        }


                                    }
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
        if (other.transform.tag.Equals("Player") == true)
        {
            switch (NowGameModeState)
            {
                case GameModeState.Multi:
                    {
                        switch (NowGameState)
                        {
                            case GameState.PLAY:
                                {

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
                        switch (NowGameState)
                        {
                            case GameState.PLAY:
                                {
                                    print("DeadEye Trigger Start!");

                                    spcol.enabled = false;
                                    DeadEyeBullet_Object.gameObject.SetActive(false);

                                    ActiveCoroutine = null;
                                    ActiveCoroutine = ResurectionProtocol();

                                    StopCoroutine(ActiveCoroutine);
                                    //StartCoroutine(ActiveCoroutine);
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

                case GameModeState.MiniGame:
                    {
                        switch (NowGameState)
                        {
                            case GameState.PLAY:
                                {
                                    print("DeadEye Trigger Start!");

                                    spcol.enabled = false;
                                    DeadEyeBullet_Object.gameObject.SetActive(false);

                                    ActiveCoroutine = null;
                                    ActiveCoroutine = ResurectionProtocol();

                                    StopCoroutine(ActiveCoroutine);
                                    //StartCoroutine(ActiveCoroutine);
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

    }
}
