using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace wpf;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void NumberButtonClick(object sender, EventArgs e)
    {
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
        try
        {
            var result = new DataTable().Compute(TextBox.Text.Pow().Replace(",", "."), null);
            TextBox.Text = result.ToString();
        }
        catch
        {
            TextBox.Text = "Error";
        }
    }

    public void DeleteButtonClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TextBox.Text))
        {
            TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1);
        }
    }

    public void WindowKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Back)
        {
            DeleteButtonClick(sender, e);
            e.Handled = true;
        }
        if (e.Key == Key.Enter)
        {
            SolutionButtonClick(sender, e);
            e.Handled = true;
        }
        if (e.Key == Key.C)
        {
            ClearButtonClick(sender, e);
            e.Handled = true;
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
            Key.D9 or Key.NumPad9 => "9",
            Key.D0 or Key.NumPad0 => "0",
            Key.OemComma => ",",
            Key.OemPlus or Key.Add => "+",
            Key.OemMinus or Key.Subtract => "-",
            Key.Multiply => "*",
            Key.Divide or Key.OemQuestion => "/",
            _ => ""
        };
    }
}

public static class UtilityMethods
{
    public static string Pow(this string textBox)
    {
        while (textBox.Contains('^'))
        {
            int caretIndex = textBox.IndexOf("^");
            string leftPart = textBox.Substring(0, caretIndex);
            string rightPart = textBox.Substring(caretIndex + 1);
            return Math.Pow(double.Parse(leftPart), double.Parse(rightPart)).ToString();
        }
        return textBox;
    }
}
