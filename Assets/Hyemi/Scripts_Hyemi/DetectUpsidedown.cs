using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class DetectUpsidedown : MonoBehaviour
{
    const float PinchThreshold = 0.7f;
    const float GrabThreshold = 0.4f;

    Handedness trackedHand = Handedness.Both;

    public GameObject tagObject;
    public GameObject contentObject;
    public GameObject contentObject1;
    public GameObject contentObject2;
    public TextMeshProUGUI debugText;

    MixedRealityLineRenderer tagLine;

    Vector3 contentRotation;
    Vector3 myRotation;
    float contentRotationY;
    float myRotationY;
    float diff;

    // Start is called before the first frame update
    void Start()
    {
        //tagObject = GameObject.FindWithTag("IconTag");
        //contentObject = GameObject.FindWithTag("TagContent");

        tagLine = tagObject.GetComponent<MixedRealityLineRenderer>();

        contentObject1.SetActive(false);
        contentObject2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        contentRotation = contentObject.transform.rotation.eulerAngles;
        contentRotationY = contentRotation.y;
        myRotation = gameObject.transform.rotation.eulerAngles;
        myRotationY = myRotation.y;

        Debug.Log("content" + contentRotation);
        Debug.Log("my" + myRotation);
        Debug.Log(contentRotationY - myRotationY);
        diff = contentRotationY - myRotationY;
        debugText.text = diff.ToString();

        if (IsPinching(trackedHand))
        {
            Debug.Log("Pinched");

            if (Mathf.Abs(contentRotationY - myRotationY) > 300 &&
                Mathf.Abs(contentRotationY - myRotationY) < 360)        // threshold를 줄이고 싶으면 여기 숫자를 늘리고 줄이면 됨.
            {
                contentObject1.SetActive(true);
                contentObject2.SetActive(true);
                //tagLine.enabled = true;
            }
            else
            {
                //contentObject1.SetActive(false);
                //contentObject2.SetActive(false);
                tagLine.enabled = false;
            }
        }
    }

    bool IsPinching(Handedness trackedHand)
    {
        return HandPoseUtils.CalculateIndexPinch(trackedHand) > PinchThreshold;
    }
    
    
    




    // 안 쓰는 함수
    bool IsGrabbing(Handedness trackedHand)
    {
        return !IsPinching(trackedHand) &&
               HandPoseUtils.MiddleFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.RingFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.PinkyFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.ThumbFingerCurl(trackedHand) > GrabThreshold;
    }
}
