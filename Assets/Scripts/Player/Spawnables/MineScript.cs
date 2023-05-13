using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private AudioSource exSoundEffect;
    public AudioClip explosionSound;
    public ParticleSystem exParticle;

    // Start is called before the first frame update
    void Start()
    {
        exSoundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tactical Nuke!!!! INCOMMING!!!!");
            exParticle.Play();
            exSoundEffect.PlayOneShot(explosionSound, 1.0f);
            Destroy(gameObject, 1.0f);
        }
    }
}
