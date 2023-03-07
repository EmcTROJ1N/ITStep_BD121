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
    (1, '����� �� ������������� ������� �������� �������� ������?'),
    (2, '����� ������ ���������������� ����� ����������?'),
    (3, '����� �� ������������� ����� ������ �������� ������?'),
    (4, '����� �� ������������� ������ �������� ����� ������� � ������?'),
    (5, '����� �� ������������� ������ �������� �������� ������ ����� ������?');

-- Insert sample data into the `answers` table
INSERT INTO answers (answer_id, question_id, answer_text, points)
VALUES
    (1, 1, '������', 5),
    (2, 1, '���������', 0),
    (3, 1, '������������', 0),
    (4, 2, 'Python', 5),
    (5, 2, 'Java', 3),
    (6, 2, 'C++', 4),
    (7, 3, '������ ����������', 5),
    (8, 3, '������ �������', 0),
    (9, 4, '��������', 5),
    (10, 4, '������', 0),
    (11, 5, '�������', 5),
    (12, 5, '�������', 0),
    (13, 5, '������', 0);