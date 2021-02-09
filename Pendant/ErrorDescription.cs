using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Pendant
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("c44c5d0a-1a70-4f14-86bd-9812c7df8e63")]
    public class ErrorDescription : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDescription"/> class.
        /// </summary>
        public ErrorDescription() : base(null)
        {
            this.Caption = "ErrorDescription";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new ErrorDescriptionControl();
        }
    }
}
