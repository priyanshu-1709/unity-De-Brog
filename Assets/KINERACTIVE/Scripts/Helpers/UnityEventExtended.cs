using System;
using UnityEngine.Events;

namespace Kineractive
{
    [Serializable]
    public class UnityEventInt : UnityEvent<int>
    {

    }

    [Serializable]
    public class UnityEventFloat : UnityEvent<float>
    {

    }

    [Serializable]
    public class UnityEventThreeFloats : UnityEvent<float, float, float>
    {

    }

    [Serializable]
    public class UnityEventVector3Quaternion: UnityEvent<UnityEngine.Vector3, UnityEngine.Quaternion>
    {

    }

}