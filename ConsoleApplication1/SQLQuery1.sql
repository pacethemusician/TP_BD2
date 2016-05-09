SELECT sp.StateProvinceID as id, sp.Name, count(p.BusinessEntityID) as personnes
FROM  Person.Person p
JOIN Person.BusinessEntity as be
ON p.BusinessEntityID = be.BusinessEntityID
JOIN Person.BusinessEntityAddress bea
ON be.BusinessEntityID = bea.BusinessEntityID
JOIN Person.AddressType at
ON bea.AddressTypeId = at.AddressTypeId
JOIN Person.Address ad
ON bea.AddressID = ad.AddressID
JOIN Person.StateProvince sp
ON ad.StateProvinceID = sp.StateProvinceID
WHERE at.Name = 'Home'
GROUP BY sp.StateProvinceID, sp.Name