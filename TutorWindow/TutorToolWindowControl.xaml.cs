using System.Collections.Generic;
using Microsoft.VisualStudio.Shell.TableManager;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;

namespace TutorWindow
{
    public class ErrorInfo
    {
        /// <summary>
        /// The Document this error occurrs in
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// The line on which this error occurs
        /// </summary>
        public int Line { get; set; }
        
        /// <summary>
        /// The text naming the error
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The code identifying the error
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// The severity of the error
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// The URL where the help documentation for this error is located
        /// </summary>
        public string HelpLink { get; set; }

        /// <summary>
        /// Converts the ErrorInfo into a string combining the error code and text
        /// </summary>
        /// <returns>A string identifying the error</returns>
        public override string ToString()
        {
            return $"{ErrorCode}: {Text}";
        }
    }


    /// <summary>
    /// Interaction logic for TutorToolWindowControl.
    /// </summary>
    public partial class TutorToolWindowControl : UserControl
    {
        IErrorList _errorList;
        
        /// <summary>
        /// The IErrorList this <see cref="TutorToolWindowControl"/> utilizes.
        /// </summary>
        public IErrorList ErrorList 
        {
            get => _errorList; 
            set
            {
                if (_errorList != null) _errorList.TableControl.EntriesChanged -= TableControl_EntriesChanged;
                _errorList = value;
                if(_errorList != null) _errorList.TableControl.EntriesChanged += TableControl_EntriesChanged;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TutorToolWindowControl"/> class.
        /// </summary>
        public TutorToolWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles when an error in the list is selected by the user by launching 
        /// an associated error description.
        /// </summary>
        /// <param name="sender">The list generating the event</param>
        /// <param name="e">The event arguments</param>
        private void Issues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                if(e.AddedItems[0] is ErrorInfo info)
                {
                    Viewer.Source = new System.Uri(info.HelpLink);
                }
                else
                {
                    // TODO: Change to a "no info found" page
                    Viewer.Source = null;
                }
            }
            else
            {
                // TODO: Change to a "no info found" page
                Viewer.Source = null;
            }
        }

        /// <summary>
        /// Updates the TutorWindowControl with the errors currently found in the VS Error Table
        /// </summary>
        /// <param name="sender">The VS Error table</param>
        /// <param name="e">The event parameters</param>
        /// <remarks>
        /// This handler keeps the TutorToolWindowControl synchronized with the Visual Studio error 
        /// list by effectively re-creating the items in the TutorToolWindow Issues list every time 
        /// the Visual Studio error list changes.  
        /// </remarks>
        private void TableControl_EntriesChanged(object sender, Microsoft.VisualStudio.Shell.TableControl.EntriesChangedEventArgs e)
        {
            List<ErrorInfo> issues = new List<ErrorInfo>();

            foreach(var entry in e.AllEntries)
            {
                // Use the ITableEntry.TryGetValue() to extract the needed information 
                // (if it exists)
                ErrorInfo info = new ErrorInfo();
                if (entry.TryGetValue(StandardTableKeyNames.ErrorCode, out string code))
                {
                    info.ErrorCode = code;
                }
                if(entry.TryGetValue(StandardTableKeyNames.HelpLink, out string link))
                {
                    info.HelpLink = link;
                }
                if(entry.TryGetValue(StandardTableKeyNames.Text, out string text))
                {
                    info.Text = text;
                }
                if(entry.TryGetValue(StandardTableKeyNames.DocumentName, out string document))
                {
                    info.DocumentName = document;
                }
                issues.Add(info);
            }

            Issues.ItemsSource = issues;
            Prompt.Visibility = issues.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        
    }
}