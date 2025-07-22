using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Controls/Mouse Look Seated")]
    public class MouseLookSeated : MonoBehaviour
    {
        [SerializeField] private float currentMouseSpeed = 1f;
        [SerializeField] private float minMouseSpeed = 0.1f;
        [SerializeField] private float maxMouseSpeed = 1.5f;

        private float xAxisClamp = 0f;
        private float yAxisClamp = 0f;


        private void Update()
        {
            RotateCamera();
        }

        private void RotateCamera()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float rotAmountX = mouseX * currentMouseSpeed;
            float rotAmountY = mouseY * currentMouseSpeed;

            xAxisClamp -= rotAmountY;
            yAxisClamp += rotAmountX;



            Vector3 targetRot = transform.localRotation.eulerAngles;
            targetRot.x -= rotAmountY;
            targetRot.z = 0; //prevents cam flipping at exterme angles
            targetRot.y += rotAmountX;


            if (xAxisClamp > 90)
            {
                xAxisClamp = 90;
                targetRot.x = 90;
            }
            else if (yAxisClamp < -90)
            {
                yAxisClamp = -90;
                targetRot.y = 270;
            }

            if (yAxisClamp > 90)
            {
                yAxisClamp = 90;
                targetRot.y = 90;
            }
            else if (xAxisClamp < -90)
            {
                xAxisClamp = -90;
                targetRot.y = 270;
            }

            transform.localRotation = Quaternion.Euler(targetRot);
        }

        public void SetMouseSpeed(float newMouseSpeed)
        {
            currentMouseSpeed = newMouseSpeed;
        }

        public void SetMouseSpeedBasedOnZoomChanage(float zoomMin, float zoomMax, float currentZoom)
        {
            float newMouseSpeed = MathfExtended.Scale(zoomMin, zoomMax, minMouseSpeed, maxMouseSpeed, currentZoom);

            currentMouseSpeed = newMouseSpeed;
        }

        public void DecreaseMouseSpeed(float decreaseAmount)
        {
            currentMouseSpeed -= decreaseAmount;
        }

        public void IncreaseMouseSpeed(float increaseAmount)
        {
            currentMouseSpeed += increaseAmount;
        }


    }
}