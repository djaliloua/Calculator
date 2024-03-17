﻿using Calculator.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
namespace Calculator.MVVM.Views
{
    /// <summary>
    /// Interaction logic for InputResultView.xaml
    /// </summary>
    public partial class InputResultView : UserControl
    {
        public InputResultView()
        {
            InitializeComponent();
            DataContext = ServiceHelper.GetInputResultVM();
        }
    }
}
