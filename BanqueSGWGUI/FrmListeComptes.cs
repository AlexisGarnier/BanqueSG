using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Banque;
using BanqueWindowsGUI;

namespace BanqueSGWGUI
{
    public partial class FrmListeComptes : Form
    {
        private Comptes comptes; 
        public FrmListeComptes()
        {
            InitializeComponent();
        }

        private void btnAjouterCompte_Click(object sender, EventArgs e)
        {
            Compte nouveau = new Compte() {CodeClient="23456754" };
            FrmNouveauCompte nouveauCompte = new FrmNouveauCompte(nouveau);
            if (nouveauCompte.ShowDialog()==DialogResult.OK)
            {
                if(comptes.Add(nouveau))
                {
                    comptesComboBox.Items.Add(nouveau.LibelleCompte);
                    comptes.Save(BanqueWindowsGUI.Properties.Settings.Default.BanqueAppData);
                }
                else
                {
                    MessageBox.Show(string.Format("Le compte {0}-{1}-{2}-{3} ne peut être ajouté car il existe déjà", nouveau.CodeClient, nouveau.CodeGuichet, nouveau.Numero, nouveau.LibelleCompte), "Doublon", MessageBoxButtons.OK);
                }
            }
        }

        private void FrmListeComptes_Load(object sender, EventArgs e)
        {
            comptes = new Comptes();
            comptes.Load(BanqueWindowsGUI.Properties.Settings.Default.BanqueAppData);
            foreach (Compte item in comptes)
            {
                comptesComboBox.Items.Add(item.LibelleCompte);
            }   
        }
    }
}
