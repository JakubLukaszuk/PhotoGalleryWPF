using PhotoGalleryWPF.Helpers.Command;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using PhotoGalleryWPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhotoGalleryWPF.Model;
using IOPath = System.IO.Path;
using System.Collections.ObjectModel;
using PhotoGalleryWPF.Utils.Math;
using PhotoGalleryWPF.Utils.Comparer;
using System.Windows;

namespace PhotoGalleryWPF.ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        private readonly IDialogService dialogService;
        public PhotoGalleryWPF.Helpers.Command.RelayCommand setImages { get; private set; }
        public RelayCommand SortBySize { get; private set; }
        public RelayCommand SortByName { get; private set; }
        public RelayCommand SortByCreationDate { get; private set; }
        public RelayCommand SortByModificationDate { get; private set; }
        public RelayCommand SetSortOption { get; private set; }
        public RelayCommand SetImageFormList { get; private set; }
        private DataProvider dataProvider;

        private string path;
        public ObservableCollection<Image> Images { get; private set; }
        private Image image;
        private int _Rotation;
        bool SortFlag;

 



        public ViewModelMain(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            Image = new Image();
            Images = new ObservableCollection<Image>();
            dataProvider = new DataProvider();
            BrowseFolderCommand = new GalaSoft.MvvmLight.Command.RelayCommand(BrowseFolder);
            OpenFileCommand = new GalaSoft.MvvmLight.Command.RelayCommand(OpenFile);
            setImages = new RelayCommand(setImagesExecutable);
            SortBySize = new RelayCommand(sortBySizeExecutable);
            SetSortOption = new RelayCommand(setExecutableSortoption);
            SortByName = new RelayCommand(sortByNameExecutable);
            SortByCreationDate = new RelayCommand(sortByCreationDateExecutable);
            SortByModificationDate = new RelayCommand(sortByModifiactionDateExecutable);
        }

        private void setImagesFormFolder(string[] filters, string searchFolderPath)
        {
            Images.Clear();
            try
            {
                var filePaths = dataProvider.getPathsFilesFromFolder(searchFolderPath, filters, false);
                for (int i = 0; i < filePaths.Length; i++)
                {
                    Image image = new Image();
                    image.Path = filePaths[i];
                    image.BitmapImg = dataProvider.getBitmapFromPath(filePaths[i]);
                    image.Size = UnitConversion.ConvertBytesToMegabytes(dataProvider.getFileSizeMb(filePaths[i]));
                    image.CreationDate = dataProvider.getFileCreationTime(filePaths[i]);
                    image.LastModificationDate = dataProvider.getFileLastModificationTime(filePaths[i]);
                    image.Name = dataProvider.getFileName(filePaths[i]);
                    Images.Add(image);
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                ShowMessageWithoutGetingResult("The directory was probably removed.", "Directory not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.IO.FileNotFoundException)
            {
                ShowMessageWithoutGetingResult("The file was probably removed from selected directory.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (System.IO.FileFormatException)
            {
                ShowMessageWithoutGetingResult("Please select file with supported format.", "Wrong file format", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                ShowMessageWithoutGetingResult("Unknown Error", e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void setImageOrImages(IEnumerable<string> filters)
        {
            if (Path != null)
            {
                int iteration = 0;
                string[] filtersToGo = (string[])filters;
                foreach (string extenction in filters)
                {
                    iteration++;
                    if (Path.EndsWith(extenction))
                    {
                        try
                        {
                            Image.Path = Path;
                            Image.BitmapImg = dataProvider.getBitmapFromPath(Path);
                            Image.Size = UnitConversion.ConvertBytesToMegabytes(dataProvider.getFileSizeMb(Path));
                            Image.CreationDate = dataProvider.getFileCreationTime(Path);
                            Image.LastModificationDate = dataProvider.getFileLastModificationTime(Path);
                            Image.Name = dataProvider.getFileName(Path);
                        }
                        catch (System.IO.FileNotFoundException)
                        {
                            ShowMessageWithoutGetingResult("Please select another file.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        catch (System.IO.FileFormatException)
                        {
                            ShowMessageWithoutGetingResult("Please select file with supported format.", "Wrong file format", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception e)
                        {
                            ShowMessageWithoutGetingResult("Unknown Error", e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
                    else if (iteration == filtersToGo.Length)
                    {
                        setImagesFormFolder(filtersToGo, Path);
                        break;
                    }
                }

            }
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

        private void ShowMessageWithoutGetingResult(String text, String caption, MessageBoxButton button, MessageBoxImage image)
        {
             dialogService.ShowMessageBox(
                this,
                text,
                caption,
                button,
                image);
        }

        void sortBySizeExecutable(object parametr)
        {
            List<Image> tmp = new List<Image>(Images);
            Images.Clear();
            tmp.Sort(new ImgComparer.SizeComparer(SortFlag).Compare);
            Images = new ObservableCollection<Image>(tmp);
            RaisePropertyChanged("Images");
        }

        void sortByNameExecutable(object parametr)
        {
            List<Image> tmp = new List<Image>(Images);
            Images.Clear();
            tmp.Sort(new ImgComparer.NameComparer(SortFlag).Compare);
            Images = new ObservableCollection<Image>(tmp);
            RaisePropertyChanged("Images");
        }

        void sortByModifiactionDateExecutable(object parametr)
        {
            List<Image> tmp = new List<Image>(Images);
            Images.Clear();
            tmp.Sort(new ImgComparer.DataModificationComparer(SortFlag).Compare);
            Images = new ObservableCollection<Image>(tmp);
            RaisePropertyChanged("Images");
        }

        void sortByCreationDateExecutable(object parametr)
        {
            List<Image> tmp = new List<Image>(Images);
            Images.Clear();
            tmp.Sort(new ImgComparer.DataCreationComparer(SortFlag).Compare);
            Images = new ObservableCollection<Image>(tmp);
            RaisePropertyChanged("Images");
        }

        void setImageFormListExecutable(object parametr)
        {
            Image = (Image)parametr;
        }

        void setImagesExecutable(object parametr)
        {
            setImageOrImages(new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" });
        }

        void setExecutableSortoption(object parametr)
        {
            SortFlag = Convert.ToBoolean(Int32.Parse(parametr.ToString()));
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

        public Image Image
        {
            get
            {
                return image;
            }

            set
            {
                if (image != value && value != null)
                {
                    image = value;
                    RaisePropertyChanged("Image");
                }
            }
        }

        public int Rotation
        {
            get
            {
                return _Rotation;
            }
            set
            {
                if (_Rotation != value)
                {
                    _Rotation = value;
                    RaisePropertyChanged("Rotation");
                }
            }
        }

    }
}
