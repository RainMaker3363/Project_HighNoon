using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Upgrade_MaxBullet_Button : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public SoundManager SDManager;
    public AudioClip Command_Touch_Sound;

    public GameObject Upgrade_Dialog;

	// Use this for initialization
	void Start () {
        
	}

    void Enable()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

     // 터치를 하고 있을 대 발생하는 함수
    public virtual void OnPointerDown(PointerEventData ped)
    {
        SDManager.PlaySfx(Command_Touch_Sound);

        GameManager.NowPlayerMaxBulletQuantity++;

        GameManager.MiniGame_Upgrade_End = true;
        Upgrade_Dialog.SetActive(false);
    }

    // 터치가 드래그(Drag) 했을때 호출 되는 함수
    public virtual void OnDrag(PointerEventData ped)
    {

    }

    // 터치에서 손을 땠을때 발생하는 함수
    public virtual void OnPointerUp(PointerEventData ped)
    {

    }
}
