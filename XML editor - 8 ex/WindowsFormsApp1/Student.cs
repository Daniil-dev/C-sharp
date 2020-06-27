using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Student
    {
        public string name;
        public Group group;
        [XmlArray]
        [XmlArrayItem("SingleMark")]
        public List<byte> marks;
    }

    [Serializable]
    public class Group
    {
        [XmlAttribute]
        public int course;
        [XmlAttribute]
        public int number;
        [XmlAttribute]
        public string track;
    }
}
