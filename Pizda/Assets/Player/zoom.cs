using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Camera target;
    private Vector3 prevTouch1;
    private Vector3 prevTouch2;
    public float zoom_speed = 1f;
    public float Size;

    // Задаём прямоугольную область, в которой зум не будет работать
    public Rect excludedArea;

    void Start()
    {
        target = Camera.main;
        Size = target.orthographicSize;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        { 
            // Получаем информацию о касаниях
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Если это начало касания
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                prevTouch1 = touch1.position;
                prevTouch2 = touch2.position;
            }
            // Если это перемещение пальцев
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 currentTouch1 = touch1.position;
                Vector2 currentTouch2 = touch2.position;

                float prevMagnitude = (prevTouch1 - prevTouch2).magnitude;
                float currentMagnitude = (currentTouch1 - currentTouch2).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                // Проверяем, находится ли текущее касание в исключенной области (облась джойстика)
                if (!IsPointInExcludedArea(currentTouch1) && !IsPointInExcludedArea(currentTouch2))
                {
                    // Меняем размер ортографической камеры, только если оба касания вне исключенной области
                    target.orthographicSize = Mathf.Clamp(target.orthographicSize - difference * zoom_speed * Time.deltaTime, 1f, 10f);
                }

                prevTouch1 = currentTouch1;
                prevTouch2 = currentTouch2;
            }
        }
    }

    // Проверка, находится ли точка в исключенной области
    private bool IsPointInExcludedArea(Vector2 point)
    {
        return excludedArea.Contains(point);
    }
}