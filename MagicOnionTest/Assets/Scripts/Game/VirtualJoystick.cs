using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;

    public Vector3 InputDirection { get; set; }

    private static VirtualJoystick _instance;
    public static VirtualJoystick Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            float x = (bgImg.rectTransform.pivot.x == 1 ? pos.x * 2 + 1 : pos.x * 2 - 1); //Sinistra o destra
            float y = (bgImg.rectTransform.pivot.y == 1 ? pos.y * 2 + 1 : pos.y * 2 - 1); 

            InputDirection = new Vector3(x, 0, y);

            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            joystickImg.rectTransform.anchoredPosition = new Vector3(
                InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3),
                InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));

            Debug.Log(InputDirection);
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public bool IsActiveInScene()
    {
        return InputDirection != Vector3.zero;
    }

    public bool IsMovingW()
    {
        return InputDirection.x == 0 && InputDirection.y == 0 && InputDirection.z > 0;
    }

    public bool IsMovingWD()
    {
        return InputDirection.x > 0 && InputDirection.y == 0 && InputDirection.z > 0;
    }

    public bool IsMovingWA()
    {
        return InputDirection.x < 0 && InputDirection.y == 0 && InputDirection.z > 0;
    }

    public bool IsMovingSA()
    {
        return InputDirection.x < 0 && InputDirection.y == 0 && InputDirection.z < 0;
    }

    public bool IsMovingSD()
    {
        return InputDirection.x > 0 && InputDirection.y == 0 && InputDirection.z < 0;
    }

    public bool IsMovingS()
    {
        return InputDirection.x == 0 && InputDirection.y == 0 && InputDirection.z < 0;
    }
}
