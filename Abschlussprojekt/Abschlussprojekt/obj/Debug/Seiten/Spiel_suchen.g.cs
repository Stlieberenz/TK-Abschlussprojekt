﻿#pragma checksum "..\..\..\Seiten\Spiel_suchen.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D8B0FEEAF0A4B24E8BC7078AE07BC99"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Abschlussprojekt.Seiten;
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


namespace Abschlussprojekt.Seiten {
    
    
    /// <summary>
    /// Spiel_suchen
    /// </summary>
    public partial class Spiel_suchen : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Seiten\Spiel_suchen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Spiel_suchen_grid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Seiten\Spiel_suchen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Hosts;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Seiten\Spiel_suchen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_aktualisieren;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Seiten\Spiel_suchen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_beitreten;
        
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
            System.Uri resourceLocater = new System.Uri("/Abschlussprojekt;component/seiten/spiel_suchen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Seiten\Spiel_suchen.xaml"
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
            this.Spiel_suchen_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Hosts = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.btn_aktualisieren = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Seiten\Spiel_suchen.xaml"
            this.btn_aktualisieren.Click += new System.Windows.RoutedEventHandler(this.btn_aktualisieren_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_beitreten = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Seiten\Spiel_suchen.xaml"
            this.btn_beitreten.Click += new System.Windows.RoutedEventHandler(this.btn_beitreten_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

