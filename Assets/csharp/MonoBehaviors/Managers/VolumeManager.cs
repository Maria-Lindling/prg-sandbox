using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetVolume(VolumeGroup group, float volume) => audioMixer.SetFloat($"{group}Volume", volume);
    
}
