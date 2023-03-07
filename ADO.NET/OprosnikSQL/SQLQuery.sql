create database Oprosnik
go
-- Create the `questions` table
CREATE TABLE questions (
    question_id INT PRIMARY KEY,
    question_text TEXT NOT NULL
);

-- Create the `answers` table
CREATE TABLE answers (
    answer_id INT PRIMARY KEY,
    question_id INT NOT NULL,
    answer_text TEXT NOT NULL,
    points INT NOT NULL,
    FOREIGN KEY (question_id) REFERENCES questions(question_id)
);

INSERT INTO questions (question_id, question_text)
VALUES
    (1, 'Какой из перечисленных городов является столицей России?'),
    (2, 'Какое языком программирования самым популярным?'),
    (3, 'Какой из перечисленных видов спорта является летним?'),
    (4, 'Какая из перечисленных планет является самой близкой к Солнцу?'),
    (5, 'Какой из перечисленных цветов является основным цветом флага России?');

-- Insert sample data into the `answers` table
INSERT INTO answers (answer_id, question_id, answer_text, points)
VALUES
    (1, 1, 'Москва', 5),
    (2, 1, 'Петербург', 0),
    (3, 1, 'Екатеринбург', 0),
    (4, 2, 'Python', 5),
    (5, 2, 'Java', 3),
    (6, 2, 'C++', 4),
    (7, 3, 'Летняя гимнастика', 5),
    (8, 3, 'Летний бейсбол', 0),
    (9, 4, 'Меркурий', 5),
    (10, 4, 'Венера', 0),
    (11, 5, 'Красный', 5),
    (12, 5, 'Зеленый', 0),
    (13, 5, 'Желтый', 0);