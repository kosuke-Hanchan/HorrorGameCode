using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationEvent_scr : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float pitchRange = 0.1f;
    protected AudioSource source;


    private void Awake()
    {
        source = GetComponents<AudioSource>()[0];
    }

    public void PlayFootstepSE()
    {
        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
