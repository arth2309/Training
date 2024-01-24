--stored procedure 1

create proc spGetAvgFreight
@cust_id varchar(15)
as
begin
select CustomerID, Avg(Freight) from Orders group by CustomerID having CustomerID=@cust_id
end



exec spGetAvgFreight 'CENTC'

--stored procedure 2

create procedure spGetSalesByCountry
@country nvarchar(20)
as
begin
    select e.EmployeeID, e.FirstName, e.LastName, count(e.EmployeeID) from Employees e 
    inner join Orders o on e.EmployeeID=o.EmployeeID 
    group by o.ShipCountry,e.EmployeeID,e.FirstName,e.LastName 
    having o.ShipCountry = @country
end

exec spGetSalesByCountry 'Finland'

--stored procedure 3


create proc spGetSalesByYear
@year int
as
begin
   select count(year(OrderDate)) from orders where year(OrderDate)= @year
end

exec spGetSalesByYear 1996

--stored procedure 4

create proc spGetSalesByCategory
@Category nvarchar(20)
as
begin
     select count(o.OrderID) from [Order Details Extended] as o
	 inner join Products p on o.ProductID=p.ProductID
	 inner join Categories c on p.CategoryID=c.CategoryID
	 where c.CategoryName=@Category
end
 
exec spGetSalesByCategory 'Beverages'

--stored procedure 5

create proc spGetExpensiveProducts
as
begin
  select top 10 ProductName, UnitPrice from Products order by UnitPrice desc
end

exec spGetExpensiveProducts 

--stored procedure 6


create proc spInesrtOrderDetails
@OrderID  int,
@ProductID int,
@UnitPrice float,
@Quantity int ,
@Discount float


as
begin

insert into [Order Details ]

values
(
@OrderID,
@ProductID,
@UnitPrice,
@Quantity,
@Discount 
)

end   

exec spInesrtOrderDetails 10248, 52, 16.00, 34, 0

--stored procedure 7

create proc spUpdateOrderDetails
@OrderID  int,
@ProductID int,
@UnitPrice float,
@Quantity int ,
@Discount float


as
begin

update [Order Details ]
set UnitPrice=@UnitPrice, Quantity=@Quantity, Discount=@Discount
where OrderID=@OrderID and ProductID=@ProductID

end   

exec spUpdateOrderDetails 10248, 45, 16, 40, 0





