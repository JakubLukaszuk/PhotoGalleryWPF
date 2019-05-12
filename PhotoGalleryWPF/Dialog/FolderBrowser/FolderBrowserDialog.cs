using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ookii.Dialogs.Wpf;


namespace PhotoGalleryWPF.Dialog.FolderBrowser
{
    public class BrowserDialog : IFrameworkDialog
    {
        private readonly FolderBrowserDialogSettings settings;
        private readonly VistaFolderBrowserDialog folderBrowserDialog;


        public BrowserDialog(FolderBrowserDialogSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

            folderBrowserDialog = new VistaFolderBrowserDialog
            {
                Description = settings.Description,
                SelectedPath = settings.SelectedPath,
                ShowNewFolderButton = settings.ShowNewFolderButton
            };
        }

        public bool? ShowDialog(Window owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            var result = folderBrowserDialog.ShowDialog(owner);

            // Update settings
            settings.SelectedPath = folderBrowserDialog.SelectedPath;

            return result;
        }
    }
}
