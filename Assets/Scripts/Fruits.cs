using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruiterigidbody;
    private Collider fruitecolider;

    private ParticleSystem juiceeffect;

    private void Awake()
    {
        fruiterigidbody = GetComponent<Rigidbody>();
        fruitecolider = GetComponent<Collider>();
        juiceeffect = GetComponentInChildren<ParticleSystem>();
        
    }

    private void slice(Vector3 direction, Vector3 position , float force )
    {

        FindObjectOfType<GameManager>().IncreaseScore();
        whole.SetActive(false);
        sliced.SetActive(true);
        fruitecolider.enabled = false;
        juiceeffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice  in  slices)
        {
            slice.velocity = fruiterigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);

        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            slice(blade.direction,blade.transform.position,blade.sliceForce);

        }
    }


}
