using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAwake : MonoBehaviour
{
    public bool ShouldDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if(ShouldDestroy)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
