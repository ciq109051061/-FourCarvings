using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
