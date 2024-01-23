
select * from products;

select ProductID, ProductName, UnitPrice FROM Products where UnitPrice<20 and Discontinued=0;

select ProductID, ProductName, UnitPrice FROM Products where UnitPrice>15 and UnitPrice<25;

select ProductName, UnitPrice FROM Products where UnitPrice>(select AVG(UnitPrice) from products);

SELECT TOP 10 ProductName, UnitPrice from Products order by UnitPrice DESC;

select discontinued, count(discontinued) from products group by discontinued;

select ProductName, UnitsInStock, UnitsOnOrder from products where  UnitsOnOrder>UnitsInStock;
