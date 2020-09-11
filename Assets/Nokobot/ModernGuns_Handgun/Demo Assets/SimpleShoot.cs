using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform barrelLocation;
    public Transform casingExitLocation;
    public GameObject Decal;
    public float fireSpeed = 0.5f;
    public bool semiAuto;
    public float shotSpeed= 100f;
    Animator m_animator;
    public int magSize = 30;
    public int currentMag;
    private bool canShoot = true;
    private float reloadSpeed = 1f;
    private float currentTime;
    public Image foreground;
    public Text ammoCount;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
        Cursor.visible = false;
        m_animator = GetComponent<Animator>();

        currentMag = magSize;

        ammoCount.text = currentMag.ToString() + "/" + magSize.ToString();
        foreground.fillAmount = (float)currentMag / (float)magSize;
    }

    void Update()
    {
        if (semiAuto == false)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= fireSpeed)
            {
                if (Input.GetButton("Fire1") && canShoot)
                {
                    currentMag--;
                    ammoCount.text = currentMag.ToString() + "/" + magSize.ToString();
                    foreground.fillAmount = (float)currentMag / (float)magSize;
                    if (currentMag < 0)
                    {
                        currentMag = 0;
                        ammoCount.text = currentMag.ToString() + "/" + magSize.ToString();
                        foreground.fillAmount = (float)currentMag / (float)magSize;
                        return;
                    }
                    GetComponent<Animator>().SetTrigger("Fire");
                    currentTime = 0;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    m_animator.SetTrigger("Shoot");
                }
            }
        }

        if (semiAuto == true)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= fireSpeed)
            {
                if (Input.GetButtonDown("Fire1") && canShoot)
                {
                    currentMag--;
                    ammoCount.text = currentMag.ToString() + "/" + magSize.ToString();
                    foreground.fillAmount = (float)currentMag / (float)magSize;
                    if (currentMag < 0)
                    {
                        currentMag = 0;
                        ammoCount.text = currentMag.ToString() + "/" + magSize.ToString();
                        foreground.fillAmount = (float)currentMag / (float)magSize;
                        return;

                    }
                    GetComponent<Animator>().SetTrigger("Fire");
                    currentTime = 0;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    m_animator.SetTrigger("Shoot");
                }
            }
        }

        if (Input.GetButton("Fire2")) {
            {
                GetComponent<Animator>().SetTrigger("ADS");
            }    
        }

        if (Input.GetButtonUp("Fire2"))
        {
            GetComponent<Animator>().SetTrigger("HP");

        }

        if (Input.GetKeyDown("r"))
        {
            GetComponent<Animator>().SetTrigger("Reload");
            StartCoroutine(reload());
        }
    }

    void Shoot()
    {
        //  GameObject bullet;
        //  bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        GameObject tempFlash;
       Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotSpeed);
       tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

        // Destroy(tempFlash, 0.5f);
        //  Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation).GetComponent<Rigidbody>().AddForce(casingExitLocation.right * 100f);
       
        //GameObject clone = Instantiate(Decal, hit.point - transform.TransformDirection(new Vector3(0, 0, .05f)), Quaternion.FromToRotation(Vector3.forward, hit.normal));
        //clone.transform.SetParent(hit.transform);
        //Destroy(clone, 6);
    }

    void CasingRelease()
    {
         GameObject casing;
        casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.3f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
    }

    IEnumerator reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadSpeed);
        currentMag = magSize;
        canShoot = true;
        ammoCount.text = currentMag.ToString() + "/" + magSize.ToString();
        foreground.fillAmount = (float)currentMag / (float)magSize;
    }


}
