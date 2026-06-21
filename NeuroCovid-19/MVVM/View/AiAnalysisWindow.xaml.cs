using Markdig.Wpf;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuroCovid19.MVVM.View
{
    public partial class AiAnalysisWindow : Window
    {
        public AiAnalysisWindow(string markdownText, string subtitle = "Анализ кластера нейросетью DeepSeek")
        {
            InitializeComponent();

            SubtitleText.Text = subtitle;

            var doc = Markdig.Wpf.Markdown.ToFlowDocument(markdownText);
            doc.FontFamily = new FontFamily("Segoe UI");
            doc.FontSize = 14;
            doc.LineHeight = 1.5;
            doc.PagePadding = new Thickness(20, 15, 20, 15);
            MarkdownViewer.Document = doc;

            // Закрытие по Escape
            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                    Close();
            };
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var textRange = new TextRange(
                    MarkdownViewer.Document.ContentStart,
                    MarkdownViewer.Document.ContentEnd);

                if (!string.IsNullOrEmpty(textRange.Text))
                {
                    Clipboard.SetText(textRange.Text);
                    StatusText.Text = "\u2713 Скопировано в буфер обмена";
                }
            }
            catch
            {
                StatusText.Text = "\u2717 Ошибка копирования";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
