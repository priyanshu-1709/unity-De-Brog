using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Rotation Swapper")]
    public class RotationSwapper : MonoBehaviour
    {
        [SerializeField] Transform transformA = null;
        [SerializeField] Transform transformB = null;

        public void Swap()
        {
            Quaternion rotationOfA = transformA.rotation;
            Quaternion rotationOfB = transformB.rotation;

            transformA.rotation = rotationOfB;
            transformB.rotation = rotationOfA;

        }
    }
}