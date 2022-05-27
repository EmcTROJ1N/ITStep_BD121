all: output

output: utils.o main.o tasks.o
	g++ utils.o main.o -o output

utils.o: utils.cpp
	g++ -c utils.cpp

main.o: main.cpp
	g++ -c main.cpp

clean:
	rm *.o
