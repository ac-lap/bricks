using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Diagnostics;
namespace PhoneApp2
{
    public class SoundsInitialiser
    {
        private static bool initialised = false;
        public SoundsInitialiser()
        {

        }
        public static void initialiseSounds()
        {
            if (!initialised)
            {
                loadSettings();
                new BackgroundMusic();
                new BrickClick();
                new MineBlast();
                new GameMusic();
                new BlockCapture();
                initialised = true;
            }
        }
        static void loadSettings()
        {
            if (Page1.save_g.Contains(cons.term[12]))
                cons.sound = (bool)Page1.save_g[cons.term[12]];

            if (Page1.save_g.Contains(cons.term[11]))
                cons.music = (bool)Page1.save_g[cons.term[11]];
            
        }
        public static void stopMusic()
        {
            BackgroundMusic.stop();
        }
        public static void startMusic()
        {
            BackgroundMusic.play();
        }
    }
    public class BackgroundMusic
    {
        static Stream stream;
        static SoundEffect effect;
        static SoundEffectInstance soundInstance;
        static bool isRunning;
        static bool isPaused;

        public BackgroundMusic()
        {
            initialise();
        }
        public void initialise()
        {
            stream = TitleContainer.OpenStream("sound_clips/background_music.wav");
            effect = SoundEffect.FromStream(stream);
            soundInstance = effect.CreateInstance();
            soundInstance.IsLooped = true;
            isRunning = false;
            isPaused = false;
        }
        public static void play()
        {
            if (isRunning)
                return;
            if (isPaused)
                soundInstance.Resume();
            else
                soundInstance.Play();
            isRunning = true;
            isPaused = false;
        }
        public static void stop()
        {
            isRunning = false;
            isPaused = false;
            soundInstance.Stop();
        }
        public static void pause()
        {
            if (isPaused)
                return;
            isRunning = false;
            isPaused = true;
            soundInstance.Pause();
        }
    }
    class BrickClick
    {
        static Stream stream;
        static SoundEffect effect;
        static SoundEffectInstance soundInstance;
        public BrickClick()
        {
            initialise();
        }
        public void initialise()
        {
            stream = TitleContainer.OpenStream("sound_clips/brick_click.wav");
            effect = SoundEffect.FromStream(stream);
            soundInstance = effect.CreateInstance();
        }
        public static void play()
        {
            soundInstance.Play();
        }
        public static void stop()
        {
            soundInstance.Stop();
        }
    }
    class MineBlast
    {
        static Stream stream;
        static SoundEffect effect;
        static SoundEffectInstance soundInstance;
        public MineBlast()
        {
            initialise();
        }
        public void initialise()
        {
            stream = TitleContainer.OpenStream("sound_clips/mine_blast.wav");
            effect = SoundEffect.FromStream(stream);
            soundInstance = effect.CreateInstance();
        }
        public static void play()
        {
            soundInstance.Play();
        }
        public static void stop()
        {
            soundInstance.Stop();
        }
    }
    class GameMusic
    {
        static Stream stream;
        static SoundEffect effect;
        static SoundEffectInstance soundInstance;
        public GameMusic()
        {
            initialise();
        }
        public void initialise()
        {
            stream = TitleContainer.OpenStream("sound_clips/game_music.wav");
            effect = SoundEffect.FromStream(stream);
            soundInstance = effect.CreateInstance();
            soundInstance.IsLooped = true;
        }
        public static void play()
        {
            soundInstance.Play();
        }
        public static void stop()
        {
            soundInstance.Stop();
        }
    }
    public class BlockCapture
    {
        static Stream stream;
        static SoundEffect effect;
        static SoundEffectInstance soundInstance;
        public BlockCapture()
        {
            initialise();
        }
        public void initialise()
        {
            stream = TitleContainer.OpenStream("sound_clips/block_capture.wav");
            effect = SoundEffect.FromStream(stream);
            soundInstance = effect.CreateInstance();
        }
        public static void play()
        {
            soundInstance.Play();
        }
        public static void stop()
        {
            soundInstance.Stop();
        }
    }
}
