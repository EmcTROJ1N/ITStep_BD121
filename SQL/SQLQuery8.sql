--1) создать таблицу имеющую следующие поля (type, cnt, avg_price)
--										  (название жанра, количество книг в жанре, средняя цена книги в жанре)
--		
create table table1 (type varchar(20), cnt int, avg_price int)

insert into table1 (type, cnt, avg_price)
select type, (select count(*) from titles where t.type = type), (select avg(price) from titles where type = t.type) from titles t
group by type

select * from table1
drop table table1

--2) создать триггеры, которые при пересчитывают данные в сводной таблице по жанрам в следующих случаях:
-- добавление книги
-- удаление книги
-- изменение цены книги
-- добавление книги

create table report (id int identity primary key, type varchar(20), cnt int, avg_price money)

insert into titles (title_id, title, price, type)
values
('TC7711', 'Skazki 3', 10, 'fairy tales'),
('TC7788', 'Greate Poem', 2, 'UNDECIDED')

delete from titles
where title_id = 'TC7711' or title_id = 'TC7788'

create trigger add_books
on titles after insert
as
begin
    print 'Add book!!!'
    update report
    set cnt = (select count(*) from titles where type = ins.type),
    avg_price = (select avg(price) from titles where type = ins.type)
    from report, inserted ins
    where report.type = ins.type
end

drop trigger add_books

--удаление книги
create trigger del_books
on titles for delete
as
begin
    print 'Deleted book!!!'
    update report
    set cnt = (select count(*) from titles where type = del.type),
    avg_price = (select avg(price) from titles where type = del.type)
    from report, deleted del
    where report.type = del.type
end

--3) 
--Показать книги, у которых среднее количество продаж (Sales) выше среднего по жанру

select * from titles t
where title_id in
(select title_id from sales
group by title_id
having avg(qty) > 
(select avg(qty) from sales, titles
where sales.title_id = titles.title_id and
titles.type = t.type))

select * from sales

select title from titles,sales where titles.title_id in
(select sales.title_id from sales where avg(count(*)) > (select avg(count(*)) from sales group by type))

--4) Показать название штата,
--	количество авторов из этого штата,
--	количество книг, написанных авторами из этого штата
--	минимальную и максимальную ценых книг авторов из этого штата

select state,
(select count(*) from authors where state = a.state)
[Count authors from state],
(select count(*) from titles where title_id in (select title_id from titleauthor where au_id in (select au_id from authors where state = a.state))) 
[Count books from state],
(select min(price) from titles where title_id in (select title_id from titleauthor where au_id in (select au_id from authors where state = a.state)))
[Min price],
(select max(price) from titles where title_id in (select title_id from titleauthor where au_id in (select au_id from authors where state = a.state)))
[Max Price]
from titles, authors a
group by state