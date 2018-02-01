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
            private int metadataDepth;


            public BPDSerializationInfo(BPDAttribute _attrib, FieldInfo _field, int _metadataDepth = 0)
            {
                attrib = _attrib;
                field = _field;
                prop = null;
                isProperty = false;
                metadataDepth = _metadataDepth;
            }

            public BPDSerializationInfo(BPDAttribute _attrib, PropertyInfo _prop, int _metadataDepth = 0)
            {
                attrib = _attrib;
                field = null;
                prop = _prop;
                isProperty = true;
                metadataDepth = _metadataDepth;
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



            public object GetParentAtMetadataDepth(Object david)
            {
                if (metadataDepth == 0)
                    return david;
                return Metadata.GetOrCreate(david);
            }

            public void Write(Object o, BinaryWriter bw)
            {
                object v = GetValue(GetParentAtMetadataDepth(o)); ;
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
                {
                    if (v == null)
                        bw.Write(string.Empty);
                    else
                        bw.Write((string)Convert.ChangeType(v, t));
                }
                else
                    throw new System.Exception("Unable to serialize " + t.Name + " values.");
            }

            public void Read(Object _o, BinaryReader bw)
            {

                object o = GetParentAtMetadataDepth(_o);
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

        private static List<BPDSerializationInfo> GetListedSerializationData(Type t, int metaDepth = 0)
        {
            List<BPDSerializationInfo> sinfo = new List<BPDSerializationInfo>();

            var fields = t.GetFields().Where(prop => prop.IsDefined(typeof(BPDAttribute), false));

            foreach (var field in fields)
            {
                sinfo.Add(new BPDSerializationInfo(((BPDAttribute[])field.GetCustomAttributes(typeof(BPDAttribute), false))[0], field, metaDepth));
                System.Diagnostics.Debug.WriteLine("new field at height: " + metaDepth + " called " + sinfo[sinfo.Count - 1].Name + " in type " + t.FullName);
            }

            var props = t.GetProperties().Where(prop => prop.IsDefined(typeof(BPDAttribute), false));

            foreach (var prop in props)
                sinfo.Add(new BPDSerializationInfo(((BPDAttribute[])prop.GetCustomAttributes(typeof(BPDAttribute), false))[0], prop, metaDepth));

            sinfo.Sort(delegate (BPDSerializationInfo a, BPDSerializationInfo b)
            {
                return a.attrib.position.CompareTo(b.attrib.position);
            });

            //do the same for the metadata class, then combine two lists.
            Type mdt = BPDClassMetadataAttribute.GetMetadataType(t);
            if (mdt != null) { 
                System.Diagnostics.Debug.WriteLine("Reading meta of " + t.FullName);
                    
                List <BPDSerializationInfo>  metaInfo = GetListedSerializationData(mdt, ++metaDepth);
                metaInfo.AddRange(sinfo);
                return metaInfo;
            } else
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
                System.Diagnostics.Debug.WriteLine("Serializing: " + s.Name + " = " + s.GetValue(s.GetParentAtMetadataDepth(o)));
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
                System.Diagnostics.Debug.WriteLine("Deserializing: " + s.Name + " = " + s.GetValue(s.GetParentAtMetadataDepth(o)));
            }

            br.Close();
            fs.Close();
        }
    }

    public enum BPDDepth
    {
        All,
        BlueprintOnly,
        InstanceOnly
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BPDAttribute : Attribute
    {
        public int position;
        public BPDDepth depth;

        public BPDAttribute(int _position, BPDDepth _depth = BPDDepth.All)
        {
            position = _position;
            depth = _depth;
        }

        public BPDAttribute()
        {
            position = 0;
        }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class BPDClassMetadataAttribute : Attribute
    {
        public Type type;


        public BPDClassMetadataAttribute(Type _type)
        {
            type = _type;
        }

        public static Type GetMetadataType(System.Type t)
        {
            var attribs = ((BPDClassMetadataAttribute[])t.GetCustomAttributes(typeof(BPDClassMetadataAttribute), false));
            if (attribs.Length == 0)
                return null;
            return attribs[0].type;
        }

    }

    public class TypeMetadata
    {
        public static TypeMetadata Get(System.Type child)
        {
            TypeMetadata res;
            if (TypeMetadataHandler.instance.TryGetValue(child, out res))
                return res;
            return null;
        }

        public static TypeMetadata GetOrCreate(System.Type child)
        {
            TypeMetadata res = Get(child);
            if (res == null)
            {
                System.Diagnostics.Debug.WriteLine("Creating type-metadata for " + child.FullName);
                Type metaType = BPDClassMetadataAttribute.GetMetadataType(child);
                if (metaType == null)
                    throw new Exception(child.FullName + " does have no type-metadata class defined.");
                res = (TypeMetadata)Activator.CreateInstance(metaType);
                TypeMetadataHandler.instance.Add(child, res);
                return res;
            }
            else
                return res;
        }
    }

    public class Metadata
    {
        public static Metadata Get(Object child)
        {
            Metadata res;
            if (MetadataHandler.instance.TryGetValue(child, out res))
                return res;
            return null;
        }

        public static Metadata GetOrCreate(Object child)
        {
            Metadata res = Get(child);
            if (res == null)
            {
                System.Diagnostics.Debug.WriteLine("Creating metadata for " + child.Name);
                Type metaType = BPDClassMetadataAttribute.GetMetadataType(child.GetType());
                if (metaType == null)
                    throw new Exception(child.Name + " does have no metadata class defined.");
                res = (Metadata)Activator.CreateInstance(metaType);
                MetadataHandler.instance.Add(child, res);
                return res;
            }
            else
                return res;
        }
    }

}
