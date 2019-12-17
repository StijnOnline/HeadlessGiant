using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform rightFootObj = null;
    public Transform leftFootObj = null;
    public float yOffset;
    public Vector3 rotOffset;
    public Vector2 minMaxFootHeight;

    void Start() {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK() {
        Debug.Log("on animator IK");
        if(animator) {

            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {
                Debug.Log("active");


                //Position the model
                if(rightFootObj != null && leftFootObj != null && rightFootObj.gameObject.activeSelf && leftFootObj.gameObject.activeSelf) {

                    Vector3 footAVG = leftFootObj.position - (leftFootObj.position - rightFootObj.position) * 0.5f;
                    float minHeight = Mathf.Min(leftFootObj.position.y, rightFootObj.position.y);
                    footAVG.y = minHeight;
                    transform.position = footAVG  + Vector3.up * yOffset;

                    
                    Quaternion footRot = Quaternion.LookRotation((rightFootObj.forward + leftFootObj.forward), (Vector3.up + Vector3.up) * 0.5f);     
                    //footRot
                    transform.rotation = footRot * Quaternion.Euler(rotOffset);
                    

                }



                    // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null && rightHandObj.gameObject.activeSelf) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }

                if(leftHandObj != null && leftHandObj.gameObject.activeSelf) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }

                if(rightFootObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    Vector3 pos = rightFootObj.position;
                    //pos.y = Mathf.Min(minMaxFootHeight.y, Mathf.Max(minMaxFootHeight.x, pos.y));
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, pos);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
                }

                if(leftFootObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    Vector3 pos = leftFootObj.position;
                    //pos.y = Mathf.Min(minMaxFootHeight.y, Mathf.Max(minMaxFootHeight.x, pos.y));
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, pos);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}