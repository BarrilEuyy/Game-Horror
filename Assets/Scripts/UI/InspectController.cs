using UnityEngine;
using UnityEngine.UI;

public class InspectController : MonoBehaviour
{
    [Header("References")]
    public Camera inspectCam;
    public Slider zoomSlider;
    public Transform modelToInspect;

    [Header("Rotation Settings")]
    public float rotationSpeed = 0.2f;
    private Vector2 lastTouchPos;
    private bool isDragging = false;

    void Start()
    {
        if (inspectCam != null && zoomSlider != null)
        {
            inspectCam.fieldOfView = zoomSlider.value;
            zoomSlider.onValueChanged.AddListener(UpdateZoom);
        }
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPos = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging && modelToInspect != null)
            {
                if (modelToInspect.childCount != 0)
                {
                    Vector2 delta = touch.position - lastTouchPos;
                    modelToInspect.GetChild(0).transform.Rotate(Vector3.up, -delta.x * rotationSpeed, Space.World);
                    modelToInspect.GetChild(0).transform.Rotate(Vector3.right, delta.y * rotationSpeed, Space.World);
                    lastTouchPos = touch.position;
                }
            }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                }
        }
    }

    void UpdateZoom(float value)
    {
        if (inspectCam != null)
            inspectCam.fieldOfView = value;
    }
}
