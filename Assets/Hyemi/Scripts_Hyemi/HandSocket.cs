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

        // palm position ���
        HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out palmPose);
        // ���� position ���
        HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out middleTipPose);

        // ���� ������Ʈ ��ġ
        socketBox.transform.position = palmPose.Position;
        socketBox.transform.rotation = palmPose.Rotation;
        // ���� �� cube ��ġ
        leftMiddleTip.transform.position = middleTipPose.Position;
        leftMiddleTip.transform.rotation = middleTipPose.Rotation;

        // �չٴ��� ������ ���� �� SocketBox�� Ȱ��ȭ�ǰ� �ϱ�
        // �չٴ� ���� cube�� �����հ��� ���� cube�� z�� ������ ȸ������ ���غ���
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
    // trigger enter ���� �� ���� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Folder")
        {
            socketBox.gameObject.GetComponent<XRSocketInteractor>().enabled = false;
        }
    }

    // trigger exit ���� �� ���� �ѱ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Folder")
        {
            socketBox.gameObject.GetComponent<XRSocketInteractor>().enabled = true;
        }
    }
    */
}
