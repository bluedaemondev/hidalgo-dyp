﻿using Audio;
using UnityEngine;

namespace Audio.Triggers
{
    public class SoundOnTrigger : MonoBehaviour
    {
        public AudioClip clipToPlay;

        [Header("suena al entrar")]
        public bool playOnEnter = true;
        [Header("suena al salir")]
        public bool playOnExit;

        [Header("layers de objeto con las que interactua")]
        public LayerMask interactWith;

        public MixerChannel channel = MixerChannel.Sfx;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!playOnEnter)
                return;

            PlayAudioOnChannel();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!playOnExit)
                return;

            PlayAudioOnChannel();
        }

        void PlayAudioOnChannel()
        {
            switch (channel)
            {
                case MixerChannel.Sfx:
                    SoundManager.instance.PlayEffect(clipToPlay);
                    break;
                case MixerChannel.Music:
                    SoundManager.instance.PlayMusic(clipToPlay);
                    break;
                case MixerChannel.Ambient:
                    SoundManager.instance.PlayAmbient(clipToPlay);
                    break;
            }
        }
    }
}