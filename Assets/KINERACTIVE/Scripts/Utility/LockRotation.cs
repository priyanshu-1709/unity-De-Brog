using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Lock Rotation")]
    public class LockRotation : MonoBehaviour
    {
        [SerializeField] protected Transform lockThisTransform;
        protected Quaternion defaultRotation;


        protected virtual void Awake()
        {
            if (lockThisTransform == null)
                lockThisTransform = this.transform;  //if no transform is set in the inspector, use the transform this script is on

            defaultRotation = lockThisTransform.rotation;
        }


        protected virtual void Update()
        {
            lockThisTransform.rotation = defaultRotation;
        }
    }
}