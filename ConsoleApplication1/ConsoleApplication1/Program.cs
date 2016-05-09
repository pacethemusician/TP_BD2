using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TP_no3
    {
        static void Main(string[] args)
        {
            using (var context = new Model1())
            {
                context.Database.CreateIfNotExists();
                String stringId;
                Boolean parsed;
                int numId;
                do
                {
                    Console.Write("Entrez un numéro d'identification: ");
                    stringId = Console.ReadLine();
                    parsed = Int32.TryParse(stringId, out numId);
                } while (!parsed);
                var query = from pod in c.PurchaseOrderDetail
                             group pod by new { pod.PurchaseOrderHeader.Vendor.Name } into g
                             orderby g.Key.Name
                             select new
                             {
                                 vendeur = g.Key.Name,
                                 cptProduit = g.Count()
                             };

                foreach (var x in query)
                {
                    Console.WriteLine(" {0}  {1} ", x.vendeur, x.cptProduit);
                }


                Console.ReadKey();
            }
         
 //SELECT  DISTINCT  c.PersonId, p.FirstName, p.LastName, 
 //           ea.EmailAddress,
 //           pnt.Name TelephoneType, pp.PhoneNumber,
 //           adr.AddressLine1, adr.AddressLine2, 
 //           sp.Name EtatProvince,
 //           cr.Name Pays,
 //           soh.SalesOrderID, soh.OrderDate, 
 //           sd.OrderQty,sd.ProductID, sd.UnitPrice, prod.Name, prod.StandardCost
 
//FROM Sales.SalesOrderHeader soh
//JOIN Sales.Customer c
//ON soh.CustomerID = c.CustomerID
//JOIN Person.Person p
//ON c.PersonId = p.BusinessEntityID
//LEFT JOIN Person.BusinessEntityAddress bea
//On p.BusinessEntityID = bea.BusinessEntityID
//JOIN Person.AddressType adrt
//ON bea.AddressTypeID = adrt.AddressTypeID
//LEFT JOIN Person.Address adr
//ON bea.AddressID = adr.AddressID
//JOIN Sales.SalesOrderDetail sd
//ON soh.SalesOrderID = sd.SalesOrderID
//JOIN Production.Product prod
//ON sd.ProductID = prod.ProductID 
//join Person.EmailAddress ea
//on ea.BusinessEntityID = p.BusinessEntityID
//join Person.StateProvince sp
//on sp.StateProvinceID = adr.StateProvinceID
//join Person.CountryRegion cr
//on cr.CountryRegionCode = sp.CountryRegionCode
//join Person.PersonPhone pp
//on pp.BusinessEntityID = p.BusinessEntityID
//join Person.PhoneNumberType pnt
//on pnt.PhoneNumberTypeID = pp.PhoneNumberTypeID
//WHERE adrt.Name  = 'Home' 
//and p.BusinessEntityID = 1701
//ORDER BY c.PersonID, soh.SalesOrderID, sd.ProductID

 
        }
    }
}
