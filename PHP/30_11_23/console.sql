-- показать авторов самой дорогой книги
select * from authors
where au_id in
    (select au_id from titleauthor
    where title_id =
        (select title_id from titles
         order by price desc
         limit 1));

-- увеличить цену самой дешёвой книги на 30%

update titles
set price = price * 1.3
where title_id = (select title_id from titles
                order by price asc
                limit 1);

-- хранимая процедура принимает название штата и сумму денег. Она увеличивает стоимость книг авторов указанного штата на указанную сумму денег

select au_id from authors where state = 'CA';

DELIMITER //

CREATE PROCEDURE UpdateSell(IN state INT, IN sum INT)
BEGIN
    update titles
    set price = price + sum
    where title_id in
    (select title_id from titleauthor
    where au_id in (select au_id from authors where authors.state = state));
END //

DELIMITER ;

-- хранимая процедура удаляет авторов, которые не пишут книги

DELIMITER //

CREATE PROCEDURE DeleteTrashAu(IN state INT, IN sum INT)
BEGIN
    delete from authors
    where au_id in
    (select au_id from authors
    except
    select au_id from titleauthor);
END //

DELIMITER ;