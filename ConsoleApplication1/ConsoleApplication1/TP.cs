using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TP
    {
        static void Main(string[] args)
        {
            using (var context = new Model1())
            {
                context.Database.CreateIfNotExists();
                no1(context);
                no2(context);
                //no3(context);
            }
        }

        // NUMERO 1 !!!!!!!!!!!!!!!!!!!
        // sp.StateProvinceID as id, sp.Name, count(p.BusinessEntityID)
        static void no1(Model1 context) {
            Console.WriteLine("// NUMERO 1 //////////////////////////////////////////////");
            Console.WriteLine("ID          Name                           Personnes");
            Console.WriteLine("----------- ------------------------------ -----------");

            var query = context.Person
                        .GroupBy(a => a.BusinessEntity.BusinessEntityAddress.FirstOrDefault(prout => prout.AddressType.Name == "Home").Address.StateProvince).Where(p => p.Key != null)
                        .Select(ad => new {
                                            count = ad.Count(),
                                            nomProvince = ad.Key.Name,
                                            provinceId = ad.Key.StateProvinceID
                                          });
            foreach (var item in query)
            {
                Console.WriteLine("{0}{1}{2}",
                                        item.provinceId.ToString() + writeSpace(12 - item.provinceId.ToString().Length),
                                        item.nomProvince + writeSpace(31 - item.nomProvince.Length),
                                        item.count);
            }
            Console.ReadKey();
        }

        static string writeSpace(int value)
        {
            var s = "";
            for (int i = 0; i < value; i++)
			{
			    s = s + " ";
			}
            return s;
        }


            
        // NUMERO 2 !!!!!!!!!!!!!!!!!!!
        static void no2(Model1 context)
        {
            Console.WriteLine("// NUMERO 2 //////////////////////////////////////////////");
        }

        // NUMERO 3 !!!!!!!!!!!!!!!!!!!
        static void no3(Model1 context)
        {
            String stringId;
            Boolean parsed;
            int numId;
            Console.WriteLine("// NUMERO 3 //////////////////////////////////////////////");
            do
            {
                Console.Write("Entrez un numéro d'identification: ");
                stringId = Console.ReadLine();
                parsed = Int32.TryParse(stringId, out numId);
            } while (!parsed);

            var personne = context.Person.Find(numId);
            
            if (personne == null)
                Console.WriteLine("La personne n'existe pas");
            else
            {
                Console.WriteLine("#Identification: {0}", numId);
                Console.WriteLine("Prénom: {0} Nom: {1}", personne.FirstName, personne.LastName);
                foreach (var email in personne.EmailAddress)
                {
                    Console.WriteLine("Email: {0}", email.EmailAddress1);
                }
                foreach (var address in personne.BusinessEntity.BusinessEntityAddress){
                    Console.WriteLine("Addresse: {0}", address.Address.AddressLine1);
                    Console.WriteLine("          {0}", address.Address.AddressLine2);
                    Console.WriteLine("Province: {0}", address.Address.StateProvince.Name);
                    Console.WriteLine("Pays: {0}", address.Address.StateProvince.CountryRegion.Name);
                }
                foreach (var tel in personne.PersonPhone) {
                    Console.WriteLine("Téléphone ({0}): {1}", tel.PhoneNumberType.Name, tel.PhoneNumber);
                }
                foreach (var customer in personne.Customer) {
                    foreach (var soh in customer.SalesOrderHeader){
                        Console.WriteLine("--------------------------------------------------------------------------");
                        Console.WriteLine("# de commande: {0}                  Date de commande: {1}", soh.SalesOrderID, soh.OrderDate.ToShortDateString());
                        foreach (var detail in soh.SalesOrderDetail) {
                            var product = context.Product.Find(detail.ProductID);
                            Console.WriteLine("{0}X #{1} {2} $unitaire: {3} $standard: {4}", detail.OrderQty, product.ProductID, product.Name, detail.UnitPrice, product.StandardCost);
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
