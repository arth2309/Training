select * from film

---4.1
select title, replacement_cost,  rental_duration   
from film where rental_duration 
between 4 and 6 order by replacement_cost desc 
limit 100;

--4.2
select title,rating,description, length from film 
where rating in('G','PG')
and length>120
and description like ('%Action%')

select * from actor

--6.1
select first_name, count(first_name) from actor 
group by first_name 
order by count(first_name) desc,first_name


