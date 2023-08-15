using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalloon : MonoBehaviour
{
    public GameObject balloonObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(balloonObject, this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
