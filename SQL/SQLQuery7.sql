-- 1) instead of триггер при удалении автора удаляет все его книги

alter trigger deleteBooksWithAuthor
on authors instead of delete
as
begin
	delete from titles
	where titles.title_id in
	(select title_id from titleauthor
	where au_id in (select au_id from deleted))

	delete from titleauthor
	where au_id in (select au_id from deleted)

	delete from authors
	where au_id in (select au_id from deleted)
end

select * from authors
select * from titleauthor
select * from titles

delete from authors
where au_id = '172-32-6666'

alter table titleauthor drop constraint FK__titleauth__title__060DEAE8

insert into authors (au_id, au_fname, au_lname, phone, address, city, state, zip, contract) values
('172-32-6666', 'German', 'Pokrovskiy', '123 123-123', 'baker st 221b', 'Donetsk', 'CA', '94025', 1)

insert into titleauthor (title_id, au_id)
values
('CU1032', '172-32-6666')

-- 2) constraint запрещает добавлять авторов, если в имени или фамилии нет гласных или они короче 2 символов

alter table authors add constraint constr1 check 
(len([au_lname]) >= 2 and [au_lname] LIKE '%[aeoiuy]%' and 
len([au_fname]) >= 2 and [au_fname] LIKE '%[aeoiuy]%')

alter table authors drop constraint constr1

-- 3) constraint запрещает добавлять книги с отрицательной ценой или если в названии книги больше 5 слов

alter table titles add constraint constr1 check ([price] < 0 or len([price]) < 5)