using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Folder

public class PutFileIntoFolder : MonoBehaviour
{

    public bool isFileEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bbbbbb");
        if (other.tag == "File")
        {
            isFileEnter = true;
            Destroy(other.gameObject);
            Debug.Log("aaaaa");
        }
        else
        {
            Debug.Log("bbbbbb");
        }
    }
}
