using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Win32;
using PacketDescriptionEditor.Model;

namespace PacketDescriptionEditor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            _addPacketCommand = new ActionCommand(AddEmptyPacket);
            _importCommand = new ActionCommand(ImportState);
            _exportCommand = new ActionCommand(ExportState);

            LinkProperties("SelectedPacket", "IsPacketSelected");
        }

        #region MVVM

        ObservableCollection<PacketViewModel> _packets = new ObservableCollection<PacketViewModel>();
        public ObservableCollection<PacketViewModel> Packets
        {
            get { return _packets; }
            set { SetValue(ref _packets, value); }
        }

        PacketViewModel _selectedPacket;

        public PacketViewModel SelectedPacket
        {
            get { return _selectedPacket; }
            set { SetValue(ref _selectedPacket, value); }
        }

        ActionCommand _addPacketCommand;

        public ICommand AddPacketCommand
        {
            get { return _addPacketCommand; }
        }

        ActionCommand _importCommand;

        public ICommand ImportCommand
        {
            get { return _importCommand; }
        }

        ActionCommand _exportCommand;

        public ActionCommand ExportCommand
        {
            get { return _exportCommand; }
            set { SetValue(ref _exportCommand, value); }
        }

        public bool IsPacketSelected
        {
            get { return SelectedPacket != null; }
        }

        #endregion

        #region Methods

        public void AddEmptyPacket()
        {
            Packets.Add(new PacketViewModel
            {
                Id = 0x00,
                Name = "New Packet",
                Description = "A empty packet",
                Size = "1 Byte",
                Fields = new ObservableCollection<FieldViewModel>(new [] { new FieldViewModel{ Name = "Test", Type = "int", Example = "1", Note = "Just a test"}})
            });
        }

        public void ImportState()
        {
            if (MessageBox.Show("Do you really want to import a saved state and override everything?", "Import", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = ".xml",
                    DereferenceLinks = true,
                    Filter = "XML files (*.xml)|*.*",
                    FileName = "packets.xml",
                    Multiselect = false,
                    Title = "Choose file"
                };
                if (dialog.ShowDialog() == true)
                {
                    var serializer = new XmlSerializer(typeof(DescriptionConfig));
                    using (FileStream fileStream = File.Open(dialog.FileName, FileMode.Open))
                    {
                        SetConfig((DescriptionConfig)serializer.Deserialize(fileStream));
                    }
                }
            }
        }


        public void ExportState()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = ".xml",
                DereferenceLinks = true,
                Filter = "XML files (*.xml)|*.*",
                FileName = "packets.xml",
                Title = "Save file"
            }; 
            if (dialog.ShowDialog() == true)
            {
                DescriptionConfig config = GetConfig();
                var serializer = new XmlSerializer(typeof(DescriptionConfig));
                using (FileStream fileStream = File.Create(dialog.FileName))
                {
                    serializer.Serialize(fileStream, config);
                }
            }
        }

        private DescriptionConfig GetConfig()
        {
            DescriptionConfig config = new DescriptionConfig();

            config.Packets = new List<PacketConfig>();

            foreach (var item in Packets)
            {
                config.Packets.Add(item.GetConfig());
            }

            return config;
        }

        private void SetConfig(DescriptionConfig descriptionConfig)
        {
            SelectedPacket = null;
            Packets = new ObservableCollection<PacketViewModel>();
            foreach (var item in descriptionConfig.Packets)
            {
                Packets.Add(new PacketViewModel(item));
            }
        }

        #endregion

    }
}
