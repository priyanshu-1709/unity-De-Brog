using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    public class Repositioner : MonoBehaviour
    {
        [SerializeField] float _lerpMoveSpeed = 1f;

        float _lerpMoveStartTime;
        float _lerpMoveJourneyLength;

        public delegate void RepositioningEventStart();
        public event RepositioningEventStart RepoEventStart;

        public delegate void RepositioningEventEnd();
        public event RepositioningEventEnd RepoEventEnd;

        public void BeginReposition(Vector3 newPosition, Quaternion newRotation)
        {
            StopAllCoroutines();

            if (Mathf.Abs(this.transform.position.x - newPosition.x) > 0.01f &&
                Mathf.Abs(this.transform.position.z - newPosition.z) > 0.01f)
                StartCoroutine(Reposition(newPosition, newRotation));
        }

        IEnumerator Reposition(Vector3 newPosition, Quaternion newRotation)
        {
            if (RepoEventStart != null)
                RepoEventStart.Invoke();

            _lerpMoveStartTime = Time.time;
            _lerpMoveJourneyLength = Vector3.Distance(this.transform.position, newPosition);
            float fractionOfJourney = 0;

            while (fractionOfJourney < 1f)
            {
                float distanceCovered = (Time.time - _lerpMoveStartTime) * _lerpMoveSpeed;
                fractionOfJourney = distanceCovered / _lerpMoveJourneyLength;

                float xPos = Mathf.Lerp(this.transform.position.x, newPosition.x, fractionOfJourney);
                float zPos = Mathf.Lerp(this.transform.position.z, newPosition.z, fractionOfJourney);

                if (Mathf.Abs(this.transform.position.x - newPosition.x) < 0.01f &&
                    Mathf.Abs(this.transform.position.z - newPosition.z) < 0.01f)
                    break;

                this.transform.position = new Vector3(xPos, this.transform.position.y, zPos);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, fractionOfJourney);

                yield return null;
            }


            if (RepoEventEnd != null)
                RepoEventEnd.Invoke();

        }
    }
}