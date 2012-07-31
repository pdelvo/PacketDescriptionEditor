using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketDescriptionEditor.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        Dictionary<string, HashSet<string>> _linkedProperties = new Dictionary<string, HashSet<string>>();

        protected void LinkProperties(string first, string second)
        {
            HashSet<string> firstConnections = new HashSet<string>();
            HashSet<string> secondConnections = new HashSet<string>();
            if (_linkedProperties.ContainsKey(first))
                firstConnections = _linkedProperties[first];
            else
                _linkedProperties.Add(first, firstConnections);
            if (_linkedProperties.ContainsKey(second))
                secondConnections = _linkedProperties[second];
            else
                _linkedProperties.Add(second, secondConnections);

            firstConnections.Add(second);
            secondConnections.Add(first);
        }

        protected void SetValue<T>(ref T backingField, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                var stackFrame = new StackTrace();

                var frame = stackFrame.GetFrame(1);

                var propertyName = frame.GetMethod().Name.Replace("set_", "");
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            IEnumerable<string> names = new[] { propertyName };
            if (_linkedProperties.ContainsKey(propertyName))
                names = names.Concat(_linkedProperties[propertyName]);
            OnPropertyChanged(names);
        }

        protected void OnPropertyChanged(IEnumerable<string> propertyNames)
        {
            var propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                foreach (var item in propertyNames)
                {
                    propertyChanged(this, new PropertyChangedEventArgs(item));
                }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
