using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 10;
    public int magazineSize = 30;
    public float reloadTime = 1;
    public Transform bulletSpawn;
    public float damage;

    //audio

    public AudioSource audioSrc;
    public AudioClip blastSound;
    public AudioClip reloadSound; 

    private bool firing = false;
    private bool reloading = false;
    private int bulletsRemaining;
    private float timer = 0; 


	// Use this for initialization
	void Start () {

        bulletsRemaining = magazineSize;

        if(!GetComponent<AudioSource>())
        {
            Debug.Log("No Audio Source");
            gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSrc = GetComponent<AudioSource>();
        }
		
	}
	 
	// Update is called once per frame
	void Update ()
    {
       
        if (firing)
        {
            if (timer <= 0 && !reloading)
            {
                if (bulletsRemaining > 0)
                {
                    LaunchProjectile();
                    timer = 1 / fireRate;
                }
                else
                {
                    Reload();
                }
            }
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0; 
        }

        
	}



   

    public void fireControl(bool toggle)
    {
        firing = toggle; 
    }

    public virtual void LaunchProjectile()
    {
        bulletsRemaining--;
        audioSrc.PlayOneShot(blastSound);
    }

    void Reload()
    {
        reloading = true;
        StartCoroutine("ReloadCoroutine");
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = magazineSize;
        reloading = false; 
    }


}
