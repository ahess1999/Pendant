using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;


namespace TutorWindow
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    [Guid("f8efb7db-95be-4d59-a06e-6927fba6aa7c")]
    public class TutorToolWindow : ToolWindowPane
    {
        public IErrorList ErrorList { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TutorToolWindow"/> class.
        /// </summary>
        public TutorToolWindow() : base(null)
        {
            this.Caption = "Tutor Window";

            // We must be on the UI thread to create UI resources
            // (Adn this should be the case...)
            ThreadHelper.ThrowIfNotOnUIThread();

            // Get a reference to the Document Table Entries (DTE) object
            var dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;
            Assumes.Present(dte);

            // Create the control, providing a reference to the VS Error List
            Content = new TutorToolWindowControl()
            {
                ErrorList = dte.ToolWindows.ErrorList as IErrorList
            };
        }
    }
}
