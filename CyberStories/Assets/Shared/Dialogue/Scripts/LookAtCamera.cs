using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private new Camera camera;

    // Update is called once per frame
    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        /*var lookPos = Camera.main.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.7f);*/
        Vector3 targetPosition = new Vector3(camera.transform.position.x,
                                        this.transform.position.y,
                                        camera.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
