using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.MediaFoundation;
using System.IO;


namespace GALAXIAN
{
    public class SoundManager
    {
        public enum SoundType
        {
            Shot,
            Music,
            Enemy_die
            
        }
        private bool isSoundEnabled = false;
        private Dictionary<SoundType, WaveOutEvent> waveOutEvents;
        private Dictionary<SoundType, Mp3FileReader> soundDictionary;

        public bool IsSoundEnabled
        {
            get { return isSoundEnabled; }
            set { isSoundEnabled = value; }
        }

        public SoundManager(bool onOFF)
        {
            waveOutEvents = new Dictionary<SoundType, WaveOutEvent>();
            soundDictionary = new Dictionary<SoundType, Mp3FileReader>();
            isSoundEnabled = onOFF;
            LoadSounds(); // загрузка всех звуков
        }

        private void LoadSounds()
        {
            soundDictionary.Add(SoundType.Shot, new Mp3FileReader(new MemoryStream(Properties.Resources.piu)));
            soundDictionary.Add(SoundType.Music, new Mp3FileReader(new MemoryStream(Properties.Resources.Musick)));
            soundDictionary.Add(SoundType.Enemy_die, new Mp3FileReader(new MemoryStream(Properties.Resources.enemy_die)));
            foreach (var soundType in soundDictionary.Keys)
            {
                waveOutEvents.Add(soundType, new WaveOutEvent());
            }
        }

        public void PlaySound(SoundType soundType)
        {
            if (IsSoundEnabled && soundDictionary.ContainsKey(soundType))
            {
                var waveOutEvent = waveOutEvents[soundType];

                waveOutEvent.Stop(); // стопить предыдущие воспроизведение
                waveOutEvent.Init(soundDictionary[soundType]);
                soundDictionary[soundType].Position = 0;
                waveOutEvent.Play();
            }
        }

        public void SetVolume(double volume)
        {
            if (volume < 0)
                volume = 0;
            else if (volume > 1)
                volume = 1;
            foreach (var ev in waveOutEvents.Values)
            {
                ev.Volume = (float)volume;
            }
        }
    }

}

