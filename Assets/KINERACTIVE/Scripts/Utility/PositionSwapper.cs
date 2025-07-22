using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Position Swapper")]
    public class PositionSwapper : MonoBehaviour
    {
        [SerializeField] Transform transformA = null;
        [SerializeField] Transform transformB = null;

        public void Swap()
        {
            Vector3 positionOfA = transformA.position;
            Vector3 positionOfB = transformB.position;

            transformA.position = positionOfB;
            transformB.position = positionOfA;

        }
    }
}