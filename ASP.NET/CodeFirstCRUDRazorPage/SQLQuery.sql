create table Owners
(owner_id int primary key, fullname varchar(50))

create table Auto
(owner_id int, car_id int primary key, Model varchar(20), MaxSpeed int)

drop table Owners
drop table Auto

select * from Owners
select * from Auto

alter table Auto add constraint rel1 foreign key (owner_id) references Owners (owner_id)
ON DELETE CASCADE ON UPDATE CASCADE;

insert into Owners (owner_id, fullname) values
(1, 'Anton Trojanov')

insert into Auto (owner_id, car_id, Model, MaxSpeed) values
(1, 123, 'Porsche', 100)

update Auto
set MaxSpeed = 250, Model = 'BMW X5', owner_id = 666
where car_id = 123

update Owners
set fullname = 'Putin'
where owner_id = 1