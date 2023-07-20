using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyIcon : MonoBehaviour
{
    public GameObject socketBox;
    HandSocket handSocket;
    public bool isSocketSelected;

    // Start is called before the first frame update
    void Start()
    {
        handSocket = socketBox.GetComponent<HandSocket>();
    }

    // Update is called once per frame
    void Update()
    {
        isSocketSelected = handSocket.isSocketSelected;

        if (isSocketSelected == true)
        {
            Instantiate(gameObject);
            isSocketSelected = false;
        }
    }
}
