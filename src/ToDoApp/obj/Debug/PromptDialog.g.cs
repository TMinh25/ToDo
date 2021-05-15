﻿#pragma checksum "..\..\PromptDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1693A3A7224832AA42808E51F45E30084558BBB986FE2EF2DB9625BA20FC9FA3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using ToDoApp;


namespace ToDoApp {
    
    
    /// <summary>
    /// PromptDialog
    /// </summary>
    public partial class PromptDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\PromptDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\PromptDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblQuestion;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\PromptDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border answerInput;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\PromptDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAnswer;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\PromptDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDialogCancel;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\PromptDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDialogOk;
        
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
            System.Uri resourceLocater = new System.Uri("/ToDoApp;component/promptdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PromptDialog.xaml"
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
            
            #line 8 "..\..\PromptDialog.xaml"
            ((ToDoApp.PromptDialog)(target)).ContentRendered += new System.EventHandler(this.Window_ContentRendered);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 22 "..\..\PromptDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.lblQuestion = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.answerInput = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.txtAnswer = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\PromptDialog.xaml"
            this.txtAnswer.KeyDown += new System.Windows.Input.KeyEventHandler(this.Answer_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnDialogCancel = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.btnDialogOk = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\PromptDialog.xaml"
            this.btnDialogOk.Click += new System.Windows.RoutedEventHandler(this.btnDialogOk_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

