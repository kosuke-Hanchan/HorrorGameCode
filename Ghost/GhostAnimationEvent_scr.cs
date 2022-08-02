using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationEvent_scr : MonoBehaviour
{
    [SerializeField] GameObject AudioSourceObj;
    [SerializeField] AudioClip[] clips;
    [SerializeField] float pitchRange = 0.1f;
    protected AudioSource source;


    private void Awake()
    {
        source = AudioSourceObj.GetComponents<AudioSource>()[0];
    }

    public void PlayFootstepSE()
    {
        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
