using UnityEngine;
using System.Collections;

public class FlyingCamera : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;
    private GameModeState ModeState;

    private PlayerState playerState;
    //private Vector3 BackPos;

    public GameObject PlayerObject;
    private Player m_Player;

    private AudioSource Audio;
    public AudioClip InGame_BG_Sound;
    public AudioClip DeadEyeSound;
    private bool DeadEyeSoundCheck;

	// Use this for initialization
	void Start () 
    {

        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;
        ModeState = GameManager.NowGameModeState;

        if (PlayerObject == null)
        {
            PlayerObject = GameObject.FindWithTag("Player");
            m_Player = PlayerObject.GetComponent<Player>();
        }
        else
        {
            m_Player = PlayerObject.GetComponent<Player>();
        }

        if (Audio == null)
        {
            Audio = GetComponent<AudioSource>();
            Audio.loop = true;
            //Audio.clip = InGame_BG_Sound;
        }
        else
        {
            Audio.loop = true;
            //Audio.clip = InGame_BG_Sound;
        }

        DeadEyeSoundCheck = false;


        //BackPos = this.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        State = GameManager.NowGameState;
        ModeState = GameManager.NowGameModeState;
        playerState = m_Player.GetPlayerState();

        switch(ModeState)
        {
            case GameModeState.Single:
                {
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
                                            //this.transform.position = new Vector3(this.transform.position.x, BackPos.y, this.transform.position.z);
                                            //this.transform.LookAt(PlayerObject.transform.position);

                                            DeadEyeSoundCheck = false;

                                            //if (Audio.isPlaying == false)
                                            //{
                                            //    Audio.loop = true;
                                            //    Audio.clip = InGame_BG_Sound;
                                            //    Audio.Play();
                                            //}
                                        }
                                        break;

                                    case PlayerState.DEADEYE:
                                        {
                                            if (DeadEyeSoundCheck == false)
                                            {
                                                DeadEyeSoundCheck = true;

                                                if (Audio.isPlaying == false)
                                                {
                                                    Audio.loop = false;
                                                    Audio.clip = DeadEyeSound;
                                                    Audio.Play();
                                                }
                                            }


                                        }
                                        break;

                                    case PlayerState.REALBATTLE:
                                        {
                                            DeadEyeSoundCheck = false;

                                            //this.transform.LookAt(PlayerObject.transform.position);
                                            //if (Audio.isPlaying == false)
                                            //{
                                            //    Audio.loop = true;
                                            //    Audio.clip = InGame_BG_Sound;
                                            //    Audio.Play();
                                            //}

                                        }
                                        break;

                                    case PlayerState.DEAD:
                                        {

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
                break;

            case GameModeState.Multi:
                {

                }
                break;

            case GameModeState.MiniGame:
                {
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
                                            //this.transform.position = new Vector3(this.transform.position.x, BackPos.y, this.transform.position.z);
                                            //this.transform.LookAt(PlayerObject.transform.position);

                                            DeadEyeSoundCheck = false;

                                            if (Audio.isPlaying == false)
                                            {
                                                Audio.loop = true;
                                                Audio.clip = InGame_BG_Sound;
                                                Audio.Play();
                                            }
                                        }
                                        break;

                                    case PlayerState.DEADEYE:
                                        {
                                            if (DeadEyeSoundCheck == false)
                                            {
                                                DeadEyeSoundCheck = true;

                                                if (Audio.isPlaying == false)
                                                {
                                                    Audio.loop = false;
                                                    Audio.clip = DeadEyeSound;
                                                    Audio.Play();
                                                }
                                            }


                                        }
                                        break;

                                    case PlayerState.REALBATTLE:
                                        {
                                            DeadEyeSoundCheck = false;

                                            if (Audio.isPlaying == false)
                                            {
                                                Audio.loop = true;
                                                Audio.clip = InGame_BG_Sound;
                                                Audio.Play();
                                            }

                                        }
                                        break;

                                    case PlayerState.DEAD:
                                        {

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
                break;

            case GameModeState.NotSelect:
                {

                }
                break;


        }
   
        
	}
}
