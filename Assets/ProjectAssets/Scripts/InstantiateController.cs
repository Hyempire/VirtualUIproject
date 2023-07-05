using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InteractionController

public class InstantiateController : MonoBehaviour
{
    public GameObject prefab_;
    public GameObject parent_;
    Transform initiatePosition;

    public bool isButtonPressed = false;

    private void Start()
    {
        initiatePosition = parent_.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonPressed)
        {
            Instantiate(prefab_, initiatePosition.position + new Vector3(0.0f, 0.0f, -0.05f), Quaternion.Euler(-90,0,0), null);
            isButtonPressed = false;
        }
    }

    public void IsButtonPressed()
    {
        isButtonPressed = true;
    }

    public void IsButtonreleased()
    {
        isButtonPressed = false;
    }
}
