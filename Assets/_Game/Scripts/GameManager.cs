using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager ins;
    public static GameManager Ins => ins;

    private void Awake()
    {
        ins = this;
    }
}
