﻿using UnityEngine;
using System.Collections;

public class PlayerAni : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    public AnimateTiledTexture _animatedTileTexture;

    [HideInInspector]
    public PlayerState playerState;
    private AnimationState PlayerAniState;

    private Player m_Player;
    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
	
        if(m_Player == null)
        {
            m_Player = GameObject.FindWithTag("Player").GetComponent<Player>();

            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            PlayerAniState = m_Player.GetPlayerAniState();

            this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
        }
        else
        {
            State = GameManager.NowGameState;
            ControlState = GameManager.NowGameControlState;

            PlayerAniState = m_Player.GetPlayerAniState();

            this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
        }

        if (MainCamera == null)
        {
            MainCamera = GameObject.FindWithTag("MainCamera");
        }

        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            _animatedTileTexture.RegisterCallback(AnimationFinished);
        }

        
        

        this.transform.LookAt(MainCamera.transform.position);
        this.transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
	}

    // This function will get called by the AnimatedTiledTexture script when the animation is completed if the EnableEvents option is set to true
    void AnimationFinished()
    {
        // The animation is finished
    }
	
	// Update is called once per frame
	void Update () 
    {
        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        playerState = m_Player.GetPlayerState();
        PlayerAniState = m_Player.GetPlayerAniState();

        

        switch (State)
        {
            case GameState.START:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.PLAY:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);

                    switch(PlayerAniState)
                    {
                        case AnimationState.DOWNSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(1);
                                //_animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.UPSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(2);
                            }
                            break;

                        case AnimationState.LEFTSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(4);
                            }
                            break;

                        case AnimationState.RIGHTSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(3);
                            }
                            break;

                        case AnimationState.LEFTDOWNSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(6);
                            }
                            break;

                        case AnimationState.LEFTUPSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(8);
                            }
                            break;

                        case AnimationState.RIGHTUPSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(7);
                            }
                            break;

                        case AnimationState.RIGHTDOWNSTAND:
                            {
                                _animatedTileTexture.ChangeCheckRow(5);
                            }
                            break;

                        case AnimationState.LEFTWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(12);
                            }
                            break;

                        case AnimationState.RIGHTWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(11);
                            }
                            break;

                        case AnimationState.UPWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(10);
                            }
                            break;

                        case AnimationState.DOWNWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(9);
                            }
                            break;

                        case AnimationState.LEFTUPWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(16);
                            }
                            break;

                        case AnimationState.LEFTDOWNWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(14);
                            }
                            break;

                        case AnimationState.RIGHTUPWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(15);
                            }
                            break;

                        case AnimationState.RIGHTDOWNWALK:
                            {
                                _animatedTileTexture.ChangeCheckRow(13);
                            }
                            break;

                        case AnimationState.LEFTSHOOT:
                            {

                            }
                            break;

                        case AnimationState.RIGHTSHOOT:
                            {

                            }
                            break;

                        case AnimationState.DOWNSHOOT:
                            {

                            }
                            break;

                        case AnimationState.UPSHOOT:
                            {

                            }
                            break;

                        case AnimationState.LEFTUPSHOOT:
                            {

                            }
                            break;

                        case AnimationState.LEFTDOWNSHOOT:
                            {

                            }
                            break;

                        case AnimationState.RIGHTUPSHOOT:
                            {

                            }
                            break;

                        case AnimationState.RIGHTDOWNSHOOT:
                            {

                            }
                            break;

                        case AnimationState.SHOOTING:
                            {

                            }
                            break;

                        case AnimationState.DEADEYING:
                            {
                                _animatedTileTexture.ChangeCheckRow(1);
                            }
                            break;

                        case AnimationState.LEFTDEAD:
                            {

                            }
                            break;

                        case AnimationState.UPDEAD:
                            {

                            }
                            break;

                        case AnimationState.DOWNDEAD:
                            {

                            }
                            break;

                        case AnimationState.RIGHTDEAD:
                            {

                            }
                            break;
                    }
                }
                break;

            case GameState.PAUSE:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.EVENT:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.GAMEOVER:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;

            case GameState.VICTORY:
                {
                    this.transform.position = new Vector3(m_Player.gameObject.transform.position.x, m_Player.gameObject.transform.position.y + 0.2f, m_Player.gameObject.transform.position.z);
                }
                break;
        }
	}
}
