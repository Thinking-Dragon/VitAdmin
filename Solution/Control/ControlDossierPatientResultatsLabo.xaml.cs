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

namespace VitAdmin.Control
{
    /// <summary>
    /// Interaction logic for ControlDossierPatientResultatsLabo.xaml
    /// </summary>
    public partial class ControlDossierPatientResultatsLabo : UserControl
    {
        public ControlDossierPatientResultatsLabo(Citoyen patient, Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = new ControlModelDossierPatientResultatsLabo(patient, hospit /*résultat de la requête*/);
        }
    }
}