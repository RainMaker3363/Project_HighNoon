using UnityEngine;
using System.Collections;

public class NormalBullet : MonoBehaviour {

    private GameState NowGameState;
    private GameModeState NowGameModeState;

    private IEnumerator ActiveCoroutine;
    private SphereCollider spcol;
    public GameObject NormalBullet_Object;

    private AudioSource Audio;
    public AudioClip Get_Sound;

    // Use this for initialization
    void Start()
    {

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = Get_Sound;
        }
        else
        {
            Audio.clip = Get_Sound;
        }

        if (ActiveCoroutine == null)
        {
            ActiveCoroutine = null;
            ActiveCoroutine = ResurectionProtocol();
        }

        if (spcol == null)
        {
            spcol = GetComponent<SphereCollider>();
        }


        //StartCoroutine(ResurectionProtocol(true));
        //StopCoroutine(ResurectionProtocol(true));
    }

    void OnEnable()
    {

        NowGameState = GameManager.NowGameState;
        NowGameModeState = GameManager.NowGameModeState;

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.clip = Get_Sound;
        }
        else
        {
            Audio.clip = Get_Sound;
        }


        if(ActiveCoroutine == null)
        {
            ActiveCoroutine = null;
            ActiveCoroutine = ResurectionProtocol();
        }

        if (spcol == null)
        {
            spcol = GetComponent<SphereCollider>();
        }
        //StartCoroutine(ResurectionProtocol(true));
        //StopCoroutine(ResurectionProtocol(true));

    }

    IEnumerator ResurectionProtocol()
    {
        Debug.Log("Protocol Start!");

        spcol.enabled = false;
        NormalBullet_Object.gameObject.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        Debug.Log("Protocol End!");

        spcol.enabled = true;
        NormalBullet_Object.gameObject.SetActive(true);

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

            print("Bullet + 1");


            Audio.Play();

            ActiveCoroutine = ResurectionProtocol();

            StopCoroutine(ActiveCoroutine);
            StartCoroutine(ActiveCoroutine);
        }

    }
}
