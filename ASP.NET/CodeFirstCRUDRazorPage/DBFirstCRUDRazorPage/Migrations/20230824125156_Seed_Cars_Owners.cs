using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBFirstCRUDRazorPage.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Cars_Owners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO OwnersTable (fullname) VALUES
('Anton Trojanov'),
('Ivan Ivanov'),
('Petr Petrov'),
('Svetlana Ivanova'),
('Dmitry Sidorov'),
('Anna Petrova'),
('Oleg Kuznetsov'),
('Maria Ivanova'),
('Sergey Romanov'),
('Elena Sidorova');
    ");
            migrationBuilder.Sql(@"
        INSERT INTO CarsTable (owner_id, car_id, Model, MaxSpeed) VALUES
(1, 123, 'Porsche', 100),
(2, 456, 'Mercedes-Benz', 100),
(3, 789, 'BMW', 100),
(4, 1011, 'Audi', 100),
(5, 1213, 'Volkswagen', 100),
(6, 1415, 'Toyota', 100),
(7, 1617, 'Honda', 100),
(8, 1819, 'Nissan', 100),
(9, 2021, 'Mazda', 100),
(10, 2223, 'Suzuki', 100);

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
