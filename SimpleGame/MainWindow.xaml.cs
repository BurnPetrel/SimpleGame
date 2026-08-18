using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleGame.GameWorldCatalog;
using SimpleGame.Entities;

namespace SimpleGame
{
    /// <summary>
    ///Draw the world
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameWorld _world;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private bool _spacePressed;
        private bool _leftPressed;
        private bool _rightPressed;

        private void OnLoaded(object sender, RoutedEventArgs e)
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
            CompositionTarget.Rendering += OnFrame;
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

        private DateTime _lastFrameTime;

        private void OnFrame(object? sender, EventArgs e)
        {
            var now = DateTime.UtcNow;
            float deltaTime = (float)(now - _lastFrameTime).TotalSeconds;
            _lastFrameTime = now;

            _world.Update(deltaTime, _leftPressed, _rightPressed, _spacePressed);

            // Добавляем новые визуалы
            foreach (var obj in _world.NewVisuals)
            {
                obj.CreateVisual();
                GameCanvas.Children.Add(obj.Visual);
            }
            _world.NewVisuals.Clear();

            // Удаляем мёртвые визуалы
            foreach (var obj in _world.RemovedVisuals)
            {
                GameCanvas.Children.Remove(obj.Visual);
            }
            _world.RemovedVisuals.Clear();

            // Обновляем позиции
            _world.Player.UpdateVisualPosition();
            foreach (var enemy in _world.Enemies)
                enemy.UpdateVisualPosition();
            foreach (var proj in _world.Projectiles)
                proj.UpdateVisualPosition();

            ScoreTextBlock.Text = $"Score: {_world.PlayerScore}";
            LivesTextBlock.Text = $"Lives: {_world.PlayerLives}";

            if (_world.IsGameOver)
            {
                ScoreTextBlock.Text = $"Game Over! Score: {_world.PlayerScore}";
                ScoreTextBlock.Foreground = System.Windows.Media.Brushes.Red; 
            }
        }
    }
}