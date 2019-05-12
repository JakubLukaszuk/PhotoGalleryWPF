using MvvmDialogs.FrameworkDialogs;
using MvvmDialogs.FrameworkDialogs.MessageBox;


namespace PhotoGalleryWPF.Dialog.MessageBox
{
    public class DialogFactory : DefaultFrameworkDialogFactory
    {
        public override IMessageBox CreateMessageBox(MessageBoxSettings settings)
        {
            return new MessageBoxDialog(settings);
        }
    }
}
