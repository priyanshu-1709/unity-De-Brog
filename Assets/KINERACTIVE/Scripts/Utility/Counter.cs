using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] protected int countToReach = 0;
        [SerializeField] protected int startingCount = 0;
        [SerializeField] protected UnityEvent OnCountReached = null;

        [Header("Debug")]
        [SerializeField] protected int currentCount = 0;

        protected virtual void Start()
        {
            currentCount = startingCount;
        }
        public virtual void AddCount(int addCount)
        {
            currentCount += addCount;
            EvaluteCount();
        }

        public virtual void RemoveCount(int removeCount)
        {
            currentCount -= removeCount;
            EvaluteCount();
        }

        public virtual void SetCount(int setCount)
        {
            currentCount = setCount;
            EvaluteCount();
        }

        protected virtual void EvaluteCount()
        {
            if (currentCount == countToReach)
                OnCountReached.Invoke();
        }

    }
}