select * from publishers
select * from titles
select * from stores
select * from sales

-- 1 показать магазин, который продал больше всего книг в жанре 'business'
select stor_name
from stores 
where stor_id in
(select top 1 stor_id from sales where title_id in
(select title_id from titles where type =  'business')
group by stor_id
order by sum(qty) desc)

select * from stores where stor_id in
(select stor_id from sales where title_id in
(select sum(qty) from sales where title_id  in
(select title_id from titles where type =  'business')))

-- 2 показать авторов, которые написали книги в жанре 'business'
select * from authors
where au_id in
(select au_id from titleauthor where title_id in
(select title_id from titles where type = 'business'))

-- 3 показать магазины, которые не продают книги
select * from stores
except
select * from stores
where stor_id in
(select stor_id from sales where stor_id != null)

-- 4 показать авторов, которые написали книги, опубликованные летом
select * from authors
where au_id in (
select au_id from titleauthor
where title_id in (
select title_id from titles 
where month(pubdate) in (6, 7, 8)))


-- 5 показать жанр книг, в котором написана самая дорогая книга
select type from titles where price =
(select max(price) from titles)