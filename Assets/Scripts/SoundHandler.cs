using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour
{
    const string PLANTED_SOUND_NAME = "Siren-SoundBible.com-1094437108";
    const string EXPLODED_SOUND_NAME = "Mortar Blast-SoundBible.com-720286088";
    const string DEFUSED_SOUND_NAME = "Ta Da-SoundBible.com-1884170640";

    private AudioSource m_plantedAudio, m_explodedAudio, m_defusedAudio;
    private AudioSource m_lastPlayed;
    private static SoundHandler instance;

    public static void PlayPlantedSound()
    {
        PlaySound(ref instance.m_plantedAudio);
    }

    public static void PlayDefusedSound()
    {
        PlaySound(ref instance.m_defusedAudio);
    }

    public static void HandleTimerExpired()
    {
        switch(GameData.gameState)
        {
            case GameData.GameState.Planted:
                //Terrorists won
                PlaySound(ref instance.m_explodedAudio);
                break;
            case GameData.GameState.Unplanted:
                //CT won
                PlaySound(ref instance.m_defusedAudio);
                break;
        }
    }

    void Awake()
    {
        Debug.Assert(instance == null, "Multiple instances of SoundHandler found.");

        instance = this;

        AddAudioSource(ref m_plantedAudio, PLANTED_SOUND_NAME);
        AddAudioSource(ref m_explodedAudio, EXPLODED_SOUND_NAME);
        AddAudioSource(ref m_defusedAudio, DEFUSED_SOUND_NAME);
    }
    
    private void AddAudioSource(ref AudioSource component, string resourcePath)
    {
        component = Camera.main.gameObject.AddComponent<AudioSource>();
        component.clip = Resources.Load<AudioClip>(resourcePath);
    }

    private static void PlaySound(ref AudioSource audioSource)
    {
        if (instance.m_lastPlayed != null && instance.m_lastPlayed.isPlaying)
            instance.m_lastPlayed.Stop();
        audioSource.Play();
        instance.m_lastPlayed = audioSource;
    }
}
