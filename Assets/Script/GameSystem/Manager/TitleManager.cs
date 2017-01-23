﻿using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {

                        // 할꺼 하셈

                        // Application.Quit();

                    }

                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        switch (touch.phase)
                        {
                                // 화면을 터치하는 순간 체크!
                            case TouchPhase.Began:
                                {
                                    Debug.Log("TouchPhase Began!");

                                    AutoFade.LoadLevel("TestScene", 1.0f, 0.5f, Color.black);
                                }
                                break;

                                // 사용자가 터치하고 드래그를 했을때
                            case TouchPhase.Moved:
                                {
                                    Debug.Log("TouchPhase Moved!");
                                }
                                break;

                                // Moved 상태이다가 손을 땠을때 발생
                            case TouchPhase.Stationary:
                                {
                                    Debug.Log("TouchPhase Stationary!");
                                }
                                break;

                                // 사용자가 터치를 하고 손을 땠을때의 상황
                            case TouchPhase.Ended:
                                {
                                    Debug.Log("TouchPhase Ended!");
                                }
                                break;

                                // 시스템에 의해 터치가 취소된 경우다. 즉 사용자가 화면에서 손을 뗀 것이 아니라, 전화가 왔다던지 등등 이러한 상황일 때다.
                            case TouchPhase.Canceled:
                                {
                                    Debug.Log("TouchPhase Canceled!");
                                }
                                break;
                        }
                    }
                }
                break;

            default:
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {

                        // 할꺼 하셈

                        // Application.Quit();

                    }

                    if(Input.anyKeyDown)
                    {
                        AutoFade.LoadLevel("TestScene", 1.0f, 0.5f, Color.black);
                    }
                }
                break;
        }
    }
}