using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoGalleryWPF.Model
{
    class Image : BaseModel
    {
        private string _Path;

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                if (_Path != value)
                {
                    _Path = value;
                    RaisePropertyChanged("Path");
                }
            }
        }

        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private BitmapImage _BitmapImg;

        public BitmapImage BitmapImg
        {
            get
            {
                return _BitmapImg;
            }
            set
            {
                if (_BitmapImg != value)
                {
                    _BitmapImg = value;
                    RaisePropertyChanged("BitmapImg");
                }
            }
        }

        private float? _Size;

        public float? Size
        {
            get
            {
                return _Size;
            }
            set
            {
                if (_Size != value)
                {
                    _Size = value;
                    RaisePropertyChanged("Size");
                }
            }
        }

        private DateTime? _CreationDate;

        public DateTime? CreationDate
        {
            get
            {
                return _CreationDate;
            }
            set
            {
                if (_CreationDate != value)
                {
                    _CreationDate = value;
                    RaisePropertyChanged("CreationDate");
                }
            }
        }

        private DateTime? _LastModificationDate;

        public DateTime? LastModificationDate
        {
            get
            {
                return _LastModificationDate;
            }
            set
            {
                if (_LastModificationDate != value)
                {
                    _LastModificationDate = value;
                    RaisePropertyChanged("LastModificationDate");
                }
            }
        }
    }
}
