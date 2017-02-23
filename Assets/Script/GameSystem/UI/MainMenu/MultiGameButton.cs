using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MultiGameButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public SoundManager SDManager;
    public AudioClip Button_Touch_Sound;

    private bool buttonDown;
    private bool StartMultiOn;
    private bool StartMultiSetOn;
    private bool MultiButtonDownCheck;
    
    public Text NetText;
    public Text NetWarningText;
    public Text NetReadyText;

    // Use this for initialization
    void Start()
    {
        buttonDown = false;
        StartMultiOn = false;
        StartMultiSetOn = false;
        MultiButtonDownCheck = false;
        
    }

    IEnumerator StartMultiGame()
    {
        yield return new WaitForSeconds(1.0f);

        AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        NetText.text = GPGSManager.GetInstance.GetNetMessage();

        if(GPGSManager.GetInstance.IsAuthenticated())
        {
            NetWarningText.text = "구글 계정이 연결 되었습니다.";
        }
        else
        {
            NetWarningText.text = "구글 계정이 아직 연결되지 않았습니다.";
        }

        if (GPGSManager.GetInstance.IsConnected() == true)
        {
            MultiButtonDownCheck = true;
            MainMenuManager.StartMultiGameOn = true;

            NetReadyText.text = "잠시후 멀티 게임을 시작합니다.";

            StartCoroutine(StartMultiGame());
        }
        else
        {
            MainMenuManager.StartMultiGameOn = false;
            MultiButtonDownCheck = false;

            NetReadyText.text = "아직 멀티 게임을 준비 중입니다...";
        }

        //switch (Application.platform)
        //{
        //    case RuntimePlatform.Android:
        //        {
        //            if (Input.GetKey(KeyCode.Escape))
        //            {

        //                // 할꺼 하셈

        //                // Application.Quit();

        //            }

        //            if (Input.touchCount > 0)
        //            {
        //                Touch touch = Input.GetTouch(0);

        //                switch (touch.phase)
        //                {
        //                    // 화면을 터치하는 순간 체크!
        //                    case TouchPhase.Began:
        //                        {
        //                            Debug.Log("TouchPhase Began!");
        //                        }
        //                        break;

        //                    // 사용자가 터치하고 드래그를 했을때
        //                    case TouchPhase.Moved:
        //                        {
        //                            Debug.Log("TouchPhase Moved!");
        //                        }
        //                        break;

        //                    // Moved 상태이다가 손을 땠을때 발생
        //                    case TouchPhase.Stationary:
        //                        {
        //                            Debug.Log("TouchPhase Stationary!");
        //                        }
        //                        break;

        //                    // 사용자가 터치를 하고 손을 땠을때의 상황
        //                    case TouchPhase.Ended:
        //                        {
        //                            Debug.Log("TouchPhase Ended!");
        //                        }
        //                        break;

        //                    // 시스템에 의해 터치가 취소된 경우다. 즉 사용자가 화면에서 손을 뗀 것이 아니라, 전화가 왔다던지 등등 이러한 상황일 때다.
        //                    case TouchPhase.Canceled:
        //                        {
        //                            Debug.Log("TouchPhase Canceled!");
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //        break;

        //    default:
        //        {
        //            if (Input.GetKey(KeyCode.Escape))
        //            {

        //                // 할꺼 하셈

        //                // Application.Quit();

        //            }
        //        }
        //        break;
        //}
    }

    // 터치가 드래그(Drag) 했을때 호출 되는 함수
    public virtual void OnDrag(PointerEventData ped)
    {

    }


    // 터치를 하고 있을 대 발생하는 함수
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if(GPGSManager.GetInstance.IsAuthenticated() == true)
        {
            if (MainMenuManager.MainModeBtnDownOn == true)
            {
                MainMenuManager.StartMultiGameOn = true;

                if (buttonDown == false)
                {
                    buttonDown = true;

                    SDManager.PlaySfx(Button_Touch_Sound);

                    MainMenuManager.gameMode = GameModeState.Multi;



                    //AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);

                    //GPGSManager.GetInstance.SignInAndStartMPGame();
                }

                if (MultiButtonDownCheck == false)
                {
                    MultiButtonDownCheck = true;

                    if (GPGSManager.GetInstance.IsConnected() == false)
                    {

                        GPGSManager.GetInstance.SignInAndStartMPGame();

                        //GPGSManager.GetInstance.ShowRoomUI();
                        //AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);
                    }
                    else
                    {
                        //AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);
                        //AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);
                    }
                }


                //AutoFade.LoadLevel("MultiPlayScene", 0.2f, 0.2f, Color.black);
            }
        }
  

    }

    // 터치에서 손을 땠을때 발생하는 함수
    public virtual void OnPointerUp(PointerEventData ped)
    {

    }
}
