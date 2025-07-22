using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script fires a ray and if the ray hits, it checks whether the object has an Interactive Script on attached. 
/// If so, then it enables the Interactive script. Allowing for interaction.
/// If nothing is hit by the ray, or an Interactive script is not on the object, the current Interactive is disabled, since the 
/// player is no longer pointing/looking at an interactive object.
/// </summary>

namespace Kineractive
{
    public enum Handside // which hand(s) to move to ACTIVE position and return to which Ready position
    {
        Left,
        Right
    }

    [AddComponentMenu("KINERACTIVE/Prerequisite/Kineractive Manager")]
    public class KineractiveManager : Singleton<KineractiveManager>
    {
        [SerializeField] private float interactChecksPerSec = 10;
        [SerializeField] private float rayDistance = 1;
        [SerializeField] private Transform rayOriginTransform = null;
        [SerializeField] private LayerMask layerMask = 0;


        [SerializeField] private float returnToRestMoveSpeed = 7f;
        [SerializeField] private float returnToRestRotateSpeed = 7f;
        [SerializeField] private float idleMoveSpeed = 100f;
        [SerializeField] private float idleRotateSpeed = 100f;

        [SerializeField] private Transform leftHandHelper = null;
        [SerializeField] private Transform rightHandHelper = null;
        [SerializeField] private Transform leftHandRest = null;
        [SerializeField] private Transform rightHandRest = null;

        [SerializeField] private Animator handAnimator = null;
        [SerializeField] private PlayerAnims playerAnims = null;

        //feet
        [SerializeField] private Transform leftFootHelper = null;
        [SerializeField] private Transform rightFootHelper = null;

        [SerializeField] private Text interactionText = null;
        [SerializeField] private Image backgroundImage = null;
        [SerializeField] private RawImage controlsIcon = null;
        [SerializeField] private RawImage crosshair = null;
        [SerializeField] private float defaultCrosshairScale = 1f;


        [SerializeField] private Transform audioSourceObject = null;

        [SerializeField] private PlayerInputs playerInputs = null;

        private InputHandler currentInteractive = null;

        private float leftHandMoveSpeed = 10f;
        private float leftHandRotateSpeed = 10f;
        private Transform leftHandTarget = null;

        private float rightHandMoveSpeed = 10f;
        private float rightHandRotateSpeed = 10f;
        private Transform rightHandTarget = null;

        private float detectionDistance = 0;

        #region Properties

        public Text InteractionText
        {
            get { return interactionText; }
        }

        public Image BackgroundImage
        {
            get { return backgroundImage; }
        }

        public RawImage Crosshair
        {
            get { return crosshair; }
            set { crosshair = value; }
        }

        public Transform AudioSourceObject
        {
            get { return audioSourceObject; }
        }

        public Transform LeftHandHelper
        {
            get { return leftHandHelper; }
        }

        public Transform RightHandHelper
        {
            get { return rightHandHelper; }
        }

        public Transform LeftHandRest
        {
            get { return leftHandRest; }
        }

        public Transform RightHandRest
        {
            get { return rightHandRest; }
        }

        public Animator HandAnimator
        {
            get { return handAnimator; }
        }

        public Transform LeftFootHelper
        {
            get { return leftFootHelper; }
        }

        public Transform RightFootHelper
        {
            get { return rightFootHelper; }
        }

        public float DefaultCrosshairScale
        {
            get { return defaultCrosshairScale; }
        }

        public RawImage ControlsIcon
        {
            get { return controlsIcon; }
        }

        public PlayerInputs PlayerInputs
        {
            get { return playerInputs; }
        }

        public PlayerAnims PlayerAnims
        {
            get { return playerAnims; }
        }

        public float DetectionDistance
        {
            get { return detectionDistance; }
        }

        #endregion


        private void Start()
        {
            InvokeRepeating("CheckForInteractives", 0, 1f / interactChecksPerSec);  // only fire the ray periodically, 60 fps not required
            leftHandTarget = LeftHandHelper;  //set the inital target for left hand
            rightHandTarget = RightHandHelper; //set the initial target for the right hand
            RestLeftArm();
            RestRightArm();
        }


        private void Update()
        {
            LeftHandHelper.position = Vector3.Slerp(LeftHandHelper.position, leftHandTarget.position, Time.deltaTime * leftHandMoveSpeed);
            LeftHandHelper.rotation = Quaternion.Lerp(LeftHandHelper.rotation, leftHandTarget.rotation, Time.deltaTime * leftHandRotateSpeed);

            RightHandHelper.position = Vector3.Slerp(RightHandHelper.position, rightHandTarget.position, Time.deltaTime * rightHandMoveSpeed);
            RightHandHelper.rotation = Quaternion.Lerp(RightHandHelper.rotation, rightHandTarget.rotation, Time.deltaTime * rightHandRotateSpeed);

            if (idleMoveSpeed > 0)
                ChangeIdleSpeed();  //increasing the idle speed to a very high value, allows the hands to follow a steering wheel or flight stick 

        }


        private void CheckForInteractives()
        {
            RaycastHit hit;
            Ray ray = new Ray(rayOriginTransform.position, rayOriginTransform.forward);

            if (Physics.Raycast(ray, out hit, rayDistance, layerMask))   //if the ray hits something then check if it contains an Interactive component/script
            {
                InputHandler potentialInteractive = hit.collider.gameObject.GetComponent<InputHandler>();
                detectionDistance = Vector3.Distance(rayOriginTransform.position, hit.point);

                if (IsInteractive(potentialInteractive))    //if the object has an Interactive Component, then enable it, thus enabling interaction with object
                {
                    EnableInputHandler(potentialInteractive);
                }
                else
                {
                    DisableCurrentInputHandler(); //if object has no interactive component, disable the currently active one (since we're no longer looking at it)
                }
            }
            else
            {
                DisableCurrentInputHandler();  // if ray does not hit anything, then disable current Interactive component
            }
        }

        private bool IsInteractive(InputHandler possibleInteractive)
        {
            if (possibleInteractive == null)
                return false;
            else
                return true;
        }


        private void EnableInputHandler(InputHandler inputHandler)
        {
            if (currentInteractive == inputHandler) //no need to enable it if it's the same interactive object that we are still looking at
                return;

            if (detectionDistance > inputHandler.MaxInteractionRange)
                return;

            DisableCurrentInputHandler();

            currentInteractive = inputHandler;

            currentInteractive.enabled = true;
        }

        private void DisableCurrentInputHandler()
        {
            if (currentInteractive == null)  // don't try to disable an null component
                return;

            RestLeftArm();
            RestRightArm();

            currentInteractive.enabled = false;
            currentInteractive = null;
        }


        private void RestLeftArm()
        {
            SetIKTarget(Handside.Left, leftHandRest, returnToRestMoveSpeed, returnToRestRotateSpeed, "rest left arm");
        }

        private void RestRightArm()
        {
            SetIKTarget(Handside.Right, rightHandRest, returnToRestMoveSpeed, returnToRestRotateSpeed, "rest right arm");
        }

        private void ChangeIdleSpeed()
        {
            if (Vector3.Distance(leftHandHelper.position, leftHandTarget.position) < 0.01f)
            {
                leftHandMoveSpeed = idleMoveSpeed;
                leftHandRotateSpeed = idleRotateSpeed;
            }

            if (Vector3.Distance(rightHandHelper.position, rightHandTarget.position) < 0.01f)
            {
                rightHandMoveSpeed = idleMoveSpeed;
                rightHandRotateSpeed = idleRotateSpeed;
            }
        }

        public void SetIKTarget(Handside handSide, Transform targetTransform, float moveSpeed, float rotateSpeed, string debugName)
        {
            if (handSide == Handside.Left)
            {
                leftHandTarget = targetTransform;
                leftHandMoveSpeed = moveSpeed;
                leftHandRotateSpeed = rotateSpeed;
            }

            if (handSide == Handside.Right)
            {
                rightHandTarget = targetTransform;
                rightHandMoveSpeed = moveSpeed;
                rightHandRotateSpeed = rotateSpeed;
            }
        }

        public void SetCrosshairScale(float sizeMltiplier)
        {
            crosshair.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1) * defaultCrosshairScale;
            crosshair.gameObject.GetComponent<RectTransform>().localScale *= sizeMltiplier;
        }
    }
}