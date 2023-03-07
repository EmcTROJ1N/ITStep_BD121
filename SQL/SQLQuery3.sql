select * from titles
select * from authors
select * from titleauthor
select * from stores
select * from sales

--1 �������� ����� ������� �� ����� CA
select distinct title, state
from authors, titles, titleauthor
where state = 'CA'
and authors.au_id=titleauthor.au_id
and titles.title_id = titleauthor.title_id

--2 �������� ��������, ������� ������� ����� � ����� 'business' � ����� � ��������� �� $15 �� $25 +
select stor_name, price
from stores, titles, sales
where stores.stor_id = sales.stor_id 
and sales.title_id = titles.title_id
and type = 'business'
and price between 15 and 25 

--3 �������� �������, ������� �� ������ �����
select distinct au_lname, au_fname
from authors
except
select au_lname, au_fname
from authors, titles, titleauthor
where authors.au_id = titleauthor.au_id
and titles.title_id = titleauthor.title_id

--4 �������� ��������, ������� ������� ����� ������� �� ������ CA, UT, IN
select distinct stores.stor_name
from titleauthor, authors, stores, sales
where stores.stor_id = sales.stor_id
and sales.title_id = titleauthor.title_id
and authors.au_id = titleauthor.au_id
and authors.state  in ('CA', 'UT', 'IN')

--5 �������� ������� ���� ���� � ����� 'business' �������, ���������� �� ��������� +
select avg(price) titles
from titles, authors, titleauthor
where type = 'business' and contract = 1
and authors.au_id = titleauthor.au_id
and titleauthor.title_id = titleauthor.title_id 


--4 �������� ��������, ������� ������� ����� ������� �� ������ CA, UT, IN
select distinct stor_name, authors.state, au_lname, au_fname,stores.stor_name,sales.ord_num
from titleauthor, authors, stores, sales
where stores.stor_id = sales.stor_id
and sales.title_id = titleauthor.title_id
and authors.au_id = titleauthor.au_id
and authors.state  in ('CA', 'UT', 'IN')