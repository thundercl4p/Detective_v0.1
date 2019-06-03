using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    // Возможность передвижения свайпами <---СВАЙПЫ
    private Vector3 fp;   //Первая позиция касания
    private Vector3 lp;   //Последняя позиция касания
    private float dragDistance;  //Минимальная дистанция для определения свайпа
    private List<Vector3> touchPositions = new List<Vector3>(); //Храним все позиции касания в списке --->

    public int swypesens = 20; // Сколько процентов экрана отводится на свайп
    public float speed = 40.0f;
    public float gravity = -9.8f;
    private CharacterController _charController; // Для распознования столкновений
    void Start()
    {
        _charController = GetComponent<CharacterController>(); // Доступ к другим компонентам, присоединенным к этому объекту
        dragDistance = Screen.height / 2 * swypesens / 100; //dragDistance это 20% высоты экрана <---СВАЙПЫ--->
        
    }
    void Update()
    {

        // <--СВАЙПЫ

        foreach (Touch touch in Input.touches)  //используем цикл для отслеживания больше одного свайпа
        {

            if (touch.phase == TouchPhase.Moved) //добавляем касания в список, как только они определены
            {
                touchPositions.Add(touch.position);
            }

            if (touch.phase == TouchPhase.Ended) //проверяем, если палец убирается с экрана
            {
                fp = touchPositions[0]; //получаем первую позицию касания из списка касаний
                lp = touchPositions[touchPositions.Count - 1]; //позиция последнего касания

                //проверяем дистанцию перемещения больше чем 20% высоты экрана
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//это перемещение
                 //проверяем, перемещение было вертикальным или горизонтальным 
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //Если горизонтальное движение больше, чем вертикальное движение ...
                        if (lp.x > fp.x)  //Если движение было вправо
                        {   //Свайп вправо

                            SwypeMovement(1, 0);
                            Debug.Log("Right Swipe");
                        }
                        else if (lp.x < fp.x)
                        {   //Свайп влево

                            SwypeMovement(-1, 0);
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {   //Если вертикальное движение больше, чнм горизонтальное движение
                        if (lp.y > fp.y)  //Если движение вверх
                        {   //Свайп вверх

                            SwypeMovement(0, 1);
                            Debug.Log("Up Swipe");
                        }
                        else if (lp.y < fp.y)
                        {   //Свайп вниз
                            SwypeMovement(0, -1);
                            Debug.Log("Down Swipe");
                        }
                    }
                }
            }
            else
            {   //Это ответвление, как расстояние перемещения составляет менее 20% от высоты экрана

            }
        }

        // -->
        /*
        float deltaX = Input.GetAxis("Horizontal") * speed; // Ввод с клавиатуры A/D
        float deltaZ = Input.GetAxis("Vertical") * speed;   // Ввод с клавиатуры W/S

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); // Скорость по диагонали = скорости по осям (Ограничение скорости движения вообще)

        movement.y = gravity;  
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);  // Преобразование локальных координат в глобальные

        _charController.Move(movement);
        */       
    }
    void SwypeMovement(int ox, int oz)
    {
        Vector3 movement = new Vector3(ox * speed, 0, oz * speed);
        movement = Vector3.ClampMagnitude(movement, speed); // Скорость по диагонали = скорости по осям (Ограничение скорости движения вообще)

        movement.y = gravity;
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);  // Преобразование локальных координат в глобальные

        _charController.Move(movement);
    }

}

