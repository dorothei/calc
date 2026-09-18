using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AngouriMath;
using System.Globalization;

namespace wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void NumberButtonClick(object sender, EventArgs e)
    {
        if (TextBox.Text == "Error")
        {
            TextBox.Text = "";
        }
        if (sender is Button button)
        {
            string number = button.Content.ToString()!;
            TextBox.Text += number;
        }
    }

    public void ClearButtonClick(object sender, EventArgs e)
    {
        TextBox.Text = "";
    }

    public void SolutionButtonClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextBox.Text))
        {
            try
            {
                var expression = MathS.FromString(TextBox.Text);
                var numeric = expression.EvalNumerical();
                double value = (double)numeric;
                TextBox.Text = value.ToString("0.##########", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                TextBox.Text = "Error";
            }
        }
    }

    public void DeleteButtonClick(object sender, EventArgs e)
    {
        if (TextBox.Text == "Error")
        {
            TextBox.Text = "";
        }
        if (!string.IsNullOrEmpty(TextBox.Text))
        {
            TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1);
        }
    }

    public void WindowKeyDown(object sender, KeyEventArgs e)
    {
        if (TextBox.Text == "Error")
        {
            TextBox.Text = "";
        }
        if (e.Key == Key.Back)
        {
            DeleteButtonClick(sender, e);
            e.Handled = true;
            return;
        }
        else if (e.Key == Key.Enter)
        {
            SolutionButtonClick(sender, e);
            e.Handled = true;
            return;
        }
        else if (e.Key == Key.C)
        {
            ClearButtonClick(sender, e);
            e.Handled = true;
            return;
        }
        TextBox.Text += e.Key switch
        {
            Key.D1 or Key.NumPad1 => "1",
            Key.D2 or Key.NumPad2 => "2",
            Key.D3 or Key.NumPad3 => "3",
            Key.D4 or Key.NumPad4 => "4",
            Key.D5 or Key.NumPad5 => "5",
            Key.D6 or Key.NumPad6 => Keyboard.Modifiers == ModifierKeys.Shift ? "^" : "6",
            Key.D7 or Key.NumPad7 => "7",
            Key.D8 or Key.NumPad8 => Keyboard.Modifiers == ModifierKeys.Shift ? "*" : "8",
            Key.D9 or Key.NumPad9 => Keyboard.Modifiers == ModifierKeys.Shift ? "(" : "9",
            Key.D0 or Key.NumPad0 => Keyboard.Modifiers == ModifierKeys.Shift ? ")" : "0",
            Key.OemComma or Key.OemPeriod => ".",
            Key.OemPlus or Key.Add => "+",
            Key.OemMinus or Key.Subtract => "-",
            Key.Multiply => "*",
            Key.Divide or Key.OemQuestion => "/",
            Key.OemOpenBrackets => "(",
            Key.OemCloseBrackets => ")",
            _ => null,
        };
    }
}
