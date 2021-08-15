using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float decreaseScale;

    Vector3 direction;
    Vector3 startScale;
    float startMouseHigh;
    const float maxScale = 2.5f;
    const float minScale = 0.35f;
    float lastScale = 1;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = gameObject.transform.localScale;
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMoving();
        ControlScale();
    }

    void ControlMoving()
    {
        gameObject.transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    void ControlScale()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMouseHigh = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(0))
        {
            scale = lastScale + (Input.mousePosition.y - startMouseHigh) / decreaseScale;
            if (scale > maxScale) scale = maxScale;
            if (scale < minScale) scale = minScale;
            if (scale != 0)
            {
                gameObject.transform.localScale = new Vector3(startScale.x, startScale.y * scale, startScale.z / scale);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastScale = scale;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Turn right")
        {
            gameObject.transform.Rotate(new Vector3(0, 90, 0));
            //gameObject.transform.RotateAroundLocal(Vector3.up, 90);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0;
        }
    }
}