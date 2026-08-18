using SimpleGame.Entities;
using SimpleGame.GameWorldCatalog;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleGame
{
    public partial class MainWindow : Window
    {
        private GameWorld _world;
        private DateTime _lastFrameTime;

        private bool _spacePressed;
        private bool _leftPressed;
        private bool _rightPressed;

        private enum GameState { Menu, Playing, GameOver }
        private GameState _currentState = GameState.Menu;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _currentState = GameState.Menu;
            UpdateMenuVisibility();
        }

        private void UpdateMenuVisibility()
        {
            if (_currentState == GameState.Menu)
            {
                MenuOverlay.Visibility = Visibility.Visible;
                MenuTitle.Text = "Simple Game";
                StartButton.Visibility = Visibility.Visible;
                RestartButton.Visibility = Visibility.Collapsed;
            }
            else if (_currentState == GameState.GameOver)
            {
                MenuOverlay.Visibility = Visibility.Visible;
                MenuTitle.Text = "GAME OVER";
                StartButton.Visibility = Visibility.Collapsed;
                RestartButton.Visibility = Visibility.Visible;
            }
            else
            {
                MenuOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private void InitializeGame()
        {
            _world = new GameWorld();
            _world.SpawnEnemies();
            _world.SpawnBarricades();

            _world.Player.CreateVisual();
            GameCanvas.Children.Add(_world.Player.Visual);

            foreach (var barricade in _world.Barricades)
            {
                barricade.CreateVisual();
                GameCanvas.Children.Add(barricade.Visual);
                barricade.UpdateVisualPosition();
            }

            _world.Player.UpdateVisualPosition();
            _lastFrameTime = DateTime.UtcNow;

            LivesTextBlock.Text = $"Lives: {_world.PlayerLives}";
            ScoreTextBlock.Text = $"Score: {_world.PlayerScore}";

            CompositionTarget.Rendering += OnFrame;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _currentState = GameState.Playing;
            UpdateMenuVisibility();
            InitializeGame();

            SoundManager.Initialize();
            SoundManager.PlayBackgroundMusic();

        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {

            GameCanvas.Children.Clear();

            _currentState = GameState.Playing;
            UpdateMenuVisibility();
            InitializeGame();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Left) _leftPressed = true;
            if (e.Key == Key.Right) _rightPressed = true;
            if (e.Key == Key.Space) _spacePressed = true;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Left) _leftPressed = false;
            if (e.Key == Key.Right) _rightPressed = false;
            if (e.Key == Key.Space) _spacePressed = false;
            base.OnKeyUp(e);
        }

        private void OnFrame(object? sender, EventArgs e)
        {
            if (_currentState != GameState.Playing) return;

            var now = DateTime.UtcNow;
            float deltaTime = (float)(now - _lastFrameTime).TotalSeconds;
            _lastFrameTime = now;

            _world.Update(deltaTime, _leftPressed, _rightPressed, _spacePressed);

            foreach (var obj in _world.NewVisuals)
            {
                obj.CreateVisual();
                GameCanvas.Children.Add(obj.Visual);
            }
            _world.NewVisuals.Clear();

            foreach (var obj in _world.RemovedVisuals)
            {
                GameCanvas.Children.Remove(obj.Visual);
            }
            _world.RemovedVisuals.Clear();

            _world.Player.UpdateVisualPosition();
            foreach (var enemy in _world.Enemies)
                enemy.UpdateVisualPosition();
            foreach (var proj in _world.Projectiles)
                proj.UpdateVisualPosition();

            ScoreTextBlock.Text = $"Score: {_world.PlayerScore}";
            LivesTextBlock.Text = $"Lives: {_world.PlayerLives}";

            if (_world.IsGameOver)
            {
                _currentState = GameState.GameOver;
                UpdateMenuVisibility();
                CompositionTarget.Rendering -= OnFrame;
            }
        }
    }
}