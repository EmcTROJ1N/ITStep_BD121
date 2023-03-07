create table NewTable(id int primary key, fname varchar(20), lname varchar(20))
drop table NewTable

alter table NewTable
add address varchar(20)	NULL

select * from NewTable

-- показать авторов, которые написали книги в жанре 'business'
select distinct au_lname, au_fname, title
from authors, titles, titleauthor
where authors.au_id = titleauthor.au_id
and titleauthor.title_id = titles.title_id
and type =  'business'

-- жанр, в котором написана самая дорогая книга
select top 1 type, max(price) 
from titles
group by type
order by max(price) desc

-- показать авторов, которые написали книги в жанре 'business'
select * from authors
where au_id in
(select au_id from titleauthor where title_id in
(select title_id from titles where type = 'business'))

-- показать авторов, которые написали книги, опубликованные летом
select * from authors
where au_id in (
select au_id from titleauthor
where title_id in (
select title_id from titles 
where month(pubdate) in (6, 7, 8)))

-- показать магазин, который продал больше всего книг в жанре 'business'
select stor_name
from stores 
where stor_id in
(select top 1 stor_id from sales where title_id in
(select title_id from titles where type =  'business')
group by stor_id
order by sum(qty) desc)

-- показать количество букв в имени и фамилии писателя
select au_lname, len(au_lname) [Count Letters], au_fname, len(au_fname) [Count Letters] from authors

-- переводит имена в верхний регистр, фамилии в нижний
select upper(au_lname), lower(au_fname) from authors

-- показывает сколько лет назад были изданы все эти книги
select title, datediff(year, pubdate, GETDATE()) [Years ago] from titles

-- добавляет к дате публикации книг один год (или отнимает, как пожелаете)
update titles
set pubdate = dateadd(year, -1, pubdate)

-- select into
select *
into myTitles
from titles

select * from myTitles
drop table myTitles

-- view 1
create view GetAllAuthorsInCA (fname, lname) as
select au_fname, au_lname from authors
where state = 'CA'

select * from GetAllAuthorsInCA
drop view GetAllAuthorsInCA

-- view 2
create view GetStorNamesWithBusinessBooks (stor_name) as
select stor_name
from stores 
where stor_id in
(select top 1 stor_id from sales where title_id in
(select title_id from titles where type =  'business'))

select * from GetStorNamesWithBusinessBooks
drop view GetStorNamesWithBusinessBooks

-- join
create table myTable(id varchar(20) primary key)
drop table myTable

select * from titleauthor
join myTable on titleauthor.au_id = myTable.id

-- хранимая фунция принимает строку и подсчитывает в ней количество гласных букв
create function ReplaceVowerlsToOne(@str varchar(20))
returns varchar(20)
as
begin
    declare @resStr varchar(20), @i int = 1
	set @resStr = ''

    while(@i <= len(@str))
    begin
        if(SUBSTRING(@str, @i, 1) in ('a', 'o', 'i', 'e', 'u', 'y'))
            set @resStr = concat(@resStr, 1)
		else
			set @resStr = concat(@resStr, SUBSTRING(@str, @i, 1))
        set @i = @i + 1
    end
    return @resStr
end

select dbo.ReplaceVowerlsToOne('barabaka')

-- хранимая функция 2
create function reverseStr(@str varchar(20))
returns varchar(20)
as
begin
    declare @resStr varchar(20), @i int = len(@str)
	set @resStr = ''

    while(@i >= 1)
    begin
		set @resStr = concat(@resStr, SUBSTRING(@str, @i, 1))
        set @i = @i - 1
    end
    return @resStr
end

select dbo.reverseStr('hello world')

-- процедура 1

-- показать магазин, который продал больше всего книг в жанре 'business'
create procedure GetMaxPricesBusiness(@stor varchar(20) out)
as
begin
	select @stor = stor_name
	from stores 
	where stor_id in
	(select top 1 stor_id from sales where title_id in
	(select title_id from titles where type =  'business')
	group by stor_id
	order by sum(qty) desc)
end

declare @stor varchar(20)
exec GetMaxPricesBusiness @stor out
select 'this stor is ' + @stor

-- процедура 2



-- показать писателей жанра бизнесс
create procedure ShowBusinessAuthors
as
begin
	select * from authors
	where au_id in
	(select au_id from titleauthor where title_id in
	(select title_id from titles where type = 'business'))
end

exec ShowBusinessAuthors


-- union возвращает всех авторов из СА и ЮТ
select * from authors
where state = 'CA'
union
select * from authors
where state = 'UT'

-- interset писатели которые писали в жанре бизнес и родом из CA
select * from authors
where state = 'CA'
intersect
select * from authors
where au_id in
(select au_id from titleauthor
where title_id in 
(select title_id from titles
where type = 'business'))


-- except показать магазины, которые не продают книги
select * from stores
except
select * from stores
where stor_id in
(select stor_id from sales where stor_id != null)

--1. Показать жанр, в котором написано больше всего книг
select top(1) type from titles
group by type
order by count(type) desc

--2. Показать самую дорогую книгу в жанре "business"
select top(1) title from titles
where type = 'business'
group by title
order by max(price) desc

--3. Увеличить цену всех книг на 2 процента (update)
update titles
set price += 0.02 * price

--4. Увеличить цену самых дешёвых книг на 10 процентов
update titles
set price += 0.10 * price
where price = (select(min(price)) from titles)

-- 5. Хранимая процедура принимает название издательства и удаляет его и все изданные им книги

-- 6. Хранимая функция принимает строку и возвращает количество цифер в ней

alter function countNums(@str varchar(20))
returns int
as
begin
    declare @count int = 0, @i int = 1

    while(@i <= len(@str))
    begin
		if (SUBSTRING(@str, @i, 1) in ('1', '2', '3', '4', '5', '6', '7', '8', '9', '0'))
			set @count = @count + 1
        set @i = @i + 1
    end
    return @count
end

select dbo.countNums('1234hello')

-- 7. Показать жанр, в котором работает больше всего писателей (view)

-- 8. Показать магазин, заработавший больше всего денег за все годы