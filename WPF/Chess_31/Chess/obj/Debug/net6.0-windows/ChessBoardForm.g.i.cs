﻿#pragma checksum "..\..\..\ChessBoardForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0454BCADDDFD2B0378127E047802C7EC4D365BC3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Chess;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Chess {
    
    
    /// <summary>
    /// ChessBoardForm
    /// </summary>
    public partial class ChessBoardForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Chess.ChessBoardForm MyWindow;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement TickPlayer;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel EatenFirstTeam;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel EatemSecondTeam;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Info;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimeLeftLabelTeamFirst;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimeLeftLabelTeamSecond;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView NotationView;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border borderField;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ChessBoard;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\ChessBoardForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Field;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Chess;component/chessboardform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ChessBoardForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MyWindow = ((Chess.ChessBoardForm)(target));
            return;
            case 2:
            this.TickPlayer = ((System.Windows.Controls.MediaElement)(target));
            
            #line 14 "..\..\..\ChessBoardForm.xaml"
            this.TickPlayer.MediaEnded += new System.Windows.RoutedEventHandler(this.MediaElement_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 17 "..\..\..\ChessBoardForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.NewGame);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\..\ChessBoardForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveGame);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 19 "..\..\..\ChessBoardForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveSteps);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 20 "..\..\..\ChessBoardForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RotateField);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 21 "..\..\..\ChessBoardForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTags);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 22 "..\..\..\ChessBoardForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit);
            
            #line default
            #line hidden
            return;
            case 9:
            this.EatenFirstTeam = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.EatemSecondTeam = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.Info = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 12:
            this.TimeLeftLabelTeamFirst = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.TimeLeftLabelTeamSecond = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.NotationView = ((System.Windows.Controls.ListView)(target));
            return;
            case 15:
            this.borderField = ((System.Windows.Controls.Border)(target));
            return;
            case 16:
            this.ChessBoard = ((System.Windows.Controls.Grid)(target));
            return;
            case 17:
            this.Field = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

