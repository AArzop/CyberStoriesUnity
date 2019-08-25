using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        /*var lookPos = Camera.main.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.7f);*/
        Vector3 targetPostition = new Vector3(Camera.main.transform.position.x,
                                        this.transform.position.y,
                                        Camera.main.transform.position.z);
        this.transform.LookAt(targetPostition);
    }
}
