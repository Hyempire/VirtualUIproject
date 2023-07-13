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
        // hand joint의 position 얻기
        HandJointUtils.TryGetJointPose(TrackedHandJoint.Wrist, Handedness.Left, out LWpose);
        HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyKnuckle, Handedness.Right, out RPKpose);

        // 왼쪽 손목, 오른쪽의 손날 쪽 joint의 position에 "게임오브젝트"를 실시간으로 위치 시키기 (게임오브젝트는 메쉬는 없고 콜라이더만 있음. isTrigger 체크)
        leftWrist.transform.position = LWpose.Position;
        leftWrist.transform.rotation = LWpose.Rotation;
        
        rightPinkyKnuckle.transform.position = RPKpose.Position;
        rightPinkyKnuckle.transform.rotation = RPKpose.Rotation;

        // "게임 오브젝트"들끼리 닿았을 때 인터랙션 트리거 발동. 이 때의 오른손 palm의 위치를 저장하고, 타이머를 작동 시킨다든지..
        if (isThrowTriggered == true)
        {
            DetectThrow();
        }
        //debugText.text = isThrowTriggered.ToString();

        // 오른손이 앞쪽 방향으로 움직이면 인터랙션 완성!
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

            // 여기다가 앞으로 폴더 튀어나가는 코드 넣기!!!!!!
            isThrown = true;

            return;
        }
    }
}
