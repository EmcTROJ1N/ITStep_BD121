create database CarsBase
go
use CarsBase

create table Cars
(id int primary key identity, Model varchar(20), Owner varchar(20), MaxSpeed int)

insert into Cars
(Model, Owner, MaxSpeed)
values
('BMW M5', 'Anton', 200)

select * from Cars

delete table Cars
drop table Cars


use pubs

select * from authors
where state = 'CA' and contract = 1

select * from titles
where type = 'business' and price < 20

select * from authors
order by contract, au_fname desc

select * from titles
where price between 5 and 15 and type='Psychology'

select max(price) - min(price) from titles