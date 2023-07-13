using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowIcon : MonoBehaviour
{
    public GameObject leftWristObject;
    public float throwForce = 1.0f;
    private HandJointManager handJointManager;
    private bool isThrown;

    // Start is called before the first frame update
    void Start()
    {
        handJointManager = leftWristObject.GetComponent<HandJointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isThrown = handJointManager.isThrown;
        throwIcon(isThrown);
    }

    public void throwIcon(bool isThrown)
    {
        if (isThrown == true)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(leftWristObject.transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}
