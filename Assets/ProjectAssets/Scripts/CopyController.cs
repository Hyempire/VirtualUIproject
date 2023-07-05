using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Folder

public class CopyController : MonoBehaviour
{
    // Right IndexTip
    bool isRightEntered = false;
    bool isLeftEntered = false;
    bool isRightTriggered = false;
    bool isLeftTriggered = false;
    Transform initiatePosition;

    float timer = 0.0f;
    int waitingTime = 5;
    bool copyAvailable = true;

    public GameObject prefab_;
    private GameObject parent_;

    Collider collider_;

    // Start is called before the first frame update
    void Start()
    {
        parent_ = GameObject.Find("Right IndexTip");
        

        collider_ = prefab_.GetComponent<BoxCollider>();
        collider_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        initiatePosition = parent_.transform;

        if (timer > waitingTime)
        {
            copyAvailable = true;
            timer = 0.0f;
            collider_.enabled = true;
        }

        if (isRightTriggered || isLeftTriggered)
        {
            Instantiate(prefab_, initiatePosition.position, Quaternion.Euler(-90, 0, 0), null);
            isRightEntered = false;
            isLeftEntered = false;
            isRightTriggered = false;
            isLeftTriggered = false;

            copyAvailable = false;
            collider_.enabled = false;
            timer = 0.0f;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (copyAvailable)
        {
            if (other.gameObject.name == "Right IndexTip")
            {
                parent_ = GameObject.Find("Right IndexTip");
                isRightEntered = true;
            }
                
            if (other.gameObject.name == "Left IndexTip")
            {
                parent_ = GameObject.Find("Left IndexTip");
                isLeftEntered = true;
            }
                
        }
        else
        {
            isRightEntered = false;
            isLeftEntered = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLeftEntered && other.gameObject.name == "Right IndexTip")
        {
            Debug.Log("Right!!");
            isRightTriggered = true;
        }
        if (isRightEntered && other.gameObject.name == "Left IndexTip")
        {
            Debug.Log("Left!!");
            isLeftTriggered = true;
        }
    }
}
