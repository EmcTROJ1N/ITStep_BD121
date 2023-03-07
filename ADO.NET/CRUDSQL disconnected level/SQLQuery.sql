-- 2. ѕри помощи несв€зного уровн€ (SqlDataAdapter) реализовать систему хранени€ информации о животных (id, семейство, вид, возраст, масса)
-- реализовать CRUD (insert, update, delete, read)
-- использовать WPF или Windows Forms

create database AnimalsDB
go
use AnimalsDB
create table Animals
(id int primary key, family varchar(100), species varchar(100), yearsOld int, mass int)

insert into Animals (id, family, species, yearsOld, mass) values
(1, 'feline', 'Siamese', 5, 10)

insert into Animals (id, family, species, yearsOld, mass) values
(2, 'spdiers', 'tarantulf', 1, 1)

select * from Animals