using System.ComponentModel;

//FINISCH: ThemeValue
namespace Killuas.UI.Theming.ViewModels
{
    /// <summary>
    /// A bindable wrapper for a <see cref="ThemeResources"/> value.
    /// Supports change notification to update the UI on theme switch.
    /// </summary>
    public class ThemeValue : INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private object? _value;

        /// <summary>
        /// The current value of the themed resource.
        /// </summary>
        public object? Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
    }
}
