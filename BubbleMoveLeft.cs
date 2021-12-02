using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    int lespeed = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * lespeed * Time.deltaTime);
    }
}
