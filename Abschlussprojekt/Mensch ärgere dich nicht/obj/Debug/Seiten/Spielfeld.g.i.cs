﻿#pragma checksum "..\..\..\Seiten\Spielfeld.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "89BD271D8C02166A6F0D848B21876A4D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Mensch_ärgere_dich_nicht.Seiten;
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


namespace Mensch_ärgere_dich_nicht.Seiten {
    
    
    /// <summary>
    /// Spielfeld
    /// </summary>
    public partial class Spielfeld : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\Seiten\Spielfeld.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid G_spielfeld;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Seiten\Spielfeld.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_Würfel;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Seiten\Spielfeld.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Verlauf_msg;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Seiten\Spielfeld.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Msg;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Seiten\Spielfeld.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button send_msg;
        
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
            System.Uri resourceLocater = new System.Uri("/Mensch ärgere dich nicht;component/seiten/spielfeld.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Seiten\Spielfeld.xaml"
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
            
            #line 12 "..\..\..\Seiten\Spielfeld.xaml"
            ((Mensch_ärgere_dich_nicht.Seiten.Spielfeld)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\..\Seiten\Spielfeld.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.G_spielfeld = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.BTN_Würfel = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\Seiten\Spielfeld.xaml"
            this.BTN_Würfel.Click += new System.Windows.RoutedEventHandler(this.BTN_Würfel_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Verlauf_msg = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Msg = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.send_msg = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Seiten\Spielfeld.xaml"
            this.send_msg.Click += new System.Windows.RoutedEventHandler(this.send_msg_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

