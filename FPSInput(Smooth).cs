using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    private IEnumerator coroutine;
    // Возможность передвижения свайпами <---СВАЙПЫ
    private Vector3 fp;   //Первая позиция касания
    private Vector3 lp;   //Последняя позиция касания
    private float dragDistance;  //Минимальная дистанция для определения свайпа
    private List<Vector3> touchPositions = new List<Vector3>(); //Храним все позиции касания в списке --->

    public int swypesens = 20; // Сколько процентов экрана отводится на свайп
    public float speed = 40.0f;
    public float gravity = -9.8f;
    public float smoothing = 2;
    private Rigidbody _charController; // Для распознования столкновений
   

    public float step;
    private float progress;
    
    private Vector3 EndPos;



    void Start()
    {
        _charController = GetComponent<Rigidbody>(); // Доступ к другим компонентам, присоединенным к этому объекту
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
                            
                            _charController.AddForce(100, 0, 0);

                            //StartCoroutine(SmoothMove(transform.position, movement, 2));

                            Debug.Log("Right Swipe");
                        }
                        else if (lp.x < fp.x)
                        {   //Свайп влево
                           


                            _charController.AddForce(-100, 0, 0);
                            // StartCoroutine(SmoothMove(transform.position, movement, 2));

                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {   //Если вертикальное движение больше, чнм горизонтальное движение
                        if (lp.y > fp.y)  //Если движение вверх
                        {   //Свайп вверх
                            



                            //StartCoroutine(SmoothMove(transform.position, movement, 2));

                            _charController.AddForce(0, 0, 100);




                            Debug.Log("Up Swipe");
                        }
                        else if (lp.y < fp.y)
                        {   //Свайп вниз
                           
                            _charController.AddForce(0, 0, -100);
                            //StartCoroutine(SmoothMove(transform.position, movement, 2));
                            Debug.Log("Down Swipe");
                        }
                    }
                }
            }
            else
            {   //Это ответвление, как расстояние перемещения составляет менее 20% от высоты экрана

            }
        }
       
    }

//    IEnumerator SmoothMove(Vector3 startPos,Vector3 endPos, float time)
//{
        
//        float currTime = 0;
// do {
//            Vector3 kek;
//           kek = Vector3.Lerp(startPos, endPos, currTime/time);   НЕ УДОЛЯТЬ
//            // kek = transform.TransformDirection(kek);
//            _charController.AddForce(kek);
//            currTime += Time.deltaTime;
//  yield return null; 
// }
// while (currTime<=time);
//}
}


