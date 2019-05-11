using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IOPath = System.IO.Path;

namespace PhotoGalleryWPF.ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        private readonly IDialogService dialogService;

        private string path;

        public ViewModelMain(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            BrowseFolderCommand = new RelayCommand(BrowseFolder);
            OpenFileCommand = new RelayCommand(OpenFile);
        }


        public ICommand BrowseFolderCommand { get; }

        private void BrowseFolder()
        {
            var settings = new FolderBrowserDialogSettings
            {
                Description = "Select folder with images",
                SelectedPath = IOPath.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };

            bool? success = dialogService.ShowFolderBrowserDialog(this, settings);
            if (success == true)
            {
                Path = settings.SelectedPath;
            }
        }

        public ICommand OpenFileCommand { get; }

        private void OpenFile()
        {
            var settings = new OpenFileDialogSettings
            {
                Title = "Select image",
                InitialDirectory = IOPath.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png"
            };

            bool? success = dialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                Path = settings.FileName;
            }
        }

        public string Path
        {
            get => path;

            set
            {
                if(path != value)
                {
                    path = value;
                    RaisePropertyChanged("Path");
                }
            }
        }
    }
}
