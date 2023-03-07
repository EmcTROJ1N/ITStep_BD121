CREATE TABLE Cars (
  id INT PRIMARY KEY,
  brand VARCHAR(200),
  model VARCHAR(200),
  speed INT,
  price INT,
  year INT,
  photo VARCHAR(200)
);

INSERT INTO Cars (id, brand, model, speed, price, year, photo)
VALUES
  (1, 'Toyota', 'Camry', 180, 25000, 2018, 'C:\Users\19et7\Desktop\ADO.NET\CRUDEntityFrameworkCars\Cars\camry.png'),
  (2, 'Honda', 'Civic', 160, 22000, 2020, 'C:\Users\19et7\Desktop\ADO.NET\CRUDEntityFrameworkCars\Cars\honda.jpg'),
  (3, 'Tesla', 'Model S', 250, 100000, 2019, 'C:\Users\19et7\Desktop\ADO.NET\CRUDEntityFrameworkCars\Cars\nissan.jpg'),
  (4, 'Nissan', 'Altima', 190, 28000, 2017, 'C:\Users\19et7\Desktop\ADO.NET\CRUDEntityFrameworkCars\Cars\tesla.avif')