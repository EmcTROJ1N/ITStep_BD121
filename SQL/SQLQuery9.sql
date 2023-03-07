-- Курсор добавляет в название книги вконце цену в круглых скобках - "Skazki (23.56)"

DECLARE task1 CURSOR dynamic FOR
SELECT title, price FROM titles

OPEN task1

DECLARE @title varchar(200), @price float

FETCH first FROM task1 INTO @title, @price

WHILE @@FETCH_STATUS = 0
BEGIN
    update titles set title = CONCAT(@title, ' (', @price, ')')
    where current of task1

    FETCH NEXT FROM task1 INTO @title, @price
END

close task1
deallocate task1

select * from titles

-- Курсор удаляет в названии книги вконце цену в круглых скобках

DECLARE task2 CURSOR dynamic FOR
SELECT title, price FROM titles

OPEN task2

DECLARE @title varchar(200), @price float

FETCH first FROM task2 INTO @title, @price

WHILE @@FETCH_STATUS = 0
BEGIN
	declare @str varchar(200)
	set @str = RIGHT(@title, len(CONCAT(' (', @price, ')')))

	if @str = CONCAT(' (', @price, ')') 
		update titles set title = REPLACE(@title, @str, '')
		where current of task2

    FETCH NEXT FROM task2 INTO @title, @price
END

close task2
deallocate task2

select * from titles

-- Курсор подсчитывает количество магазинов, продающих определённую книгу  и в конце находит среднее количество магазинов для всех книг

select * from titles
select * from sales
order by title_id asc

begin tran

DECLARE task3 CURSOR dynamic FOR
SELECT title_id, stor_id FROM sales
order by title_id desc

OPEN task3

declare @countShops int
set @countShops = 0

DECLARE @title_id varchar(200), @stor_id varchar(200)

FETCH first FROM task3 INTO @title_id, @stor_id
FETCH next FROM task3 INTO @title_id, @stor_id

WHILE @@FETCH_STATUS = 0
BEGIN
	declare @count int
	set @count = (select count(*) from sales where title_id = @title_id)

	if @count = 1
		set @countShops = @countShops + 1
	else
	begin
		DECLARE @oldtitle_id varchar(200), @oldstor_id varchar(200)
		FETCH PRIOR FROM task3 INTO @oldtitle_id, @oldstor_id
		if @oldtitle_id != @title_id
			set @countShops = @countShops + @count
		FETCH NEXT FROM task3 INTO @oldtitle_id, @oldstor_id
	end
	
	FETCH NEXT FROM task3 INTO @title_id, @stor_id
END

select @countShops / (select count(*) from titles)

close task3
deallocate task3