using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolster : MonoBehaviour
{
    public void DoHolster()
    {
        GetComponent<Animator>().SetBool("doHolster", true);
    }


    public void UnDoHolster()
    {
        GetComponent<Animator>().SetBool("doHolster", false);

    }

    
}
