using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Single_RevolverTouch : MonoBehaviour {

    private GameModeState ModeState;

    public GameObject Single_RevolverAction_Object;
    public GameObject Single_RevolverTouch_BG_object;
    public GameObject Single_RevolverLetterBox_Object;
    public GameObject Single_RevolverLetterBox_BG_Object;
    public GameObject Single_RevolverBullets_Object;
    public GameObject InGame_UI_Object;

    public Text AnnounceText;
    public Text TimerText;

    public static int BulletChecker;
    public static float DeadEyeGage;
    private float DeadEyeTimer;

    private IEnumerator DeadEyeEndCoroutine;
    private IEnumerator DeadEyeFailCoroutine;
    private List<int> BulletIndex = new List<int>();
    public Single_RevolverBullet[] Bullets;

	// Use this for initialization
	void Start () 
    {
        
        BulletIndex.Clear();
        
        BulletChecker = 1;
        GameManager.DeadEyeFailOn = false;
        DeadEyeGage = 100.0f;
        DeadEyeTimer = 3.0f;

        ModeState = GameManager.NowGameModeState;

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].MyNumber = 0;
            Bullets[i].MyNumber = GenerateNumber(1, 7);
            Bullets[i].gameObject.SetActive(true);
        }

        if (Single_RevolverAction_Object == null)
        {
            Single_RevolverAction_Object = GameObject.Find("Single_RevolverAction");
            //Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            //Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }
        else
        {
            //Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            //Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }

        if(Single_RevolverLetterBox_Object == null)
        {
            Single_RevolverLetterBox_Object = GameObject.Find("LetterBox");
            Single_RevolverLetterBox_Object.SetActive(true);
            Single_RevolverLetterBox_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverLetterBox_Object.GetComponent<Animator>().Stop();
            
        }
        else
        {
            Single_RevolverLetterBox_Object.SetActive(true);
            Single_RevolverLetterBox_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverLetterBox_Object.GetComponent<Animator>().Stop();
        }

        if (Single_RevolverLetterBox_BG_Object == null)
        {
            Single_RevolverLetterBox_BG_Object = GameObject.Find("RevolverAction_BG");
            Single_RevolverLetterBox_BG_Object.SetActive(true);
        }
        else
        {
            Single_RevolverLetterBox_BG_Object.SetActive(true);
        }

        if(Single_RevolverTouch_BG_object == null)
        {
            Single_RevolverTouch_BG_object = GameObject.Find("DeadEye_BG");
            Single_RevolverTouch_BG_object.SetActive(true);
        }
        else
        {
            Single_RevolverTouch_BG_object.SetActive(true);
        }

        if (Single_RevolverBullets_Object == null)
        {
            Single_RevolverBullets_Object = GameObject.Find("Revolver_BG");
            Single_RevolverBullets_Object.SetActive(true);
        }
        else
        {
            Single_RevolverBullets_Object.SetActive(true);
        }

        if (InGame_UI_Object == null)
        {
            InGame_UI_Object = GameObject.Find("Player_UI");
            InGame_UI_Object.SetActive(false);
        }
        else
        {
            InGame_UI_Object.SetActive(false);
        }

        DeadEyeEndCoroutine = null;
        DeadEyeEndCoroutine = LetterBoxProtocol(true);

        StopCoroutine(DeadEyeEndCoroutine);

        DeadEyeFailCoroutine = null;
        DeadEyeFailCoroutine = LetterBoxFailProtocol(true);

        StopCoroutine(DeadEyeFailCoroutine);

        AnnounceText.text = "순서대로 터치 하세요";

	}

    void OnEnable()
    {
        BulletIndex.Clear();

        BulletChecker = 1;
        GameManager.DeadEyeFailOn = false;
        DeadEyeGage = 100.0f;
        DeadEyeTimer = 3.0f;

        ModeState = GameManager.NowGameModeState;

        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].MyNumber = 0;
            Bullets[i].MyNumber = GenerateNumber(1, 7);
            Bullets[i].gameObject.SetActive(true);
        }

        if (Single_RevolverAction_Object == null)
        {
            Single_RevolverAction_Object = GameObject.Find("Single_RevolverAction");
            //Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            //Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }
        else
        {
            //Single_RevolverAction_Object.GetComponent<Animator>().enabled = false;
            //Single_RevolverAction_Object.GetComponent<Animator>().Stop();
        }

        if (Single_RevolverLetterBox_Object == null)
        {
            Single_RevolverLetterBox_Object = GameObject.Find("LetterBox");
            Single_RevolverLetterBox_Object.SetActive(true);
            Single_RevolverLetterBox_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverLetterBox_Object.GetComponent<Animator>().Stop();
        }
        else
        {
            Single_RevolverLetterBox_Object.SetActive(true);
            Single_RevolverLetterBox_Object.GetComponent<Animator>().enabled = false;
            Single_RevolverLetterBox_Object.GetComponent<Animator>().Stop();
        }

        if (Single_RevolverLetterBox_BG_Object == null)
        {
            Single_RevolverLetterBox_BG_Object = GameObject.Find("RevolverAction_BG");
            Single_RevolverLetterBox_BG_Object.SetActive(true);
        }
        else
        {
            Single_RevolverLetterBox_BG_Object.SetActive(true);
        }

        if (Single_RevolverTouch_BG_object == null)
        {
            Single_RevolverTouch_BG_object = GameObject.Find("DeadEye_BG");
            Single_RevolverTouch_BG_object.SetActive(true);
        }
        else
        {
            Single_RevolverTouch_BG_object.SetActive(true);
        }

        if (Single_RevolverBullets_Object == null)
        {
            Single_RevolverBullets_Object = GameObject.Find("Revolver_BG");
            Single_RevolverBullets_Object.SetActive(true);
        }
        else
        {
            Single_RevolverBullets_Object.SetActive(true);
        }

        if (InGame_UI_Object == null)
        {
            InGame_UI_Object = GameObject.Find("Player_UI");
            InGame_UI_Object.SetActive(false);
        }
        else
        {
            InGame_UI_Object.SetActive(false);
        }

        DeadEyeEndCoroutine = null;
        DeadEyeEndCoroutine = LetterBoxProtocol(true);

        StopCoroutine(DeadEyeEndCoroutine);

        DeadEyeFailCoroutine = null;
        DeadEyeFailCoroutine = LetterBoxFailProtocol(true);

        StopCoroutine(DeadEyeFailCoroutine);

        AnnounceText.text = "순서대로 터치 하세요";
    }
	
	// Update is called once per frame
	void Update () {
        
        ModeState = GameManager.NowGameModeState;

        switch(ModeState)
        {
            case GameModeState.Single:
                {
                    if (BulletChecker > Bullets.Length)
                    {

                        GameManager.DeadEyeVersusAction = false;
                        GameManager.DeadEyeRevolverAction = false;
                        GameManager.DeadEyeFailOn = false;

                        Single_RevolverTouch_BG_object.SetActive(false);
                        Single_RevolverAction_Object.SetActive(false);
                        Single_RevolverBullets_Object.SetActive(false);
                        Single_RevolverLetterBox_BG_Object.SetActive(false);

                        AnnounceText.text = " ";

                        DeadEyeEndCoroutine = null;
                        DeadEyeEndCoroutine = LetterBoxProtocol(true);

                        StopCoroutine(DeadEyeEndCoroutine);
                        StartCoroutine(DeadEyeEndCoroutine);
                    }
                }
                break;


            case GameModeState.MiniGame:
                {

                    if (DeadEyeTimer > 0)
                    {
                        DeadEyeTimer -= Time.deltaTime;
                        TimerText.text = DeadEyeTimer.ToString("0.0");

                        if (GameManager.DeadEyeFailOn == false)
                        {

                            if (BulletChecker > Bullets.Length)
                            {

                                GameManager.DeadEyeVersusAction = false;
                                GameManager.DeadEyeRevolverAction = false;
                                GameManager.DeadEyeFailOn = false;
                                GameManager.MiniGame_DeadEye_Sight_Number = 0;

                                Single_RevolverTouch_BG_object.SetActive(false);
                                Single_RevolverAction_Object.SetActive(false);
                                Single_RevolverBullets_Object.SetActive(false);
                                Single_RevolverLetterBox_BG_Object.SetActive(false);

                                //AnnounceText.text = " ";

                                DeadEyeEndCoroutine = null;
                                DeadEyeEndCoroutine = LetterBoxProtocol(true);

                                StopCoroutine(DeadEyeEndCoroutine);
                                StartCoroutine(DeadEyeEndCoroutine);
                            }
                            else
                            {
                                DeadEyeGage -= (33.0f * Time.deltaTime);
                            }
                        }
                        else
                        {
                            GameManager.DeadEyeVersusAction = false;
                            GameManager.DeadEyeRevolverAction = false;


                            Single_RevolverTouch_BG_object.SetActive(false);
                            Single_RevolverAction_Object.SetActive(false);
                            Single_RevolverBullets_Object.SetActive(false);
                            Single_RevolverLetterBox_BG_Object.SetActive(false);

                            DeadEyeFailCoroutine = null;
                            DeadEyeFailCoroutine = LetterBoxFailProtocol(true);

                            StopCoroutine(DeadEyeFailCoroutine);
                            StartCoroutine(DeadEyeFailCoroutine);

                            GameManager.DeadEyeFailOn = true;
                            GameManager.MiniGame_DeadEye_Sight_Number = 0;
                        }
                    }
                    else
                    {
                        DeadEyeTimer = 0.0f;

                        GameManager.DeadEyeVersusAction = false;
                        GameManager.DeadEyeRevolverAction = false;


                        Single_RevolverTouch_BG_object.SetActive(false);
                        Single_RevolverAction_Object.SetActive(false);
                        Single_RevolverBullets_Object.SetActive(false);
                        Single_RevolverLetterBox_BG_Object.SetActive(false);

                        DeadEyeFailCoroutine = null;
                        DeadEyeFailCoroutine = LetterBoxFailProtocol(true);

                        StopCoroutine(DeadEyeFailCoroutine);
                        StartCoroutine(DeadEyeFailCoroutine);

                        GameManager.DeadEyeFailOn = true;
                        GameManager.MiniGame_DeadEye_Sight_Number = 0;
                    }

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
     


        //print("BulletChecker : " + BulletChecker);
	}

    IEnumerator LetterBoxProtocol(bool IsOn = true)
    {
        GameManager.DeadEyeActiveOn = false;
        Single_RevolverLetterBox_Object.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(1.0f);

        Single_RevolverLetterBox_Object.SetActive(false);
        InGame_UI_Object.SetActive(true);
        this.gameObject.SetActive(false);
    }

    IEnumerator LetterBoxFailProtocol(bool IsOn = true)
    {
        GameManager.DeadEyeActiveOn = false;
        Single_RevolverLetterBox_Object.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(1.0f);

        Single_RevolverLetterBox_Object.SetActive(false);
        InGame_UI_Object.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public int GenerateNumber(int min, int max)
    {
        int num = Random.Range(min, max);

        while (BulletIndex.Contains(num))
        {
            num = Random.Range(min, max);
        }

        BulletIndex.Add(num);

        return num;
    }
}
