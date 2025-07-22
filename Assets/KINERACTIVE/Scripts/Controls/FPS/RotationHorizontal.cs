using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    public class RotationHorizontal : MonoBehaviour
    {
        [SerializeField] private float Sensitivity = 150f;
        Repositioner _repositioner = null;

        bool overrideInput = false;

        private void Awake()
        {
            _repositioner = GetComponent<Repositioner>();
        }

        void Update()
        {
            if (!overrideInput)
                InputBasedRotation();
        }

        void InputBasedRotation()
        {
            float horizontalInput = Input.GetAxis("Mouse X");

            float rotAmountHorizontal = horizontalInput * Sensitivity * Time.deltaTime;

            Vector3 targetRot = transform.localRotation.eulerAngles;

            targetRot.z = 0; //prevents cam flipping at exterme angles
            targetRot.y += rotAmountHorizontal;

            transform.localRotation = Quaternion.Euler(targetRot);
        }

        void EnableInput()
        {
            overrideInput = false;
        }
        void DisableInput()
        {
            overrideInput = true;
        }

        void OnEnable()
        {
            _repositioner.RepoEventStart += DisableInput;
            _repositioner.RepoEventEnd += EnableInput;
        }

        void OnDisable()
        {
            _repositioner.RepoEventStart -= DisableInput;
            _repositioner.RepoEventEnd -= EnableInput;
        }


    }
}