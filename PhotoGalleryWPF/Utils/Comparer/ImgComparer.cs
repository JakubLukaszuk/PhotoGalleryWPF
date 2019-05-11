using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGalleryWPF.Model;


namespace PhotoGalleryWPF.Utils.Comparer
{
    public class ImgComparer
    {
        public class NameComparer : IComparer
        {
            private bool asc;
            public NameComparer(bool asc)
            {
                this.asc = asc;
            }

            public int Compare(object x, object y)
            {
                Image X = (Image)x;
                Image Y = (Image)y;

                if (asc)
                {
                    return stringComparer(X.Name, Y.Name);
                }
                else
                    return stringComparer(X.Name, Y.Name) * -1;
            }
        }

        public class SizeComparer : IComparer
        {
            private bool asc;

            public SizeComparer(bool asc)
            {
                this.asc = asc;
            }

            public int Compare(object x, object y)
            {
                Image X = (Image)x;
                Image Y = (Image)y;

                int result;

                if (X.Size > Y.Size)
                    result = 1;
                else if (X.Size == Y.Size)
                    result = 0;
                else
                    result = -1;

                if (!asc)
                    return -1 * result;
                else
                    return result;
            }
        }

        public class DataCreationComparer : IComparer
        {
            private bool asc;

            public DataCreationComparer(bool asc)
            {
                this.asc = asc;
            }

            public int Compare(object x, object y)
            {
                Image X = (Image)x;
                Image Y = (Image)y;

                if (asc)
                {
                    return dataComparer(X.CreationDate, Y.CreationDate);
                }
                else
                {
                    return -1 * dataComparer(X.CreationDate, Y.CreationDate);
                }
            }
        }

        public class DataModificationComparer : IComparer
        {
            private bool asc;

            public DataModificationComparer(bool asc)
            {
                this.asc = asc;
            }

            public int Compare(object x, object y)
            {
                Image X = (Image)x;
                Image Y = (Image)y;

                if (asc)
                {
                    return dataComparer(X.LastModificationDate, Y.LastModificationDate);
                }
                else
                {
                    return -1 * dataComparer(X.LastModificationDate, Y.LastModificationDate);
                }
            }
        }

        private static int stringComparer(string x, string y)
        {
            return x.CompareTo(y);
        }

        private static int dataComparer(DateTime? x, DateTime? y)
        {
            return DateTime.Compare((DateTime)x, (DateTime)y);
        }

    }
}
