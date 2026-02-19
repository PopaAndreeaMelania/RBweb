using Microsoft.Maui.Controls;

namespace RBmaui.Behaviours
{
    public class RequiredTextBehaviour : Behavior<Entry>
    {
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

            entry.BackgroundColor = string.IsNullOrWhiteSpace(e.NewTextValue)
                ? Color.FromRgba("#AA4A44")   
                : Color.FromRgba("#FFFFFF");
        }
    }
}
