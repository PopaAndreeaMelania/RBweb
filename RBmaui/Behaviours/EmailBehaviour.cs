using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;

namespace RBmaui.Behaviours
{
    public class EmailBehaviour : Behavior<Entry>
    {
        static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            bool ok = !string.IsNullOrWhiteSpace(e.NewTextValue) &&
                      EmailRegex.IsMatch(e.NewTextValue);

            entry.BackgroundColor = ok
                ? Color.FromRgba("#FFFFFF")
                : Color.FromRgba("#AA4A44");
        }
    }
}