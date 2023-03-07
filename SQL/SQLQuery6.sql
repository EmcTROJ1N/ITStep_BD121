-- 1. instead of триггер запрещает добавлять книги в жанре business, если их цена ниже $10

create trigger insteadOfUpperTen
on titles instead of insert
as
begin
	insert into titles
	select * from inserted
	except
	select * from inserted
	where type = 'business'
	and price < 10
end
drop trigger insteadOfUpperTen

-- 2. instead of триггер запрещает удалять книги в жанре business

create trigger insteadOfDeleteBusiness
on titles instead of delete
as
begin
    delete from titles
    select * from deleted
    where type != 'business'
end

drop trigger insteadOfDeleteBusiness


-- 3. instead of триггер запрещает снижать цену книг

create trigger insteadOfUpdateLowerPrice
on titles instead of update
as
begin
    update titles
    set titles.price = inserted.price
    from titles, inserted, deleted
    where inserted.title_id = deleted.title_id
    and titles.title_id = inserted.title_id
    and inserted.price >= deleted.price
end

update titles
set price = price + 1

select * from titles