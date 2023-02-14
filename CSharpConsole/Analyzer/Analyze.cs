using System;
using System.Reflection;
class Analyzer
{
    public Analyzer()
    {

    }

    public void Analyze(object? obj, Random rand)
    {
        Type objInf = obj.GetType();
        MethodInfo[] methods = objInf.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (MethodInfo method in methods)
        {
            ParameterInfo[] _params = method.GetParameters();

            if (!Array.Exists(_params, param => (param.ParameterType != typeof(int)) && param.ParameterType != typeof(double) && param.ParameterType != typeof(bool)))
            {
                object[] args = new object[_params.Length];
                for (int i = 0; i < _params.Length; i++)
                {
                    switch (_params[i].ParameterType.ToString())
                    {
                        case "System.Int32":
                        {
                            args[i] = rand.Next(0, 100);
                            break;
                        }
                        case "System.Double":
                        {
                            args[i] = rand.NextDouble();
                            break;
                        }
                        case "System.Boolean":
                        {
                            args[i] = rand.Next(0, 2) == 0 ? false : true;
                            break;
                        }
                    }
                }
                method.Invoke(obj, args);
            }
        }
    }
}