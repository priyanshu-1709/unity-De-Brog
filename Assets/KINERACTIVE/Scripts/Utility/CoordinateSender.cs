using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    public class CoordinateSender : MonoBehaviour
    {
        [SerializeField] protected UnityEventVector3Quaternion Send_Coordinates;

        public void SendCoordinates()
        {
            Send_Coordinates.Invoke(this.transform.position, this.transform.rotation);
        }


    }
}