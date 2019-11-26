using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float tilt;
    public float speed;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn; //shotSpawn.transform.position

    public float fireRate;
    private float nextFire;

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            var audio = GetComponent<AudioSource>();

           // if (!audio.isPlaying)
           // {
                GetComponent<AudioSource>().Play();
           // }
        }      
    }

    private void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //get movement entered
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Rigidbody ship = GetComponent<Rigidbody>();
        //move ship
        ship.velocity = movement * speed;

        //clamp the player inside the screen
        ship.position = new Vector3
            (
            Mathf.Clamp(ship.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(ship.position.z, boundary.zMin, boundary.zMax)
            );

        ship.rotation = Quaternion.Euler(0.0f, 0.0f, ship.velocity.x * -tilt);

    }
}