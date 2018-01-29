using System;
using System.Windows.Input;

namespace ChatApplication
{
    class RelayCommand : ICommand
    {
        //Event raised when CanExecute Changes
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        //variable to store action
        private Action thisAction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            thisAction = action;
        }

        /// <summary>
        /// Set true for now 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Perfom Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            thisAction();
        }
    }
}
