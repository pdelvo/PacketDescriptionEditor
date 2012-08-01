using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDescriptionEditor.Model;

namespace PacketDescriptionEditor.ViewModels
{
    public class PacketViewModel : ViewModelBase
    {
        byte _id;
        public byte Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        string _name;

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        string _description;

        public string Description
        {
            get { return _description; }
            set { SetValue(ref _description, value); }
        }

        ObservableCollection<FieldViewModel> _fields = new ObservableCollection<FieldViewModel>();
        public ObservableCollection<FieldViewModel> Fields
        {
            get { return _fields; }
            set { SetValue(ref _fields, value); }
        }

        string _size;

        public string Size
        {
            get { return _size; }
            set { SetValue(ref _size, value); }
        }

        public PacketViewModel()
        {

        }

        public PacketViewModel(PacketConfig item)
        {
            Name = item.Name;
            Description = item.Description;
            Fields = new ObservableCollection<FieldViewModel>(item.Fields.Select(f => new FieldViewModel(f)));
            Size = item.Size;

        }

        public PacketConfig GetConfig()
        {
            return new PacketConfig
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Fields = Fields.Select(f =>f.GetConfig()).ToList(),
                Size = Size
            };
        }
    }
}
