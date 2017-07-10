using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banque;
namespace BanqueSGTests
{
    class Program
    {
        static void Main(string[] args)
        {
            creerComptes();
            testerHollerith();
        }

        private static void testerHollerith()
        {
            int letter = 0x0041;
            for (int i = 0; i < 26; i++)
            {
                int equivalent;
                char caractere = Char.Parse(char.ConvertFromUtf32(letter++));
                Hollerith.Transcoder(caractere, out equivalent);
                Console.WriteLine(string.Format("caractere : {0} valeur : {1}",caractere,equivalent));
            }
            Console.ReadLine();
        }

        private static void creerComptes()
        {
            Comptes comptes = new Comptes();
            comptes.Load(Properties.Settings.Default.BanqueAppData);
            Compte compte = new Compte();
            compte.CodeClient = "23456754";
            compte.CodeBanque = "20041";
            compte.CodeGuichet = "01006";
            compte.Numero = "0068875R027";
            compte.CleRIB = "70";
            compte.LibelleCompte = "Mickaël Barrer Banque Postale";
            comptes.Add(compte);
            compte = new Compte();
            compte.CodeClient = "23456754";
            compte.CodeBanque = "10907";
            compte.CodeGuichet = "00237";
            compte.Numero = "44219104266";
            compte.CleRIB = "03";
            compte.LibelleCompte = "Bost Banque Populaire courant";
            comptes.Add(compte);
            compte = new Compte();
            compte.CodeClient = "23456754";
            compte.CodeBanque = "10907";
            compte.CodeGuichet = "00237";
            compte.Numero = "64286104261";
            compte.CleRIB = "20";
            compte.LibelleCompte = "Bost CASDEN";
            comptes.Add(compte);
            comptes.Save(Properties.Settings.Default.BanqueAppData);

            comptes = new Comptes();
            comptes.Load(Properties.Settings.Default.BanqueAppData);
            foreach (Compte item in comptes)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
    }
}
