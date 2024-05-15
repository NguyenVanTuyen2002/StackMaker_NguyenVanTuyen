using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{



    private static LeverManager ins;
    public static LeverManager Ins => ins;

    private void Awake()
    {
        ins = this;
    }
}
