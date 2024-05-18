import random

class Interval:
    def __init__(self, start, end):

        if(start<end):
            self.start = start
            self.end = end
        else :
            self.start = end
            self.end = start

    def __str__(self):
        return f"[{self.start}, {self.end}]"
    
    def length():    
        return self.end - self.start

class IntervalCollection:
    
    def __init__(self):
        self.intervals = []

    def __del__(self):
        del self.intervals

    def Add(self, interval):
        self.intervals.append(interval)

    def RemoveByPos(self, pos):
        if pos < len(self.intervals):
            del self.intervals[pos]

    def RemoveByValue(self, value):
        self.intervals = [interval for interval in self.intervals if interval.start >= value]


    def RemoveByLength(self, length):
        self.intervals = [interval for interval in self.intervals if interval.length() >= length]

    def Count(self):
        return len(self.intervals)

    def GetLongest(self):
        return max(self.intervals, key=lambda x: x.end - x.start)

    def GetShortest(self):
        return min(self.intervals, key=lambda x: x.end - x.start)

    def hasHoles(self):
        sorted_intervals = sorted(self.intervals, key=lambda x: x.start)
        for i in range(1, len(sorted_intervals)):
            if sorted_intervals[i-1].end < sorted_intervals[i].start:
                return True
        return False
    
    def hasHolesValue(self):
        sorted_intervals = sorted(self.intervals, key=lambda x: x.start)
        for i in range(1, len(sorted_intervals)):
            if sorted_intervals[i-1].end < sorted_intervals[i].start:
                return Interval(sorted_intervals[i].start, sorted_intervals[i-1].end)
        return Interval(0,0)
        
    def GetSortIntervals(self):
        sorted_intervals = sorted(self.intervals, key=lambda x: x.start)
        # for i in range(0, len(sorted_intervals)):
        #     print(i, sorted_intervals[i]) 
        return sorted_intervals

    def GetStart(self):
        return self.intervals[0].start if self.intervals else None

    def GetEnd(self):
        return self.intervals[-1].end if self.intervals else None

    def Save(self, file_name):
        with open(file_name, 'w') as file:
            for interval in self.intervals:
                file.write(f"{interval.start} {interval.end}\n")

    def Load(self, file_name):
        with open(file_name, 'r') as file:
            for line in file:
                start, end = map(int, line.split())
                self.intervals.append(Interval(start, end))

    def __iter__(self):
        return iter(self.intervals)

    def __str__(self):
        return ', '.join(str(interval) for interval in self.intervals)

collection = IntervalCollection()


for _ in range(20):
    interval_start = random.randint(1, 100)
    # interval_end = random.randint(interval_start, 100)
    interval_end = random.randint(1, 100)

    collection.Add(Interval(interval_start, interval_end))


# collection.Add(Interval(1, 5))
# collection.Add(Interval(3, 8))
# collection.Add(Interval(6, 10))

print(collection)
print("Count:", collection.Count())
print("Longest Interval:", collection.GetLongest())
print("Shortest Interval:", collection.GetShortest())

collection2 = IntervalCollection()

collection2.intervals = collection.GetSortIntervals()
print("Sort collection 2", collection2)

print("Has Holes:", collection.hasHoles())
print("Value Holes:", collection.hasHolesValue())

print("Start of First Interval:", collection.GetStart())
print("End of Last Interval:", collection.GetEnd())

collection.RemoveByValue(4)
print("After removing intervals with start less than 4:")
print(collection)

collection.Save("intervals.txt")

new_collection = IntervalCollection()
new_collection.Load("intervals.txt")
print("Loaded from file:")
print(new_collection)