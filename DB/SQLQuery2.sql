create table salesman(
  salesman_id int primary key identity(1,1),
  name varchar(20) not null,
  city varchar(20),
  commission float
);

select * from salesman

insert into salesman( name, city, commission)
values('james Hoog','New York', 0.15)

insert into salesman( name, city, commission)
values('Nail Knite','Paris', 0.13)

insert into salesman( name, city, commission)
values('pit alex','London', 0.11)

insert into salesman( name, city, commission)
values('Mc Lyon','Paris', 0.14)

insert into salesman( name, city, commission)
values('Lauson Hen', null, 0.12)

insert into salesman( name, city, commission)
values('paul adam','Rome', 0.13)

delete from salesman where salesman_id=2


create table customer (
  customer_id int primary key,
  cust_name varchar(20) not null,
  city varchar(20),
  grade int,
  salesman_id int foreign key references salesman(salesman_id)
);

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(302, 'Nick Rimando', 'New York', 100, 1)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(305, 'Graham Zusi', 'California', 200, 3)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(301, 'Brad Guzan', 'London', null, 4)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(304, 'Fabian Johns', 'Paris', 300, 5)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(307, 'Brad Davis', 'New York', 200, 1)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(309, 'Geoff Camero', 'Berlin', 100, 6)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(308, 'Julian Green', 'london', 300, 3)

insert into customer(customer_id, cust_name, city, grade, salesman_id)
values(303, 'jozi Altidar', 'Moscow', 200, 7)

create table orders(
  ord_no int primary key,
  purch_amt float not null,
  ord_date date,
  customer_id int foreign key references customer(customer_id),
  salesman_id int foreign key references salesman(salesman_id)

);


insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(701, 150.5,' 2012-10-05', 305, 3)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(709, 270.65,' 2012-09-10', 301, 4)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(702, 65.26,' 2012-10-05', 302, 1)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(704, 110.5,' 2012-08-17', 309, 6)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(707, 948.5,' 2012-09-10', 305, 3)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(705, 2400.6,' 2012-07-27', 307, 1)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(708, 5760,' 2012-09-10', 302, 1)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(710, 1983.43,' 2012-10-10', 304, 5)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(703, 2480.4,' 2012-10-10', 309, 6)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(712, 250.45,' 2012-06-27', 308, 3)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(711, 75.29,' 2012-08-17', 303, 7)

insert into orders(ord_no, purch_amt, ord_date, customer_id, salesman_id)
values(713, 3045.6,' 2012-04-25', 302, 1)

select * from customer

--Query1

SELECT s.name, c.cust_name, s.city from salesman s inner join customer c on s.city = c.city;

--Query2

SELECT o.ord_no, o.purch_amt, c.cust_name, c.city from orders o inner join customer c on o.customer_id=c.customer_id where o.purch_amt>500 and o.purch_amt<2000;

--Query3

SELECT c.cust_name, c.city, s.name, s.commission from customer c inner join salesman s on c.salesman_id=s.salesman_id;

--Query4

SELECT c.cust_name, c.city, s.name, s.commission from customer c inner join salesman s on c.salesman_id = s.salesman_id where s.commission>0.12;

--Query5

SELECT c.cust_name, c.city, s.name, s.city, s.commission from salesman s inner join customer c on s.salesman_id=c.salesman_id where s.commission>0.12 and s.city!=c.city;

--Query6

SELECT o.ord_no, o.ord_date, o.purch_amt, c.cust_name ,c.city, s.name, s.commission from orders o
inner join customer c on o.customer_id=c.customer_id
inner join salesman s on o.salesman_id=s.salesman_id

--Query7

select o.*, c.cust_name, c.city as customer_city, c.grade, s.name as salesman_name, s.city as salesman_city, s.commission from orders o
inner join customer c on o.customer_id=c.customer_id
inner join salesman s on o.salesman_id=s.salesman_id

--Query8

select c.customer_id, c.cust_name, c.city as customer_city, c.grade, s.name as salesman_name, s.city as salesman_city from customer c inner join salesman s on c.salesman_id=s.salesman_id order by customer_id asc; 

--Query9

select c.customer_id, c.cust_name, c.city as customer_city, c.grade, s.name as salesman_name, s.city as salesman_city from customer c inner join salesman s on c.salesman_id=s.salesman_id where c.grade<300 order by customer_id asc; 


--Query10

select c.cust_name, c.city, o.ord_no, o.ord_date, o.purch_amt from customer c left outer join orders o on c.customer_id=o.customer_id order by o.ord_date

--Query11

select c.cust_name, c.city, o.ord_no, o.ord_date, o.purch_amt, s.name as salesman_name, s.commission from customer c
left outer join orders o on c.customer_id=o.customer_id
left outer join salesman s on c.salesman_id=s.salesman_id

--Query12

select s.name as "Salesman" from salesman s LEFT OUTER JOIN customer c on s.salesman_id=c.salesman_id order by c.salesman_id;


--Query13

select s.name as salesman, c.cust_name, c.city, c.grade, o.ord_no, o.ord_date, o.purch_amt from salesman s
left outer join customer c on s.salesman_id=c.salesman_id
left outer join orders o on c.customer_id=o.customer_id


--Query14

select s.name as salesman from salesman s
left outer join customer c on s.salesman_id=c.salesman_id
left outer join orders o on c.customer_id=o.customer_id
where o.purch_amt>2000 and c.grade is not null 

--Query15

select s.name as salesman from salesman s
left outer join customer c on s.salesman_id=c.salesman_id
left outer join orders o on c.customer_id=o.customer_id
where o.purch_amt>2000 and c.grade is not null 


--Query16

select c.cust_name, c.city, o.ord_no, o.ord_date, o.purch_amt from customer c
left outer join orders o on c.customer_id=o.customer_id
where c.grade is not null

--Query17

select s.*, c.* from salesman s 
cross join customer c

--Query 18

select s.*, c.* from salesman s 
cross join customer c
where s.city=c.city

--Query19

select s.*, c.* from salesman s 
cross join customer c
where s.city=c.city and grade is not null

--Query20

select s.*, c.* from salesman s 
cross join customer c
where s.city!=c.city and grade is not null


















