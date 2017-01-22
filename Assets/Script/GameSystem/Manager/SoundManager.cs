using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip[] BGMSounds;
    public AudioClip[] EffectSounds;

    public bool isSfxMute = false;
    [Range(0.0f, 1.0f)]
    public float sfxVolume = 1.0f;

    public Vector3 SoundPlayPosition;

    float DefaultMinDistance = 10.0f;
    float DefaultMaxDistance = 30.0f;
    public void SimplePlayBGM(int Index)
    {
        PlaySfx(BGMSounds[Index]);
    }
    public void SimplePlayEffect(int Index)
    {
        PlaySfx(EffectSounds[Index]);
    }
    public void PlaySfx(AudioClip sfx, Vector3 _pos, float _MinDistance, float _MaxDistance, float _Volume)
    {
        if (isSfxMute) return;

        GameObject SoundPlayObject = new GameObject("Sfx_" + sfx.name);
        SoundPlayObject.transform.position = _pos;

        AudioSource _AudioSource = SoundPlayObject.AddComponent<AudioSource>();
        _AudioSource.clip = sfx;
        _AudioSource.minDistance = _MinDistance;
        _AudioSource.maxDistance = _MaxDistance;
        _AudioSource.volume = sfxVolume;

        _AudioSource.Play();

        Destroy(SoundPlayObject, sfx.length);
    }
    public void PlaySfx(AudioClip sfx)
    {
        PlaySfx(sfx, SoundPlayPosition, DefaultMinDistance, DefaultMaxDistance, sfxVolume);
    }
    public void PlaySfx(AudioClip sfx, float _MinDistance, float _MaxDistance)
    {
        PlaySfx(sfx, SoundPlayPosition, _MinDistance, _MaxDistance, sfxVolume);
    }
    public void PlaySfx(AudioClip sfx, Vector3 _pos)
    {
        PlaySfx(sfx, _pos, DefaultMinDistance, DefaultMaxDistance, sfxVolume);
    }
    public void PlaySfx(AudioClip sfx, Vector3 _pos, float _MinDistance, float _MaxDistance)
    {
        PlaySfx(sfx, _pos, _MinDistance, _MaxDistance, sfxVolume);
    }
    public void PlaySfx(AudioClip sfx, Vector3 _pos, float sfxVolume)
    {
        PlaySfx(sfx, _pos, DefaultMinDistance, DefaultMaxDistance, sfxVolume);
    }
}
