using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravellerTracker.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrintPage : Page
    {
        public PrintPage()
        {
            this.InitializeComponent();
        }
        public PrintPage(RichTextBlockOverflow textLinkContainer)
       : this()
        {
            if (textLinkContainer == null)
                throw new ArgumentNullException(nameof(textLinkContainer));
            textLinkContainer.OverflowContentTarget = textOverflow;
        }

        internal Grid PrintableArea => printableArea;

        internal RichTextBlock TextContent => textContent;

        internal RichTextBlockOverflow TextOverflow => textOverflow;

        internal void AddContent(Paragraph block)
        {
            textContent.Blocks.Add(block);
        }
    }
}
