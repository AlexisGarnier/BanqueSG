using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Banque
{
    public class Compte
    {
        #region Champs
        private string _codeClient;
        private string _codeBanque;
        private string _codeGuichet;
        private string _numero;
        private string _cleRIB;
        private string _libelleCompte;


        #endregion
        #region Propriétés
        public string CodeClient
        {
            get { return _codeClient; }
            set { _codeClient = value; }
        }
        public string CodeBanque
        {
            get { return _codeBanque; }
            set { _codeBanque = value; }
        }
        public string CodeGuichet
        {
            get { return _codeGuichet; }
            set { _codeGuichet = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string CleRIB
        {
            get { return _cleRIB; }
            set { _cleRIB = value; }
        }
        public string LibelleCompte
        {
            get { return _libelleCompte; }
            set { _libelleCompte = value; }
        }
        #endregion
        #region Méthodes
        /// <summary>
        /// Chaine représentant l'objet instancié.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, @"{0};{1};{2};{3};{4};{5}", this.CodeClient, this.CodeBanque, this.CodeGuichet, this.Numero, this.CleRIB, this.LibelleCompte);
        }
        /// <summary>
        /// Deux comptes sont égaux si codes client,Banque,Guichet et Numéros de compte
        /// sont identiques 
        /// </summary>
        /// <returns>Vrai si les deux objets sont égaux</returns>
        public override bool Equals(Object compte)
        {
            Compte compteRef = compte as Compte;
            if (compteRef == null) return false;
            return (compteRef.CodeClient == this.CodeClient
                      && compteRef.CodeBanque == this.CodeBanque
                      && compteRef.CodeGuichet == this.CodeGuichet
                     && compteRef.Numero == this.Numero);
        }

        public override int GetHashCode()
        {
            int hashCode;
            hashCode = string.IsNullOrEmpty(_codeClient) ? 0 :_codeClient.GetHashCode();
            hashCode = string.IsNullOrEmpty(_codeBanque) ? hashCode : hashCode ^ _codeBanque.GetHashCode();
            hashCode = string.IsNullOrEmpty(_codeGuichet) ? hashCode : hashCode ^ _codeGuichet.GetHashCode();
            hashCode = string.IsNullOrEmpty(_numero) ? hashCode : hashCode ^ _numero.GetHashCode();
            return hashCode;
        }
        /// <summary>
        /// opérateur relationnel ==
        /// </summary>
        /// <param name="compteA">Instance Compte</param>
        /// <param name="compteB">Instance Compte</param>
        /// <returns>Vrai si égaux</returns>
        public static bool operator ==(Compte compteA, Compte compteB)
        {
            if ((object)compteA == null) return (object)compteB == null;
            return compteA.Equals(compteB);
        }
        /// <summary>
        ///  opérateur relationnel !=
        /// </summary>
        /// <param name="compteA">Instance Compte</param>
        /// <param name="compteB">Instance Compte</param>
        /// <returns>Vrai si différents</returns>
        public static bool operator !=(Compte compteA, Compte compteB)
        {
            if ((object)compteA == null) return (object)compteB != null;
            return !compteA.Equals(compteB);
        }
        
        /// <summary>
        /// Vérifie la validité du Libellé du Compte
        /// </summary>
        /// <param name="libelleCompte"></param>
        /// <returns>Vrai si Libellé Compte valide</returns>
        public bool IsValidLibelleCompte(string libelleCompte)
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
        /// <returns>Vrai si numero de compte valide</returns>
        public bool IsValidNumeroCompte(string numeroCompte, int n)
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
        /// Vérifie la validité du code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="n">longueur max du code valide</param>
        /// <returns>Vrai si code n'est composé que de digit et de longueur égale ou inférieure à n</returns>
        public bool IsValidDigitN(string code, int n)
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
        /// Complète la chaine fourni si la longueur est inférieur à n
        /// </summary>
        /// <param name="chaineFournie"></param>
        /// <param name="n">longueur max du code ou numéro valide</param>
        /// <returns>retourne une string de longueur n à laquelle on a accolé des 0 à gauche</returns>
        public string completerChaine(string chaineFournie, int n)
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
        /// <returns>Vrai si cleRIB est égal à cléControle</returns>
        public bool IsValidKey(string codeBanque, string codeGuichet, string numeroCompte, string cleRIB)
        {
            ulong reste;
            string cleControle;

            reste = ulong.Parse(codeBanque) % 97;
            reste = ulong.Parse(reste.ToString() + codeGuichet) % 97;
            reste = (ulong.Parse(reste.ToString() + convertNumeroCompte(numeroCompte)) * 100) % 97;
            cleControle = completerChaine((97 - reste).ToString(), 2);

            if (cleControle == cleRIB)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Convertit une chaine en digit
        /// </summary>
        /// <param name="numeroCompte"></param>
        /// <returns>retourne une chaine composée de digit</returns>
        public string convertNumeroCompte(string numeroCompte)
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
        /// Ajoute à un compte les propriétés transmises en argument
        /// </summary>
        /// <param name="codeBanque"></param>
        /// <param name="codeGuichet"></param>
        /// <param name="numeroCompte"></param>
        /// <param name="cleRIB"></param>
        /// <param name="libelleCompte"></param>
        public void ajoutProprieteCompte(string codeBanque, string codeGuichet, string numeroCompte, string cleRIB, string libelleCompte)
        {
            this.CodeBanque = codeBanque;
            this.CodeGuichet = codeGuichet;
            this.Numero = numeroCompte;
            this.CleRIB = cleRIB;
            this.LibelleCompte = libelleCompte;
        }
        #endregion
    }
}
