using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] protected float ShotsPerSecond = 8f;
        [SerializeField] UnityEvent OnFire = null;
        protected float nextFireTime = 0f;
        protected float fireDelay  = 0f;

        protected virtual void Start()
        {
            fireDelay = 1.0f / ShotsPerSecond;
        }
        public virtual void PullTrigger()
        {
            if (nextFireTime < Time.time)
                Fire();
        }

        protected virtual void Fire()
        {
            OnFire.Invoke();
            nextFireTime = Time.time + fireDelay;
        }
    }
}