using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace broccoli.Manager.Audio
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Sound Collections")]
        public Sound[] musicSounds;
        public Sound[] sfxSounds;

        private Dictionary<string, Sound> musicDict;
        private Dictionary<string, Sound> sfxDict;

        private void Awake()
        {
            musicDict = new Dictionary<string, Sound>();
            sfxDict = new Dictionary<string, Sound>();

            foreach (var sound in musicSounds)
            {
                if (!musicDict.ContainsKey(sound.name))
                    musicDict.Add(sound.name, sound);
            }
            foreach (var sound in sfxSounds)
            {
                if (!sfxDict.ContainsKey(sound.name))
                    sfxDict.Add(sound.name, sound);
            }
        }

        #region Music Methods

        public void PlayMusic(string name, bool loop = false)
        {
            if (!musicDict.TryGetValue(name, out Sound s) || s.clip == null)
            {
                Debug.LogWarning($"Music sound not found or clip missing: {name}");
                return;
            }

            AudioSource src = CreateAudioSource();
            src.clip = s.clip;
            src.loop = loop;
            src.volume = s.volume;
            src.mute = s.mute;
            src.Play();

            if (!loop)
                StartCoroutine(DelayCallback(src.clip.length, () => Destroy(src.gameObject)));
        }

        public void StopMusic(string name)
        {
            // This assumes only one instance of a music sound is playing at a time.
            // For multiple, you'd need to track all created AudioSources.
            if (!musicDict.TryGetValue(name, out Sound s) || s.clip == null)
            {
                Debug.LogWarning($"Music sound not found or clip missing: {name}");
                return;
            }

            // Find all AudioSources playing this clip
            foreach (var src in FindObjectsOfType<AudioSource>())
            {
                if (src.clip == s.clip && src.isPlaying)
                {
                    src.Stop();
                    Destroy(src.gameObject);
                }
            }
        }

        public void MuteMusic(bool value)
        {
            foreach (var src in FindObjectsOfType<AudioSource>())
            {
                if (Array.Exists(musicSounds, s => s.clip == src.clip))
                    src.mute = value;
            }
        }

        #endregion

        #region SFX Methods

        public void PlaySfx(string name, bool loop = false)
        {
            if (!sfxDict.TryGetValue(name, out Sound s) || s.clip == null)
            {
                Debug.LogWarning($"SFX sound not found or clip missing: {name}");
                return;
            }

            AudioSource src = CreateAudioSource();
            src.clip = s.clip;
            src.loop = loop;
            src.volume = s.volume;
            src.mute = s.mute;
            src.Play();

            if (!loop)
                StartCoroutine(DelayCallback(src.clip.length, () => Destroy(src.gameObject)));
        }

        public void StopSfx(string name)
        {
            if (!sfxDict.TryGetValue(name, out Sound s) || s.clip == null)
            {
                Debug.LogWarning($"SFX sound not found or clip missing: {name}");
                return;
            }

            foreach (var src in FindObjectsOfType<AudioSource>())
            {
                if (src.clip == s.clip && src.isPlaying)
                {
                    src.Stop();
                    Destroy(src.gameObject);
                }
            }
        }

        public void MuteSfx(bool value)
        {
            foreach (var src in FindObjectsOfType<AudioSource>())
            {
                if (Array.Exists(sfxSounds, s => s.clip == src.clip))
                    src.mute = value;
            }
        }

        #endregion

        #region Global Controls

        public void PauseAll()
        {
            foreach (var src in FindObjectsOfType<AudioSource>())
                src.Pause();
        }

        public void ResumeAll()
        {
            foreach (var src in FindObjectsOfType<AudioSource>())
                src.UnPause();
        }

        #endregion

        #region Helpers

        private AudioSource CreateAudioSource()
        {
            GameObject go = new GameObject("AudioSource_" + Guid.NewGuid());
            go.transform.SetParent(this.transform);
            return go.AddComponent<AudioSource>();
        }

        private IEnumerator DelayCallback(float delay, UnityAction action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        #endregion
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        public bool mute = false;
    }
}