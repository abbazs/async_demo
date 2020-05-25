using async.MVVMHelpers;
using System;
using System.Windows;

namespace async
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            BindingErrorListener.Listen(m => Console.WriteLine(m));
            InitializeComponent();
        }
    }
}
