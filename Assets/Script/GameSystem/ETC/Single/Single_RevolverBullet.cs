using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Single_RevolverBullet : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public SoundManager SDManager;
    public AudioClip Command_Touch_Sound;

    public Text MyNumberText;
    public int MyNumber;

	// Use this for initialization
	void Start () {
        MyNumberText.text = MyNumber.ToString();
	}

    void Enable()
    {
        MyNumberText.text = MyNumber.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        MyNumberText.text = MyNumber.ToString();
	}

     // 터치를 하고 있을 대 발생하는 함수
    public virtual void OnPointerDown(PointerEventData ped)
    {
        if(MyNumber == Single_RevolverTouch.BulletChecker)
        {
            SDManager.PlaySfx(Command_Touch_Sound);

            this.gameObject.SetActive(false);
            Single_RevolverTouch.BulletChecker++;
        }
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
