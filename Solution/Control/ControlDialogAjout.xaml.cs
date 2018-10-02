﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using VitAdmin.ControlModel;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlDialogAjout.xaml
    /// </summary>
    public partial class ControlDialogAjout : UserControl
    {
        public ControlDialogAjout(ICommand cmdAjout, string titre)
        {
            InitializeComponent();
            DataContext = new ControlModelDialogAjout(cmdAjout);
            lblTitre.Content = titre;
        }
    }
}
