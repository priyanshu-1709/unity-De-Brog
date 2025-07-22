using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    public class EnableOnStart : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStartEvents = null;
        public virtual void Start()
        {
            OnStartEvents.Invoke();
        }
    }
}