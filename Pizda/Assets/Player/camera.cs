using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 0.5f;
    public Vector3 offset = new Vector3(0.5f, 0, 0.5f);
    public bool isLeft;
    private bool isDown;
    private Transform player;
    private int lastX;
    private int lastZ;

    void Start()
    {
        offset = new Vector3(Mathf.Abs(offset.x), offset.y, offset.z);
        FindPlayer(isLeft, isDown);
    }

    public void FindPlayer(bool playerIsLeft, bool playerIsDown)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        lastZ = Mathf.RoundToInt(player.position.z);
        if (playerIsLeft)
        {
            if (playerIsDown)
            {
                transform.position = new Vector3(player.position.x - offset.x, transform.position.y, player.position.z - offset.z);
            }
            else if (!playerIsDown)
            {
                transform.position = new Vector3(player.position.x - offset.x, transform.position.y, player.position.z + offset.z);
            }
            else
            {
                transform.position = new Vector3(player.position.x - offset.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (playerIsDown)
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z - offset.z);
            }
            else if (!playerIsDown)
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);
            }
            else
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            }
        }
    }

    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
            lastX = currentX;
            int currentZ = Mathf.RoundToInt(player.position.z);
            if (currentZ > lastZ) isDown = false; else if (currentZ < lastZ) isDown = true;
            lastZ = currentZ;

            // ѕровер€ем, стоит ли персонаж на месте
            bool isPlayerIdle = Mathf.Approximately(player.GetComponent<Rigidbody>().velocity.magnitude, 0f);

            if (!isPlayerIdle)
            {
                // ≈сли персонаж не стоит на месте, обновл€ем положение камеры
                Vector3 target;
                if (isLeft)
                {
                    if (isDown)
                    {
                        target = new Vector3(player.position.x - offset.x, transform.position.y, player.position.z - offset.z);
                    }
                    else if (!isDown)
                    {
                        target = new Vector3(player.position.x - offset.x, transform.position.y, player.position.z + offset.z);
                    }
                    else
                    {
                        target = new Vector3(player.position.x - offset.x, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (isDown)
                    {
                        target = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z - offset.z);
                    }
                    else if (!isDown)
                    {
                        target = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);
                    }
                    else
                    {
                        target = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
                    }
                }

                Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
                transform.position = currentPosition;
            }
        }
    }
}