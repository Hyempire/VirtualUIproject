using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGrabbedPosition : MonoBehaviour
{
    public Vector3 grabPosition;
    public Quaternion grabRotation;

    // Start is called before the first frame update
    void Start()
    {
        grabPosition = gameObject.transform.position;
        grabRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabbedPosition()
    {
        grabPosition = gameObject.transform.position;
        grabRotation = gameObject.transform.rotation;
        Debug.Log(grabPosition.ToString());
    }

}
