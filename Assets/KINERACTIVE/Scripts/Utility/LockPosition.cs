using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Lock Position")]
    public class LockPosition : MonoBehaviour
    {
        [SerializeField] protected Transform lockThisTransform;
        protected Vector3 defaultPosition;


        protected virtual void Awake()
        {
            if (lockThisTransform == null)
                lockThisTransform = this.transform;  //if no transform is set in the inspector, use the transform this script is on

            defaultPosition = lockThisTransform.position;
        }


        protected virtual void Update()
        {
            lockThisTransform.position = defaultPosition;
        }
    }
}