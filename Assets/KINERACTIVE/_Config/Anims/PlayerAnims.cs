using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [CreateAssetMenu(menuName = "Kineractive/PlayerAnims")]
    public class PlayerAnims : ScriptableObject
    {
        [SerializeField] protected string[] anims;

        public string[] Anims
        {
            get { return anims; }
        }
    }
}