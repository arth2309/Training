select * from customer;
select * from rental;

--12.1
select concat(c.first_name,' ',c.last_name) as customer_name, c.email, 
sum(p.amount) as "total rentals",
case
 when sum(p.amount)>=200 then 'Elite'
 when sum(p.amount)>=150 then 'platinum'
 when sum(p.amount)>=100 then 'Gold'
 when sum(p.amount)>=0 then 'silver'
end as "customer_category"
from payment p
join customer c
on p.customer_id=c.customer_id
group by c.customer_id
order by "total rentals" desc;


--12.2

create view customer_segments as
   select concat(c.first_name,' ',c.last_name) as customer_name, c.email, 
   sum(p.amount) as "total rentals",
   case
   when sum(p.amount)>=200 then 'Elite'
   when sum(p.amount)>=150 then 'platinum'
   when sum(p.amount)>=100 then 'Gold'
   when sum(p.amount)>=0 then 'silver'
  end as "customer_category"
  from payment p
  join customer c
  on p.customer_id=c.customer_id
  group by c.customer_id
  order by "total rentals" desc;
  
  select * from payment
