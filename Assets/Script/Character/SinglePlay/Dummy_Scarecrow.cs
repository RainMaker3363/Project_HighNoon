using UnityEngine;
using System.Collections;

public class Dummy_Scarecrow : MonoBehaviour {

    private GameState NowGameState;
    private GameModeState NowGameModeState;

    private IEnumerator ActiveCoroutine;

    private SphereCollider SpColl;
    public GameObject ScareCrow_Object;
    private int HP;
    
	// Use this for initialization
	void Start () {
        
        HP = 100;

        if (SpColl == null)
        {
            SpColl = GetComponent<SphereCollider>();
            SpColl.enabled = true;
        }

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

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

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

        //ActiveCoroutine = ResurectionProtocol(true);
        //StartCoroutine(ActiveCoroutine);
        //StopCoroutine(ActiveCoroutine);
    }

    IEnumerator ResurectionProtocol(bool On = true)
    {
        SpColl.enabled = false;
        this.gameObject.SetActive(false);

        yield return new WaitForSeconds(2.5f);


        HP = 100;
        SpColl.enabled = true;
        this.gameObject.SetActive(true);

    }

	// Update is called once per frame
	void Update () {

        NowGameModeState = GameManager.NowGameModeState;
        NowGameState = GameManager.NowGameState;

        switch(NowGameModeState)
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

                ActiveCoroutine = ResurectionProtocol(true);

                StopCoroutine(ActiveCoroutine);
                StartCoroutine(ActiveCoroutine);

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
                HP -= 50;
            }


        }

    }
}
