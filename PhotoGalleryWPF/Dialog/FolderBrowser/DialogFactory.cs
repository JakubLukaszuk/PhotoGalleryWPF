using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGalleryWPF.Dialog.FolderBrowser
{
    public class DialogFactory : DefaultFrameworkDialogFactory
    {
        public override IFrameworkDialog CreateFolderBrowserDialog(FolderBrowserDialogSettings settings)
        {
            return new BrowserDialog(settings);
        }
    }
}
