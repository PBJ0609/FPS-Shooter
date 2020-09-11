using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public float rotSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, camForward, rotSpeed * Time.deltaTime);


    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(1, 1, 1, 0.5f, 0.5f);
        Ray lookAtRay = new Ray(transform.position, cam.transform.forward);
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
