using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/RotationSway")]
    public class RotationSway : MonoBehaviour
    {
        Vector3 startRotation;
        [SerializeField] Transform rotateTransform = null;
        [SerializeField] Vector3 rotationSpeed = Vector3.zero;
        [SerializeField] Vector3 rotationOffset = Vector3.zero;


        void Start()
        {
            if (rotateTransform == null)
                rotateTransform = this.transform;

            startRotation = rotateTransform.localEulerAngles;
        }


        void Update()
        {
            float rotX = startRotation.x + Mathf.Sin(Time.time * rotationSpeed.x) * rotationOffset.x;
            float rotY = startRotation.y + Mathf.Sin(Time.time * rotationSpeed.y) * rotationOffset.y;
            float rotZ = startRotation.z + Mathf.Sin(Time.time * rotationSpeed.z) * rotationOffset.z;
            rotateTransform.localRotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));
        }
    }
}