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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _world = new GameWorld();
            _world.SpawnEnemies();

            _world.Player.CreateVisual();
            GameCanvas.Children.Add(_world.Player.Visual);

            foreach (var enemy in _world.Enemies)
            {
                enemy.CreateVisual();
                GameCanvas.Children.Add(enemy.Visual);
            }

            _lastFrameTime = DateTime.UtcNow;
            CompositionTarget.Rendering += OnFrame;
        }

        private DateTime _lastFrameTime;

        private void OnFrame(object? sender, EventArgs e)
        {
            var now = DateTime.UtcNow;
            float deltaTime = (float)(now - _lastFrameTime).TotalSeconds;
            _lastFrameTime = now;

            _world.Update(deltaTime);


            _world.Player.UpdateVisualPosition();
            foreach (var enemy in _world.Enemies)
                enemy.UpdateVisualPosition();
        }
    }
}