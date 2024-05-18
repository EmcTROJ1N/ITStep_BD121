import os
import glob
import threading

# Функция для сканирования папки и поиска файлов по заданным типам
def search_files(path, extensions, report_file):
    with open(report_file, 'a') as report:
        for root, dirs, files in os.walk(path):
            for file in files:
                if file.endswith(tuple(extensions)):
                    file_path = os.path.join(root, file)
                    print(file_path)
                    report.write(file_path + '\n')


# Чтение конфигурационного файла
with open('config.txt', 'r') as config_file:
    extensions = [line.strip() for line in config_file]

path = input("Enter full folder path: ")
report_file = 'report.txt'

# Создание отдельного потока для поиска файлов
search_thread = threading.Thread(target=search_files, args=(path, extensions, report_file))
search_thread.start()
