using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace ChatApplication
{
    /// <summary>
    /// The base view model for main window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members
        
        //The window that view model controls.
        private Window _window;

        //This is the radius around the window that allows for drop shadow
        private int _outerMarginSize = 10;

        //Radius of the curve around the end of window
        private int _windowRadius = 10;

        #endregion

        #region Public Members

        //Minimum window height
        public double WindowMinimumWidth { get; set; } = 400;

        //Minimum window width
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// Resize Border
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// Thickness of the border that allows the window to resize
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// padding for the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        //Outer marging for drop shadow effect
        public int OuterMarginSize
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
            }
            set
            {
                _outerMarginSize = value;
            }
        }

        //Window radius for the curve around the window
        public int WindowRadius
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _windowRadius;
            }

            set
            {
                _windowRadius = value;
            }
        }

        //Thickness of the margin around the window to allow for drop shadow
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        //Corner radius
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        //The height of the title bar for the window
        public int TitleHeight { get; set; } = 42;

        //Height of the grid for title bar
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        /// <summary>
        /// Which application page is being displayed by the main window
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            this._window = window;

            //Events to fire in case of window resize
            _window.StateChanged += (sender, e) =>
             {
                 OnPropertyChanged(nameof(WindowRadius));
                 OnPropertyChanged(nameof(WindowCornerRadius));
                 OnPropertyChanged(nameof(OuterMarginSize));
                 OnPropertyChanged(nameof(OuterMarginSizeThickness));
                 OnPropertyChanged(nameof(ResizeBorderThickness));
             };

            //Commands
            MinimiseCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximiseCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());
            MenueCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

            //Fix window resizing issue
            var resizer = new WindowResizer(_window);
        }

        #endregion

        #region Command

        //command to Minimise the window
        public ICommand MinimiseCommand { get; set; }

        //command to Maximise the window
        public ICommand MaximiseCommand { get; set; }

        //command to open menue options for the window
        public ICommand MenueCommand { get; set; }

        //command to Close the window
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Get the current mouse position
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(_window);
            if(_window.WindowState != WindowState.Maximized)
            return new Point(position.X + _window.Left, position.Y+_window.Top);

            return new Point(position.X, position.Y);
        }


        #endregion
    }
}
