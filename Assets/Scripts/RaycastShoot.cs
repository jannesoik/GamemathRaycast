using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastShoot : MonoBehaviour
{
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public Transform gunEnd;
    public int score=0;
    public Text scoreText;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            // Convert a position relative to the camera to world space. By defining .5 on X & Y axes, we get the center of viewpor.
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                print(hit.collider.tag);


                if (hit.collider.tag == "Target")
                {
                    score++;
                    print(score);
                    scoreText.text = score.ToString();
                }
                if (hit.collider.tag=="MissSurface")
                {
                    score--;
                    print(score);
                    scoreText.text = score.ToString();

                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
