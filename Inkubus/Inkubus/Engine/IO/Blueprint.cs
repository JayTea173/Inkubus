using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;


namespace Inkubus.Engine.IO
{
    interface IBlueprintable<T> where T : Object
    {

    }

    class Blueprint
    {
        struct BPDSerializationInfo
        {
            public BPDAttribute attrib;
            private FieldInfo field;
            private PropertyInfo prop;
            private bool isProperty;

            public BPDSerializationInfo(BPDAttribute _attrib, FieldInfo _field)
            {
                attrib = _attrib;
                field = _field;
                prop = null;
                isProperty = false;
            }

            public BPDSerializationInfo(BPDAttribute _attrib, PropertyInfo _prop)
            {
                attrib = _attrib;
                field = null;
                prop = _prop;
                isProperty = true;
            }

            public string Name
            {
                get
                {
                    return (isProperty) ? prop.Name : field.Name;
                }
            }


            public object GetValue(object parent)
            {
                if (isProperty)
                    return prop.GetValue(parent);
                else
                    return field.GetValue(parent);
            }

            private void SetValue(object parent, object value)
            {
                if (isProperty)
                    prop.SetValue(parent, value);
                else
                    field.SetValue(parent, value);
            }

            public void Write(object o, BinaryWriter bw)
            {
                object v = GetValue(o);
                Type t = (isProperty) ? prop.PropertyType : field.FieldType;
                if (t == typeof(bool))
                    bw.Write((bool)Convert.ChangeType(v, t));
                else if (t == typeof(byte))
                    bw.Write((byte)Convert.ChangeType(v, t));
                else if (t == typeof(char))
                    bw.Write((char)Convert.ChangeType(v, t));
                else if (t == typeof(short))
                    bw.Write((short)Convert.ChangeType(v, t));
                else if (t == typeof(ushort))
                    bw.Write((ushort)Convert.ChangeType(v, t));
                else if (t == typeof(int))
                    bw.Write((int)Convert.ChangeType(v, t));
                else if (t == typeof(uint))
                    bw.Write((uint)Convert.ChangeType(v, t));
                else if (t == typeof(long))
                    bw.Write((long)Convert.ChangeType(v, t));
                else if (t == typeof(ulong))
                    bw.Write((ulong)Convert.ChangeType(v, t));
                else if (t == typeof(float))
                    bw.Write((float)v);
                else if (t == typeof(double))
                    bw.Write((double)Convert.ChangeType(v, t));
                else if (t == typeof(string))
                    bw.Write((string)Convert.ChangeType(v, t));
                else
                    throw new System.Exception("Unable to serialize " + t.Name + " values.");
            }

            public void Read(object o, BinaryReader bw)
            {
                object v = GetValue(o);
                Type t = (isProperty) ? prop.PropertyType : field.FieldType;
                if (t == typeof(bool))
                    SetValue(o, bw.ReadBoolean());
                else if (t == typeof(byte))
                    SetValue(o, bw.ReadByte());
                else if (t == typeof(char))
                    SetValue(o, bw.ReadChar());
                else if (t == typeof(short))
                    SetValue(o, bw.ReadInt16());
                else if (t == typeof(ushort))
                    SetValue(o, bw.ReadUInt16());
                else if (t == typeof(int))
                    SetValue(o, bw.ReadInt32());
                else if (t == typeof(uint))
                    SetValue(o, bw.ReadUInt32());
                else if (t == typeof(long))
                    SetValue(o, bw.ReadInt64());
                else if (t == typeof(ulong))
                    SetValue(o, bw.ReadUInt64());
                else if (t == typeof(float))
                    SetValue(o, bw.ReadSingle());
                else if (t == typeof(double))
                    SetValue(o, bw.ReadDouble());
                else if (t == typeof(string))
                    SetValue(o, bw.ReadString());
                else
                    throw new System.Exception("Unable to deserialize " + t.Name + " values.");
            }


        }

        private static List<BPDSerializationInfo> GetListedSerializationData(Type t)
        {
            List<BPDSerializationInfo> sinfo = new List<BPDSerializationInfo>();

            var fields = t.GetFields().Where(prop => prop.IsDefined(typeof(BPDAttribute), false));

            foreach (var field in fields)
                sinfo.Add(new BPDSerializationInfo(((BPDAttribute[])field.GetCustomAttributes(typeof(BPDAttribute), false))[0], field));

            var props = t.GetProperties().Where(prop => prop.IsDefined(typeof(BPDAttribute), false));

            foreach (var prop in props)
                sinfo.Add(new BPDSerializationInfo(((BPDAttribute[])prop.GetCustomAttributes(typeof(BPDAttribute), false))[0], prop));

            sinfo.Sort(delegate (BPDSerializationInfo a, BPDSerializationInfo b)
            {
                return a.attrib.position.CompareTo(b.attrib.position);
            });

            return sinfo;
        }

        public static void Write<T>(T o, string fileName) where T : Object
        {
            Type t = o.GetType();
            var sinfo = GetListedSerializationData(t);

            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            foreach (var s in sinfo)
            {
                s.Write(o, bw);
                System.Diagnostics.Debug.WriteLine("Serializing: " + s.Name + " = " + s.GetValue(o));
            }

            bw.Close();
            fs.Close();
        }

        public static void Read<T>(ref T o, string fileName) where T : Object
        {
            Type t = o.GetType();
            var sinfo = GetListedSerializationData(t);

            FileStream fs = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            foreach (var s in sinfo)
            {
                s.Read(o, br);
                System.Diagnostics.Debug.WriteLine("Deserializing: " + s.Name + " = " + s.GetValue(o));
            }

            br.Close();
            fs.Close();
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BPDAttribute : Attribute
    {
        public int position;

        public BPDAttribute(int _position)
        {
            position = _position;
        }

        public BPDAttribute()
        {
            position = 0;
        }
    }
}
