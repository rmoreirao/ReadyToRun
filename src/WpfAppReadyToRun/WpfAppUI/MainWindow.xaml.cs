using LibAWpfDependsOnThis;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadDll_Click(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.LoadFrom("LibDNotDepedencyButProjectReference.dll");
            Type myClassLibD = assembly.GetType("LibDNotDepedencyButProjectReference.ClassLibD");
            String message = (String)myClassLibD.InvokeMember(
                            "GetMessage",
                            BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Static,
                            null,
                            null,
                            null);

            MessageBox.Show(message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var modules = Process.GetCurrentProcess()
                .Modules
                .Cast<ProcessModule>()
                .Select(m => new { Name = m.ModuleName, Size = m.ModuleMemorySize })
                .ToArray();

            ListProcesses.Items.Clear();

            foreach (var module in modules)
            {
                if (module.Name.Contains(FilterProcessText.Text))
                    ListProcesses.Items.Add($"{module.Name} - {module.Size}");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var libA = new Class1LibA();
            libA.Test();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Button_Click(null, null);
        }

        private void LoadLibC_Click(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.LoadFrom("../../../../LibCNotDependency/bin/Release/net7.0/LibCNotDependency.dll");
            Type myClassLibD = assembly.GetType("LibCNotDependency.ClassLibC");
            String message = (String)myClassLibD.InvokeMember(
                            "GetMessage",
                            BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Static,
                            null,
                            null,
                            null);

            MessageBox.Show(message);
        }
    }
}
