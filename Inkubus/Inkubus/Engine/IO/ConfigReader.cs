using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace Inkubus.Engine.IO
{
    static class ConfigReader
    {
        static readonly char[] assignmentOperator = new char[] { '=' };
        static readonly char[] nestingOperator = new char[] { '.' };
        static void Reader()
        {
            string[] config = File.ReadAllLines(@"C:\Inkubus\Inkubus\Inkubus\data\config.txt");
            
            foreach (string line in config)
            {
                line.Replace(" ",string.Empty);
            }
            foreach (string line in config)
            {   
                if (!((line[0] == '/') && (line[1] == '/')))
                {
                    var assignment = line.Split(assignmentOperator);
                    var expression = assignment[0].Split(nestingOperator);

                    var type = Assembly.GetExecutingAssembly().GetType(expression[0]).GetField(expression[1], BindingFlags.Static);

                    TypeConverter typeConverter = TypeDescriptor.GetConverter(type);
                    object typeValue = typeConverter.ConvertFromString(assignment[1]);
                    type.SetValue(type, typeValue);
                }
            }
        }
    }
}
