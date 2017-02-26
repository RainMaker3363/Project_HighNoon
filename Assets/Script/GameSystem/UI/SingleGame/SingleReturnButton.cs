﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SingleReturnButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public SoundManager SDManager;
    public AudioClip UI_Touch_Sound;

    public GameObject Exit_Dialog_Object;
    private GameState state;

    // Use this for initialization
    void Start()
    {
        state = GameManager.NowGameState;
        Exit_Dialog_Object.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        state = GameManager.NowGameState;
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
        switch(state)
        {
            case GameState.START:
                {
                    //GameManager.NowGameState = GameState.PAUSE;

                    //Exit_Dialog_Object.SetActive(true);
        
                }
                break;

            case GameState.PLAY:
                {
                    SDManager.PlaySfx(UI_Touch_Sound);

                    GameManager.NowGameState = GameState.PAUSE;

                    Exit_Dialog_Object.SetActive(true);
        
                }
                break;

            case GameState.PAUSE:
                {

                }
                break;

            case GameState.EVENT:
                {
                    //GameManager.NowGameState = GameState.EVENT;

                    //Exit_Dialog_Object.SetActive(true);
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

        //AutoFade.LoadLevel("MainMenuScene", 0.3f, 0.3f, Color.black);
    }

    // 터치에서 손을 땠을때 발생하는 함수
    public virtual void OnPointerUp(PointerEventData ped)
    {

    }
}
