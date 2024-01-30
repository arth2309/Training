select * from film;
select * from rental;
select * from customer;
select * from actor;
select * from film_actor;
select * from inventory;

---8.1
select f.title, f.rental_rate, l.name from film f 
inner join language l 
on f.language_id=l.language_id;

--8.2
select a1.first_name, a1.last_name, count(fa.film_id)
from actor a1
join film_actor fa on a1.actor_id=fa.actor_id
group by a1.actor_id
order by  count(fa.film_id) ;
;

--8.3
select f.rating, count(f.film_id) from film f
join inventory i on i.film_id=f.film_id
join rental r on r.inventory_id=i.inventory_id
group by f.rating
order by count(f.film_id) desc ;

--10.1
select r.rental_date, r.return_date,
age(r.return_date,r.rental_date),
c.first_name,c.last_name,c.email
from rental r join customer c on r.customer_id=c.customer_id
where r.return_date is not null and age(r.return_date,r.rental_date)>= interval '7 days'
order by age;

--10.2
select title, substr(title,15) from film where length(title)>=15;


