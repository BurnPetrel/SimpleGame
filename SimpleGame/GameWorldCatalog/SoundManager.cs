using System;
using System.Windows.Media;
using SimpleGame.Entities;

namespace SimpleGame
{

    public static class SoundManager
    {
        private static MediaPlayer _backgroundMusic;
        private static Uri _shootSoundUri;
        private static Uri _playerDeathSoundUri;
        private static Uri _enemyDeathSoundUri;

        private static readonly MediaPlayer[] _shootPlayers = new MediaPlayer[5];
        private static int _currentShootIndex = 0;

        public static void Initialize()
        {

            _backgroundMusic = new MediaPlayer();
            _backgroundMusic.Volume = 0.3;
            _backgroundMusic.MediaEnded += (s, e) =>
            {

                _backgroundMusic.Position = TimeSpan.Zero;
                _backgroundMusic.Play();
            };

            _shootSoundUri = new Uri("Sounds/shoot.wav", UriKind.Relative);
            _playerDeathSoundUri = new Uri("Sounds/PlayerDeath.wav", UriKind.Relative);
            _enemyDeathSoundUri = new Uri("Sounds/EnemyDeath.wav", UriKind.Relative);


            for (int i = 0; i < _shootPlayers.Length; i++)
            {
                _shootPlayers[i] = new MediaPlayer();
                _shootPlayers[i].Volume = 0.5;
            }

            _backgroundMusic.Open(new Uri("Sounds/background.mp3", UriKind.Relative));
        }

        public static void PlayBackgroundMusic()
        {
            _backgroundMusic.Play();
        }

        public static void StopBackgroundMusic()
        {
            _backgroundMusic.Pause();
            _backgroundMusic.Position = TimeSpan.Zero;
        }

        public static void PlayShootSound()
        {
            var player = _shootPlayers[_currentShootIndex];
            player.Stop();
            player.Open(_shootSoundUri);
            player.Play();

            _currentShootIndex = (_currentShootIndex + 1) % _shootPlayers.Length;
        }

        public static void PlayPlayerDeathSound()
        {
            var player = new MediaPlayer();
            player.Volume = 0.6;
            player.Open(_playerDeathSoundUri);
            player.Play();
        }

        public static void PlayEnemyDeathSound()
        {
            var player = new MediaPlayer();
            player.Volume = 0.5;
            player.Open(_enemyDeathSoundUri);
            player.Play();
        }
    }
}