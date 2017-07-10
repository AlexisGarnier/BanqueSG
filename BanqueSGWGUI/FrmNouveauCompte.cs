using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Banque;

namespace BanqueWindowsGUI

{
    /// <summary>
    /// Création d'un nouveau compte  externe à la banque
    /// </summary>
    public partial class FrmNouveauCompte : Form
    {
        Compte nouveauCompte;
        public FrmNouveauCompte()
        {
            InitializeComponent();
        }

        public FrmNouveauCompte(Compte compte) : this()
        {
            nouveauCompte = compte;
        }
        #region Evenements
        private void FrmNouveauCompte_Load(object sender, EventArgs e)
        { }
        private void btnValider_Click(object sender, EventArgs e)
        {
            if (nouveauCompte.IsValidKey(codeBanqueTextBox.Text, codeGuichetTextBox.Text, numeroCompteTextBox.Text, cleRIBTextBox.Text))
            {
                nouveauCompte.ajoutProprieteCompte(codeBanqueTextBox.Text, codeGuichetTextBox.Text, numeroCompteTextBox.Text, cleRIBTextBox.Text,libellécompteTextBox.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(string.Format("La cléRIB ne correspond pas aux données renseignées. Vérifiez la validité des données", MessageBoxButtons.OK));
                DialogResult = DialogResult.None;
            }
        }
        private void btnAbandonner_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void codeBanqueTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nouveauCompte.IsValidDigitN(codeBanqueTextBox.Text, 5))
            {
                codeBanqueTextBox.Text = nouveauCompte.completerChaine(codeBanqueTextBox.Text, 5);
                errorProvider1.SetError(codeBanqueTextBox, string.Empty);
            }
            else
            {
                errorProvider1.SetError(codeBanqueTextBox, "Code Banque Invalide, ne peut être vide, ne doit comporter que des chiffres , longueur maximum 5 caractères");
                e.Cancel = true;
            }
        }
        private void codeGuichetTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nouveauCompte.IsValidDigitN(codeGuichetTextBox.Text, 5))
            {
                codeGuichetTextBox.Text = nouveauCompte.completerChaine(codeGuichetTextBox.Text, 5);
                errorProvider1.SetError(codeGuichetTextBox, string.Empty);
            }
            else
            {
                errorProvider1.SetError(codeGuichetTextBox, "Code Guichet Invalide, ne peut être vide, ne doit comporter que des chiffres , longueur maximum 5 caractères");
                e.Cancel = true;
            }
        }
        private void numeroCompteTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nouveauCompte.IsValidNumeroCompte(numeroCompteTextBox.Text, 11))
            {
                numeroCompteTextBox.Text = nouveauCompte.completerChaine(numeroCompteTextBox.Text, 11);
                errorProvider1.SetError(numeroCompteTextBox, string.Empty);
            }
            else
            {
                errorProvider1.SetError(numeroCompteTextBox, "Numéro de Compte Invalide, ne peut être vide, ne doit comporter que des chiffres ou lettres maj, longueur maximum 11 caractères");
                e.Cancel = true;
            }
        }
        private void cleRIBTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nouveauCompte.IsValidDigitN(cleRIBTextBox.Text, 2))
            {
                errorProvider1.SetError(cleRIBTextBox, string.Empty);
            }
            else
            {
                errorProvider1.SetError(cleRIBTextBox, "Clé RIB Invalide, ne peut être vide, ne doit comporter que des chiffres, lougueur maximum 2 caractères");
                e.Cancel = true;
            }
        }
        private void libellécompteTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (nouveauCompte.IsValidLibelleCompte(libellécompteTextBox.Text))
            {
                errorProvider1.SetError(libellécompteTextBox, string.Empty);
            }
            else
            {
                errorProvider1.SetError(libellécompteTextBox, "Libellé Invalide, ne peut être vide !");
                e.Cancel = true;
            }
        }
        #endregion
       
    }
}
