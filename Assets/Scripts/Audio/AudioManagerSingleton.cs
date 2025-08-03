using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSingleton : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioSourcePrefab;
    
    [Header("Audio Clips")]
    public List<StringAudioClip> audioClipsList;
    
    private Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>();
    
    private static AudioManagerSingleton _instance;
    
    public static AudioManagerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<AudioManagerSingleton>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        audioClipsDic = GetDictionary();
    }

    public Dictionary<string, AudioClip> GetDictionary()
    {
        Dictionary<string, AudioClip> dictionary = new Dictionary<string, AudioClip>();
        foreach (var dataPair in audioClipsList)
        {
            dictionary[dataPair.key] = dataPair.clip;
        }
        
        return dictionary;
    }
    
    public void ButtonFunctionPlaySound(string audioName)
    {
        PlaySound(audioName, this.transform);
    }
    
    public void PlaySound(string audioName, Transform spawnTransform, float volume = 1f)
    {
        if (!audioClipsDic.ContainsKey(audioName)) return;
        
        AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClipsDic[audioName];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioClipsDic[audioName].length;
        Destroy(audioSource.gameObject, clipLength);
    }
}