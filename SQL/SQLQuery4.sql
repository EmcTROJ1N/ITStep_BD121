select * from titles
select * from authors
select * from titleauthor
select * from stores
select * from sales
select * from titles



-- 1 �������� �������, ������� ������ ������ ����� ���� � ����� 'business'
select top 1 stor_name
from stores, titles, sales
where stores.stor_id = sales.stor_id
and sales.title_id = titles.title_id
and type = 'business'
group by stor_name
order by sum(qty) desc

-- 2 �������� �������, ������� �������� ����� � ����� 'business'
select distinct au_lname, au_fname, title
from authors, titles, titleauthor
where authors.au_id = titleauthor.au_id
and titleauthor.title_id = titles.title_id
and type =  'business'

-- 3 �������� ��������, ������� �� ������� �����
select stor_name
from stores, titles, sales
except
select stor_name
from stores, titles, sales
where titles.title_id = sales.title_id
and sales.stor_id = stores.stor_id

-- 4 �������� �������, ������� �������� �����, �������������� �����
select distinct au_lname, au_fname, month(pubdate), title
from authors, titles, titleauthor, publishers
where authors.au_id = titleauthor.au_id
and titles.title_id = titleauthor.title_id
and titles.pub_id = publishers.pub_id
and month(pubdate) in (6, 7, 8)


-- 5 �������� ���� ����, � ������� �������� ����� ������� �����
select top 1 type, max(price) 
from titles
group by type
order by max(price) desc