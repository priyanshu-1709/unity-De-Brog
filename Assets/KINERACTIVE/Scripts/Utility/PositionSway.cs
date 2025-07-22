using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Position Sway")]
    public class PositionSway : MonoBehaviour
    {
        [SerializeField] Transform swayTransform = null;
        [SerializeField] Vector3 moveSpeed = Vector3.zero;
        [SerializeField] Vector3 moveOffset = Vector3.zero;


        Vector3 startPosition;

        // Start is called before the first frame update
        void Start()
        {
            if (swayTransform == null)
                swayTransform = this.transform;

            startPosition = swayTransform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {

            float posX = startPosition.x + Mathf.Sin(Time.time * moveSpeed.x) * moveOffset.x;
            float posY = startPosition.y + Mathf.Sin(Time.time * moveSpeed.y) * moveOffset.y;
            float posZ = startPosition.z + Mathf.Sin(Time.time * moveSpeed.z) * moveOffset.z;

            swayTransform.localPosition = new Vector3(posX, posY, posZ);
        }
    }
}