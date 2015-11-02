namespace MyWeather.Controls
{
    using System;
    using System.Collections.Generic;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Documents;
    using Windows.UI.Xaml.Media;

    public class RichTextHelper : DependencyObject
    {
        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(RichTextHelper), new PropertyMetadata(String.Empty, OnTextChanged));

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RichTextBlock;
            if (control != null)
            {
                control.Blocks.Clear();
                var value = e.NewValue.ToString();

                var paragraph = ParagraphFromText(value);
                control.Blocks.Add(paragraph);
            }
        }

        private static Paragraph ParagraphFromText(string text)
        {
            var paragraph = new Paragraph();
            var color = Colors.Black;
            foreach (var run in GetTokens(text))
            {
                if (run.StartsWith("</")) { color = Colors.Black; continue; }
                if (run == "<red>") { color = Colors.Red; continue; }
                if (run == "<green>") { color = Colors.Green; continue; }
                if (run == "<blue>") { color = Colors.Blue; continue; }
                if (run == "<gray>") { color = Colors.Gray; continue; }
                if (run.StartsWith("\n")) paragraph.Inlines.Add(new LineBreak());
                else paragraph.Inlines.Add(new Run { Text = run, Foreground = new SolidColorBrush(color) });
            }

            return paragraph;
        }

        private static IEnumerable<string> GetTokens(string text)
        {
            var current = "";
            foreach (var c in text)
            {
                if (c == '\n') { yield return current; current = ""; yield return "\n"; continue; }
                if (c == '\r') continue;
                if (c == '<' && current != "") { yield return current; current = ""; }

                current += c;
                if (c == '>') { yield return current; current = ""; }
            }

            yield return current;
        }
    }
}
