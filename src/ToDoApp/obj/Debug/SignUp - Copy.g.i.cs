#pragma checksum "..\..\SignUp - Copy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4D1DB2FBC7AEC1DE7C681EBE9F6DF24D86731AFB46FF3C32C37CB6C4FFECF6F7"
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
    /// SignUp
    /// </summary>
    public partial class SignUp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\SignUp - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush imgAvatarSelection;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\SignUp - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserAccount;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\SignUp - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\SignUp - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRePassword;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\SignUp - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDisplayName;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\SignUp - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtErrorMessage;
        
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
            System.Uri resourceLocater = new System.Uri("/ToDoApp;component/signup%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SignUp - Copy.xaml"
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
            
            #line 17 "..\..\SignUp - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 21 "..\..\SignUp - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ImageButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.imgAvatarSelection = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 4:
            this.txtUserAccount = ((System.Windows.Controls.TextBox)(target));
            
            #line 34 "..\..\SignUp - Copy.xaml"
            this.txtUserAccount.KeyDown += new System.Windows.Input.KeyEventHandler(this.SignUp_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtPassword = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\SignUp - Copy.xaml"
            this.txtPassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.SignUp_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtRePassword = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\SignUp - Copy.xaml"
            this.txtRePassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.SignUp_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtDisplayName = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\SignUp - Copy.xaml"
            this.txtDisplayName.KeyDown += new System.Windows.Input.KeyEventHandler(this.SignUp_KeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtErrorMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            
            #line 56 "..\..\SignUp - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

