namespace Pendant
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ErrorDescriptionControl.
    /// </summary>
    public partial class ErrorDescriptionControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDescriptionControl"/> class.
        /// </summary>
        public ErrorDescriptionControl()
        {
            this.InitializeComponent();
            Loaded += ErrorListBox_Loaded;
        }

        private void ErrorListBox_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(Error err in NamingConventionsAnalyzer.ErrorList)
            {
                ErrorListBox.Items.Add(err.TypeOfError);
            }
        }
    }
}