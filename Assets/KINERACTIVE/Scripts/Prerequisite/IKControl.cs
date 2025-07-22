using UnityEngine;
using System;
using System.Collections;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Prerequisite/IK Control")]
    [RequireComponent(typeof(Animator))]

    public class IKControl : MonoBehaviour
    {

        private Transform LeftHandObj;
        private Transform RightHandObj;
        private Transform LeftFootObj;
        private Transform RightFootObj;
        private Animator animator;
        private bool ikActive = true;


        public bool IKactive
        {
            get; set;
        }


        private void Start()
        {
            animator = GetComponent<Animator>();
            LeftHandObj = KineractiveManager.Instance.LeftHandHelper;
            RightHandObj = KineractiveManager.Instance.RightHandHelper;
            LeftFootObj = KineractiveManager.Instance.LeftFootHelper;
            RightFootObj = KineractiveManager.Instance.RightFootHelper;
        }

        //a callback for calculating IK
        void OnAnimatorIK()
        {
            if (animator)
            {
                //if the IK is active, set the position and rotation directly to the goal. 
                if (ikActive)
                {
                    // HANDS
                    if (LeftHandObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandObj.rotation);
                    }

                    if (RightHandObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandObj.rotation);
                    }

                    // FEET
                    if (LeftFootObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootObj.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, LeftFootObj.rotation);
                    }

                    if (RightFootObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, RightFootObj.rotation);
                    }




                }

                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    //hands
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                    //feet

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
                }
            }
        }
    }

}