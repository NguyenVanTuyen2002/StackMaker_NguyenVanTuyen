using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabListMap = new List<GameObject>();



    private static GameManager ins;
    public static GameManager Ins => ins;

    private void Awake()
    {
        ins = this;
    }


}
