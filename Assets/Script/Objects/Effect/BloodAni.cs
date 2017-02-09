using UnityEngine;
using System.Collections;

public class BloodAni : MonoBehaviour {

    private GameState State;
    private GameControlState ControlState;

    public AnimateTiledTexture _animatedTileTexture;

	// Use this for initialization
	void Start () {

        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            _animatedTileTexture.RegisterCallback(AnimationFinished);

            _animatedTileTexture._enableEvents = true;
            //_animatedTileTexture.RegisterCallback(ShootAnimationFinished);
        }

        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        this.gameObject.SetActive(false);
	}

    void OnEnable()
    {
        if (_animatedTileTexture == null)
        {
            Debug.LogWarning("No animated tile texture script assigned!");
        }
        else
        {
            _animatedTileTexture.RegisterCallback(AnimationFinished);

            _animatedTileTexture._enableEvents = true;
            //_animatedTileTexture.RegisterCallback(ShootAnimationFinished);
        }

        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;
    }

    void AnimationFinished()
    {
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        State = GameManager.NowGameState;
        ControlState = GameManager.NowGameControlState;

        switch(State)
        {
            case GameState.START:
                {

                }
                break;

            case GameState.PLAY:
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
}
