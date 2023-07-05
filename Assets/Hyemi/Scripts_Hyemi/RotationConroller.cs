using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationConroller : MonoBehaviour
{
    public bool canRotateStart = true;

    Vector3 currentAngle;
    Vector3 prevAngle;
    Vector3 targetAngle;
    Vector3 startAngle = new Vector3(0, 0, 0);
    Vector3 endAngle = new Vector3(0,0,0);

    float ratationScale = 200.0f;
    float rotateSpeed = 10.0f;
    float rotateTrigger = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.rotation.eulerAngles;
        targetAngle = new Vector3(currentAngle.x, currentAngle.y, currentAngle.z + ratationScale);

        // 손에서 놓으면 회전 시작
        if (canRotateStart == true)
        {
            Debug.Log("canRotateStart = true");

            // 어느 방향으로 회전하고 있는지 판단
            if (endAngle.y - startAngle.y < 0) rotateTrigger = 1;
            if (endAngle.y - startAngle.y > 0) rotateTrigger = -1;

            if (rotateTrigger == 1)
            {
                Debug.Log("direction1");

                // 회전
                transform.eulerAngles = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, currentAngle.x, Time.deltaTime * rotateSpeed),
                    Mathf.LerpAngle(currentAngle.y, currentAngle.y + ratationScale, Time.deltaTime * rotateSpeed),
                    Mathf.LerpAngle(currentAngle.z, currentAngle.z, Time.deltaTime * rotateSpeed));
                rotateSpeed *= 0.99f;
            }
            if (rotateTrigger == -1)
            {
                Debug.Log("direction2");

                // 회전
                transform.eulerAngles = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, currentAngle.x, Time.deltaTime * rotateSpeed),
                    Mathf.LerpAngle(currentAngle.y, currentAngle.y - ratationScale, Time.deltaTime * rotateSpeed),
                    Mathf.LerpAngle(currentAngle.z, currentAngle.z, Time.deltaTime * rotateSpeed));
                rotateSpeed *= 0.99f;
            }
        }
        else Debug.Log("canRotateStart = false");

        prevAngle = transform.rotation.eulerAngles;

        //float diff = endAngle.y - startAngle.y;
        //Debug.Log("end" + endAngle + "start" + startAngle + diff);
    }

    public void whenRotateStopped()
    {
        canRotateStart = true;  // 손에서 놓음
        endAngle = transform.rotation.eulerAngles;
    }
    public void whenRotateStarted()
    {
        canRotateStart = false;
        startAngle = transform.rotation.eulerAngles;
        rotateSpeed = 15.0f;
        rotateTrigger = 0;
    }
}

