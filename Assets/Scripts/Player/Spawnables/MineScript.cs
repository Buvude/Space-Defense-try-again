using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField] private AudioSource exSoundEffect;
    [SerializeField] ParticleSystem exParticle = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            exSoundEffect.Play();
            exParticle.Play();
            Destroy(gameObject);
        }
    }
}
