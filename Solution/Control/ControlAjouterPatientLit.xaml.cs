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
using VitAdmin.Model;
using VitAdmin.ControlModel;
using VitAdmin.Data;
using System.Collections.ObjectModel;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlAjouterPatientLit.xaml
    /// </summary>
    public partial class ControlAjouterPatientLit : UserControl
    {
        public Action CallRequeteLits { get; set; }
        //ObservableCollection<Lit> Lits { get; set; }
        public ControlAjouterPatientLit(Citoyen citoyen, Hospitalisation hospitalisation)
        {
            InitializeComponent();

            DataContext = new ControlModelAjouterPatientLit(citoyen, hospitalisation, new List<Lit>(), CallRequeteLits);
        }
    }
}
