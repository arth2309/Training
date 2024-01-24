create table department
(
   dep_id int primary key,
   dep_name varchar(20) not null
);

insert into department(dep_id, dep_name)
values(1001, 'FINANCE');

insert into department(dep_id, dep_name)
values(2001, 'AUDIT');

insert into department(dep_id, dep_name)
values(3001, 'MARKETING');

insert into department(dep_id, dep_name)
values(4001, 'PRODUCTION');

SELECT * FROM employee ;

create table employee
(
  emp_id int primary key,
  dep_id int foreign key references department(dep_id),
  mng_id int,
  emp_name varchar(20),
  salary int
);

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(68319, 1001, null, 'Kayling', 6000)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(66928, 3001, 68319, 'Blaze', 2750)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(67832, 1001, 68319, 'Clare', 2550)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(65646, 2001, 68319, 'Jonas', 2957)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(67858, 2001, 65646, 'Scarlet', 3100)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(69062, 2001, 65646, 'Frank', 3100)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(63679, 2001, 69062, 'Sandrine', 900)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(64989, 3001, 66928, 'Adelyn', 1700)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(65271, 3001, 66928, 'wade', 1350)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(66564, 3001, 66928, 'madden', 1350)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(68454, 3001, 66928, 'tucker', 1600)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(68736, 2001, 67858, 'Adnres', 1200)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(69000, 3001, 66928, 'julius', 1050)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(69324, 1001, 67832, 'Marker', 1400)

insert into employee(emp_id, dep_id, mng_id, emp_name, salary)
values(69325, 4001, null, 'ghost', 14000)

--Query1

select  e.emp_name, d.dep_name, e.salary from employee as e left join department as d on e.dep_id=d.dep_id where e.salary in (select max(salary) from employee group by dep_id);

--Query2

select Count(e.dep_id), d.dep_name from employee e left join department d on e.dep_id=d.dep_id  group by d.dep_name having count(e.dep_id)<3 

--Query3

select Count(e.emp_name) as no_of_employee, d.dep_name from employee e left join department d on e.dep_id=d.dep_id group by d.dep_name 

--Query4

select sum(e.salary) as total_salary, d.dep_name from employee e left join department d on e.dep_id=d.dep_id group by d.dep_name 


