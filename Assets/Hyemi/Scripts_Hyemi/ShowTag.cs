using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class ShowTag : MonoBehaviour
{
    const float PinchThreshold = 0.7f;
    const float GrabThreshold = 0.4f;

    Handedness trackedHand = Handedness.Both;

    public GameObject mainCamera;
    public GameObject iconObject;
    public GameObject property;
    public TextMeshProUGUI debugText;

    float angle;

    // Start is called before the first frame update
    void Start()
    {
        property.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        angle = Vector3.Angle(iconObject.transform.TransformDirection(Vector3.right), mainCamera.transform.TransformDirection(Vector3.right));
        debugText.text = angle.ToString();

        if (IsPinching(trackedHand))
        {
            Debug.Log("Pinched");
            if (angle >= 140.0f)
            {
                property.SetActive(true);
            }
            else property.SetActive(false);
        }
        else property.SetActive(false);
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
