using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

public class HandJointManager : MonoBehaviour
{
    //TrackedHandJoint trackedHandJoint;
    //Handedness trackedHand = Handedness.Both;
    
    public GameObject leftWrist;
    public GameObject rightPinkyKnuckle;
    public GameObject rightPinkyKnuckle_temp;
    public TextMeshProUGUI debugText;
    private bool isThrowTriggered = false;
    public bool isThrown = false;


    const float TIMELIMIT = 0.5f;
    private float timeRemaining = TIMELIMIT;

    MixedRealityPose LWpose;
    MixedRealityPose RPKpose;

    Renderer leftWristRenderer;

    //Transform startPos;
    //Transform endPos;


    // Start is called before the first frame update
    void Start()
    {
        leftWristRenderer = leftWrist.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // hand joint�� position ���
        HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out LWpose);
        HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Right, out RPKpose);

        // ���� �ո�, �������� �ճ� �� joint�� position�� "���ӿ�����Ʈ"�� �ǽð����� ��ġ ��Ű�� (���ӿ�����Ʈ�� �޽��� ���� �ݶ��̴��� ����. isTrigger üũ)
        leftWrist.transform.position = LWpose.Position;
        leftWrist.transform.rotation = LWpose.Rotation;
        
        rightPinkyKnuckle.transform.position = RPKpose.Position;
        rightPinkyKnuckle.transform.rotation = RPKpose.Rotation;

        // "���� ������Ʈ"�鳢�� ����� �� ���ͷ��� Ʈ���� �ߵ�. �� ���� ������ palm�� ��ġ�� �����ϰ�, Ÿ�̸Ӹ� �۵� ��Ų�ٵ���..
        if (isThrowTriggered == true)
        {
            DetectThrow();
        }
        //debugText.text = isThrowTriggered.ToString();

        // �������� ���� �������� �����̸� ���ͷ��� �ϼ�!
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rightPinkyKnuckle)
        {
            Debug.Log("Entered");
            leftWristRenderer.material.color = Color.red;
            isThrowTriggered = true;
            //debugText.text = isThrowTriggered.ToString();

            //startPos = rightPinkyKnuckle.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == rightPinkyKnuckle)
        {
            Debug.Log("Exited");
            leftWristRenderer.material.color = Color.gray;
        }
    }

    void DetectThrow()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            debugText.text = timeRemaining.ToString();

            float dist = rightPinkyKnuckle.transform.position.z - leftWrist.transform.position.z;
            //debugText.text = dist.ToString();
        }
        else
        {
            timeRemaining = TIMELIMIT;
            //endPos = rightPinkyKnuckle.transform;

            isThrowTriggered = false;
            //debugText.text = isThrowTriggered.ToString();
            //endPos = rightPinkyKnuckle.transform;

            DetectMovement();
            return;
        }
    }

    void DetectMovement()
    {
        float dist = rightPinkyKnuckle.transform.position.z - leftWrist.transform.position.z; ;
        //debugText.text = dist.ToString();

        if (dist > 0.05f)
        {
            debugText.text = "Throw!";

            // ����ٰ� ������ ���� Ƣ����� �ڵ� �ֱ�!!!!!!
            isThrown = true;

            return;
        }
    }
}
