using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NewFolderGrabButton

public class NewFolderButton : MonoBehaviour
{
    bool isRightTriggered = false;
    bool isThumbTriggered = false;
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

        if (isRightTriggered && isThumbTriggered)
        {
            Instantiate(prefab_, initiatePosition.position, Quaternion.Euler(-90, 0, 0), null);
            isRightTriggered = false;
            isThumbTriggered = false;

            copyAvailable = false;
            collider_.enabled = false;
            timer = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (copyAvailable)
        {
            if (other.gameObject.name == "Right IndexTip")
                isRightTriggered = true;
            if (other.gameObject.name == "Right ThumbTip")
                isThumbTriggered = true;
        }
        else
        {
            isRightTriggered = false;
            isThumbTriggered = false;
        }
    }
}
