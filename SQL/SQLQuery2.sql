use pubs

-- год публикации самой книги
select top(1) datepart(yy, pubdate) from titles
order by price desc

select datediff(yy, min(pubdate), max(pubdate)) from titles

select dateadd(mm, 2, pubdate) from titles
where type = 'business'

select au_fname, au_lname, state, contract into ttable from authors
where state = 'CA' and contract = 1
select * from ttable

drop table ttable

insert into ttable
select au_fname, au_lname, state, contract from authors
where state = 'KS'