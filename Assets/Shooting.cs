using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting: MonoBehaviour
{
    public GameObject Decal;
    public float frequency = .5f;
    public float hitForce = 5.0f;
    public float range = 100.0f;
    public Transform barrel;
    private float ctime;
    public float damage = 1;
    private LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ctime < frequency)
        {
            ctime += Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {

                StartCoroutine(ShotEffect());
                laserLine.SetPosition(0, barrel.position);
                Vector3 rayOrigin = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, range))
                {
                    laserLine.SetPosition(1, hit.point);
                    GameObject clone = Instantiate(Decal, hit.point - transform.TransformDirection(new Vector3(0, 0, .05f)), Quaternion.FromToRotation(Vector3.forward, hit.normal));
                    clone.transform.SetParent(hit.transform);
                    Destroy(clone, 6);

                }

                else
                {
                    laserLine.SetPosition(1, rayOrigin + (transform.forward * range));
                }
                ctime = 0;
            }
        }
    }
    private IEnumerator ShotEffect()
    {

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return new WaitForSeconds(0.07f);

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
}
