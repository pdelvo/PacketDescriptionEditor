﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDescriptionEditor.Model;

namespace PacketDescriptionEditor.ViewModels
{
    public class FieldViewModel : ViewModelBase
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        string _type;

        public string Type
        {
            get { return _type; }
            set { SetValue(ref _type, value); }
        }

        string _example;

        public string Example
        {
            get { return _example; }
            set { SetValue(ref _example, value); }
        }

        string _note;

        public string Note
        {
            get { return _note; }
            set { SetValue(ref _note, value); }
        }

        public FieldViewModel()
        {

        }

        public FieldViewModel(FieldConfig config)
        {
            Name = config.Name;
            Type = config.Type;
            Example = config.Example;
            Note = config.Note;
        }

        public FieldConfig GetConfig()
        {
            return new FieldConfig
            {
                Name = Name,
                Type = Type,
                Example = Example,
                Note = Note
            };
        }
    }
}
