using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoryQ.Converter.Wpf.Properties;

namespace StoryQ.Converter.Wpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            ViewModel.Converter vm = (ViewModel.Converter) FindResource("vm");
            vm.PlainText = Settings.Default.InputText;
        }

        private void FocusLastChar(object sender, EventArgs e)
        {
            src.Focus();
            src.CaretIndex = src.Text.Length;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel.Converter vm = (ViewModel.Converter)FindResource("vm");
            Settings.Default.InputText = vm.PlainText;
            Settings.Default.Save();
        }
    }
}
