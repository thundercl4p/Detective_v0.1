using UnityEngine;
using UnityEngine.EventSystems;


public class TouchLook : MonoBehaviour
{

    private Touch initTouch = new Touch();
    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;

    public Camera cam;
    public float rotSpeed = 0.5f;
    public float dir = -1;

    void Start()
    {
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began) // Палец только что коснулся к экрану
            {
                initTouch = touch;
            }

            else if (touch.phase == TouchPhase.Moved) // Палец передвинулся по экрану
            {
                float deltaX = initTouch.position.x - touch.position.x;
                float deltaY = initTouch.position.y - touch.position.y;

                rotX -= deltaY * Time.deltaTime * rotSpeed * dir * (-1);
                rotY -= deltaX * Time.deltaTime * rotSpeed * dir;

                rotX = Mathf.Clamp(rotX, -45f, 45f);
                transform.eulerAngles = new Vector3(rotX, rotY, 0f);


            }

            else if (touch.phase == TouchPhase.Ended) // Палец только что оторван от экрана
            {
                initTouch = new Touch();
            }
        }
    }
}
