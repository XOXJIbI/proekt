
using UnityEngine;

public class zoom : MonoBehaviour
{
    public Camera target;
    private Vector3 prevTouch1;
    private Vector3 prevTouch2;
    public float zoom_speed = 1f;
    public float Size;
    // Start is called before the first frame update
    void Start()
    {
        target=Camera.main;
        Size = target.orthographicSize;
    }

    // Update is called once per frame
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

                // Меняем размер ортографической камеры в зависимости от разницы в расстоянии между пальцами
                target.orthographicSize = Mathf.Clamp(target.orthographicSize - difference * zoom_speed* Time.deltaTime, 1f, Mathf.Infinity);

                prevTouch1 = currentTouch1;
                prevTouch2 = currentTouch2;
            }
        }
    }
}
