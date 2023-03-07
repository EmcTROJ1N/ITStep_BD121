CREATE DATABASE planes;
go
USE planes;
CREATE TABLE planes (id INT, brand VARCHAR(255), model VARCHAR(255), seats INT, volume FLOAT, photo VARCHAR(255));

INSERT INTO planes (id, brand, model, seats, volume, photo) VALUES (1, 'Boeing', '737', 150, 50, 'C:\Users\19et7\Desktop\ADO.NET\CRUDPlanes\Sprites\boeing737.jpg');
INSERT INTO planes (id, brand, model, seats, volume, photo) VALUES (2, 'Airbus', 'A320', 180, 60,  'C:\Users\19et7\Desktop\ADO.NET\CRUDPlanes\Sprites\AirbusA320.jpg');
INSERT INTO planes (id, brand, model, seats, volume, photo) VALUES (3, 'Embraer', 'E190', 110, 40, 'C:\Users\19et7\Desktop\ADO.NET\CRUDPlanes\Sprites\EmbarerE90.jpg');
INSERT INTO planes (id, brand, model, seats, volume, photo) VALUES (4, 'Bombardier', 'CRJ700', 70, 20, 'C:\Users\19et7\Desktop\ADO.NET\CRUDPlanes\Sprites\BombardierCRJ700.jpg');
INSERT INTO planes (id, brand, model, seats, volume, photo) VALUES (5, 'Comac', 'C919', 168, 55, 'C:\Users\19et7\Desktop\ADO.NET\CRUDPlanes\Sprites\COMACÑ919.jpg');

select * from planes