using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace App
{
    public partial class MainWindow : Window
    {
        public List<Conversion> Conversions { get; set; }
        private List<Button> selectedButtons;
        
        public MainWindow()
        {
            InitializeComponent();
            LoadConversions();
            selectedButtons = new List<Button>();
        }

        // Method to load all pre configured conversions
        private void LoadConversions()
        {
            Conversions = new List<Conversion>
            {
                new Conversion { OldText = "a", NewText = "b" },
                new Conversion { OldText = "b", NewText = "c" },
                new Conversion { OldText = "c", NewText = "d" }
            };

            foreach (var conversion in Conversions)
            {
                AddConversionButton(conversion);
            }
        }

        // Method to add new custom conversions to the buttons
        private void AddConversionButton(Conversion conversion)
        {
            Button button = new Button
            {
                Name = "btnConversion",
                Content = conversion.ToString(),
                Tag = conversion,
                Margin = new Thickness(5),
                Background = Brushes.LightGray,
                Width = 50
            };

            button.Click += btnConversion_Click;
            wpButton.Children.Add(button);
        }

        // Logic behind selection and deselection of conversions
        private void btnConversion_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            // Select the button
            if (selectedButtons.Contains(button))
            {
                button.Background = Brushes.LightGray;
                selectedButtons.Remove(button);
            }
            // Deselect the button
            else
            {
                button.Background = Brushes.LightBlue;
                selectedButtons.Add(button);
            }
        }

        // Logic behind conversions
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            // Checking if conversions are selected
            if (selectedButtons.Count == 0)
            {
                MessageBox.Show("Please select at least one conversion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string input = tbInput.Text;
            char[] 
            var replacements = new List<Conversion>();

            foreach (Button button in selectedButtons)
            {
                Conversion conversion = button.Tag as Conversion;
                replacements.Add(conversion);
            }

            string output = input;
            foreach (Conversion conversion in replacements)
            {
                output = output.Replace(conversion.OldText, conversion.NewText);
            }

            tbOutput.Text = output;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string oldText = tbOldText.Text;
            string newText = tbNewText.Text;

            if (string.IsNullOrEmpty(oldText) || string.IsNullOrEmpty(newText))
            {
                MessageBox.Show("Both fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Conversion conversion = new Conversion() { OldText = oldText, NewText = newText};
            AddConversionButton(conversion);
            tbOldText.Clear();
            tbNewText.Clear();
        }

        public class Conversion
        {
            public required string OldText { get; set; }
            public required string NewText { get; set; }

            public override string ToString()
            {
                return OldText;
            }
        }
    }
}