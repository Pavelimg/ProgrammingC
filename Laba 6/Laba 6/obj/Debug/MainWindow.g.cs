﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "06EB9B8199FDAF34427B33BADDF9E594A0E63DADEFE718AA24B5C668D3A7AB5B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Lab6 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid window1;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CountryComboBox;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PhoneTypeListBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PhoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LastNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FirstNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DayTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MonthComboBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YearTextBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorTextBlock;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Laba 6;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.window1 = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.CountryComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 7 "..\..\MainWindow.xaml"
            this.CountryComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CountryComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PhoneTypeListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 9 "..\..\MainWindow.xaml"
            this.PhoneTypeListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PhoneTypeListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PhoneTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\MainWindow.xaml"
            this.PhoneTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.PhoneTextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 15 "..\..\MainWindow.xaml"
            this.PhoneTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.PhoneTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 15 "..\..\MainWindow.xaml"
            this.PhoneTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.PhoneTextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 15 "..\..\MainWindow.xaml"
            this.PhoneTextBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PhoneTextBox_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LastNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\MainWindow.xaml"
            this.LastNameTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NameTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 19 "..\..\MainWindow.xaml"
            this.LastNameTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.NameTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.FirstNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\MainWindow.xaml"
            this.FirstNameTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NameTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 22 "..\..\MainWindow.xaml"
            this.FirstNameTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.NameTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DayTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\MainWindow.xaml"
            this.DayTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.DateTextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 25 "..\..\MainWindow.xaml"
            this.DayTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumericOnly_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.MonthComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 26 "..\..\MainWindow.xaml"
            this.MonthComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MonthComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.YearTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\MainWindow.xaml"
            this.YearTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.DateTextBox_TextChanged);
            
            #line default
            #line hidden
            
            #line 40 "..\..\MainWindow.xaml"
            this.YearTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumericOnly_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ErrorTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.SubmitButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\MainWindow.xaml"
            this.SubmitButton.Click += new System.Windows.RoutedEventHandler(this.SubmitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

