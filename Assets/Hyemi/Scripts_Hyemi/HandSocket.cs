using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class HandSocket : MonoBehaviour
{
    public GameObject socketBox;
    public GameObject leftMiddleTip;
    public TextMeshProUGUI debugText;

    MixedRealityPose palmPose;
    MixedRealityPose middleTipPose;
    public bool isSocketSelected;

    private GameObject enteredIcon;
    private GameObject copiedIcon;
    private float angle;
    private Vector3 palmAngle;
    private SaveGrabbedPosition grabbedPosition;
    private Vector3 instantiatePos;
    private Quaternion instantiateRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // palm position 얻기
        HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out palmPose);
        // 중지 position 얻기
        HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out middleTipPose);

        // 소켓 오브젝트 위치
        socketBox.transform.position = palmPose.Position;
        socketBox.transform.rotation = palmPose.Rotation;
        // 중지 팁 cube 위치
        leftMiddleTip.transform.position = middleTipPose.Position;
        leftMiddleTip.transform.rotation = middleTipPose.Rotation;

        // 손바닥이 펼쳐져 있을 때 SocketBox가 활성화되게 하기
        // 손바닥 점의 cube와 중지손가락 끝의 cube의 z축 벡터의 회전값을 비교해보자
        angle = Vector3.Angle(socketBox.transform.TransformDirection(Vector3.forward), leftMiddleTip.transform.TransformDirection(Vector3.forward));
        palmAngle = socketBox.transform.TransformDirection(Vector3.right);
        Debug.Log(angle.ToString());
        debugText.text = palmAngle.ToString();
        if ((angle <= 43) && (Mathf.Abs(palmAngle.y) <= 0.3f))
        {
            debugText.text = "Socket enabled";
            socketBox.GetComponent<XRSocketInteractor>().enabled = true;
        }
        else
        {
            socketBox.GetComponent<XRSocketInteractor>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enteredIcon = other.gameObject;
        grabbedPosition = enteredIcon.GetComponent<SaveGrabbedPosition>();
        instantiatePos = grabbedPosition.grabPosition;
        instantiateRot = grabbedPosition.grabRotation;
    }
    public void IsSocketSelected()
    {
        //isSocketSelected = true;
        

        copiedIcon = Instantiate(enteredIcon, instantiatePos, instantiateRot);
        debugText.text = instantiatePos.ToString();

        enteredIcon.GetComponent<Rigidbody>().drag = 9999999f;
        enteredIcon.GetComponent<Rigidbody>().angularDrag = 9999999f;
        copiedIcon.GetComponent<Rigidbody>().drag = 9999999f;
        copiedIcon.GetComponent<Rigidbody>().angularDrag = 9999999f;


        //gameObject.GetComponent<XRSocketInteractor>().enabled = false;
    }


    /*
    // trigger enter 했을 때 소켓 끄기
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Folder")
        {
            socketBox.gameObject.GetComponent<XRSocketInteractor>().enabled = false;
        }
    }

    // trigger exit 했을 때 소켓 켜기
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Folder")
        {
            socketBox.gameObject.GetComponent<XRSocketInteractor>().enabled = true;
        }
    }
    */
}
