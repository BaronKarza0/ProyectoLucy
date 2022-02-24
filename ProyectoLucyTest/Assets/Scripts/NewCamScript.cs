using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamScript : MonoBehaviour
{
	public bool lockCursor;
	public float mouseSensitivity = 10;
	public float smoothing = .12f;
    float camDistance;
    Vector3 cameraDirection;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public Vector2 pitchMinMax = new Vector2 (-40,85);
    public Vector2 cameraDistanceMinMax = new Vector2(0.5f,5f);
    public Transform target, cam;

    float yaw;
    float pitch;
    // Start is called before the first frame update
    void Start()
    {
    	if(lockCursor)
    	{
    		Cursor.lockState = CursorLockMode.Locked;
    		Cursor.visible = false;
    	}

        cameraDirection = cam.transform.localPosition.normalized;
        camDistance = cameraDistanceMinMax.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
    	yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
    	pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
    	pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

    	currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch,yaw), ref rotationSmoothVelocity, smoothing);
    	transform.eulerAngles = currentRotation;

    	transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f);

        CheckCameraOcclusionAndCollision(cam);
    }

    public void CheckCameraOcclusionAndCollision(Transform cam)
    {
        Vector3 desiredCameraPosition = transform.TransformPoint(cameraDirection * cameraDistanceMinMax.y);
        RaycastHit hit;
        if(Physics.Linecast(transform.position, desiredCameraPosition, out hit))
        {
            camDistance = Mathf.Clamp(hit.distance, cameraDistanceMinMax.x, cameraDistanceMinMax.y);
        }
        else
        {
            camDistance = cameraDistanceMinMax.y;
        }

        cam.localPosition = cameraDirection * camDistance;
    }
}

