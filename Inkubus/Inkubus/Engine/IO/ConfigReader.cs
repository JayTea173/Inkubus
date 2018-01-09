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
        public static void Read(string filePath)
        {
            string[] config = File.ReadAllLines(@"..\data\config\" + filePath);
            
            foreach (string line in config)
            {
                line.Replace(" ",string.Empty);
            }
            foreach (string line in config)
            {   
                if (!line.StartsWith("//"))
                {
                    var assignment = line.Split(assignmentOperator);

                    if (assignment.Length > 1)
                    {
                        var expression = assignment[0].Split(nestingOperator);

                        var assembly = Assembly.GetExecutingAssembly();
                        var allTypes = assembly.GetTypes();
                        var type = assembly.GetType(expression[0]);
                        var field = type.GetField(expression[1], BindingFlags.Static);

                        TypeConverter typeConverter = TypeDescriptor.GetConverter(field);
                        object typeValue = typeConverter.ConvertFromString(assignment[1]);
                        field.SetValue(field.FieldType, typeValue);
                    }
                }
            }
        }
    }
}
