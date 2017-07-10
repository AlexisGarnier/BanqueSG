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
            if (IsValidKey(codeBanqueTextBox.Text, codeGuichetTextBox.Text, numeroCompteTextBox.Text, cleRIBTextBox.Text))
            {
                ajoutProprieteCompte(codeBanqueTextBox.Text, codeGuichetTextBox.Text, numeroCompteTextBox.Text, cleRIBTextBox.Text,libellécompteTextBox.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }
        private void btnAbandonner_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void codeBanqueTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (IsValidDigitN(codeBanqueTextBox.Text, 5))
            {
                codeBanqueTextBox.Text = completerChaine(codeBanqueTextBox.Text, 5);
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
            if (IsValidDigitN(codeGuichetTextBox.Text, 5))
            {
                codeGuichetTextBox.Text = completerChaine(codeGuichetTextBox.Text, 5);
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
            if (IsValidNumeroCompte(numeroCompteTextBox.Text, 11))
            {
                numeroCompteTextBox.Text = completerChaine(numeroCompteTextBox.Text, 11);
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
            if (IsValidDigitN(cleRIBTextBox.Text, 2))
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
            if (IsValidLibelleCompte(libellécompteTextBox.Text))
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
        #region Méthodes
        /// <summary>
        /// Vérifie la validité du Libellé du Compte
        /// </summary>
        /// <param name="libelleCompte"></param>
        /// <returns></returns>
        private bool IsValidLibelleCompte(string libelleCompte)
        {
            if (!string.IsNullOrEmpty(libelleCompte))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Vérifie la validité du numéro, composé de digit, de lettre Majuscule et de longueur égale ou inférieur à n
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <param name="n">longueur max du numéro valide</param>
        /// <returns></returns>
        private bool IsValidNumeroCompte(string numeroCompte, int n)
        {
            if (!string.IsNullOrEmpty(numeroCompte) && numeroCompte.Length <= n)
            {
                foreach (char caractere in numeroCompte)
                {
                    if (!char.IsLetterOrDigit(caractere))
                    {
                        return false;
                    }
                    if (char.IsLetter(caractere) && !char.IsUpper(caractere))
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Vérifie la validité du code, composé de digit et de longueur égale ou inférieure à n
        /// </summary>
        /// <param name="code"></param>
        /// <param name="n">longueur max du code valide</param>
        /// <returns></returns>
        private bool IsValidDigitN(string code, int n)
        {
            if (!string.IsNullOrEmpty(code) && code.Length <= n)
            {
                foreach (char caractere in code)
                {
                    if (!char.IsDigit(caractere))
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Complète la chaine fourni si la longueur est inférieur à n , retourne une string de longueur n à laquelle on a accolé des 0 à gauche
        /// </summary>
        /// <param name="chaineFournie"></param>
        /// <param name="n">longueur max du code ou numéro valide</param>
        /// <returns></returns>
        private string completerChaine(string chaineFournie, int n)
        {
            while (chaineFournie.Length < n)
            {
                chaineFournie = "0" + chaineFournie;
            }
            return chaineFournie;
        }
        /// <summary>
        /// Vérifie la validité de la cléRIB
        /// </summary>
        /// <param name="codeBanque"></param>
        /// <param name="codeGuichet"></param>
        /// <param name="numeroCompte"></param>
        /// <param name="cleRIB"></param>
        /// <returns></returns>
        private bool IsValidKey(string codeBanque, string codeGuichet, string numeroCompte, string cleRIB)
        {
            ulong reste;
            string cleControle;

            reste = ulong.Parse(codeBanque) % 97;
            reste = ulong.Parse(reste.ToString() + codeGuichet) % 97;
            reste = (ulong.Parse(reste.ToString() + convertNumeroCompte(numeroCompte)) * 100) % 97;
            cleControle = completerChaine((97 - reste).ToString(),2);

            if (cleControle == cleRIB)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Convertit le numéro de compte en digit pour calculer la clé de controle
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <returns></returns>
        private string convertNumeroCompte(string numeroCompte)
        {
            string numeroConverti = string.Empty;
            foreach (char caractere in numeroCompte)
            {
                if (char.IsLetter(caractere))
                {
                    int i;
                    Hollerith.Transcoder(caractere, out i);
                    numeroConverti += i.ToString();
                }
                else
                {
                    numeroConverti += caractere;
                }
            }
            return numeroConverti;
        }
        /// <summary>
        /// Ajoute au nouveau compte les propriétés transmises en argument
        /// </summary>
        /// <param name="codeBanque"></param>
        /// <param name="codeGuichet"></param>
        /// <param name="numeroCompte"></param>
        /// <param name="cleRIB"></param>
        /// <param name="libelleCompte"></param>
        private void ajoutProprieteCompte(string codeBanque, string codeGuichet, string numeroCompte, string cleRIB,string libelleCompte)
        {
            nouveauCompte.CodeBanque = codeBanque;
            nouveauCompte.CodeGuichet = codeGuichet;
            nouveauCompte.Numero = numeroCompte;
            nouveauCompte.CleRIB = cleRIB;
            nouveauCompte.LibelleCompte = libelleCompte;
        }
        #endregion
    }
}
