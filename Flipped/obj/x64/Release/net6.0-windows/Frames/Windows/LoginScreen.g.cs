﻿#pragma checksum "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8B1CF7E52AB62CF538D23786D44D66A54F93FDF2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Flipped.Frames.Windows;
using MicaWPF.Controls;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Wpf.Ui;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;
using Wpf.Ui.Converters;
using Wpf.Ui.Markup;
using Wpf.Ui.ValidationRules;


namespace Flipped.Frames.Windows {
    
    
    /// <summary>
    /// LoginScreen
    /// </summary>
    public partial class LoginScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TopBar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.TextBox TokenBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ErrorBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMSG;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ValidBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ValidMSG;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Flipped;component/frames/windows/loginscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TopBar = ((System.Windows.Controls.Border)(target));
            
            #line 23 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            this.TopBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TopBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 24 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            this.TopBar.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.TopBar_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            this.TopBar.MouseMove += new System.Windows.Input.MouseEventHandler(this.TopBar_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 27 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            ((MicaWPF.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 28 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            ((MicaWPF.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Minimize);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TokenBox = ((Wpf.Ui.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 40 "..\..\..\..\..\..\Frames\Windows\LoginScreen.xaml"
            ((Wpf.Ui.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoginClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ErrorBox = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.ErrorMSG = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.ValidBox = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.ValidMSG = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

