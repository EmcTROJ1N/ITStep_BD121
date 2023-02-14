using System.Collections.Generic;

class Garage
{
    List<Vehicle> Vehicles;
    
    public Garage() { Vehicles = new List<Vehicle>(); }
    public void Add (Vehicle car) { Vehicles.Add(car); }
    
    public void Print()
    {
        foreach (var car in Vehicles)
            car.Print();
    }

    public int GetPrice()
    {
        int allPrice = 0;
        foreach (var car in Vehicles)
            allPrice += car.Price;
        return allPrice;
    }

    public int GetWeight()
    {
        int allWeight = 0;
        foreach (var car in Vehicles)
            allWeight += car.Weight;
        return allWeight;
    }
}