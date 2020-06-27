using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Text;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var students = new List<Student>();
            var group = new Group
            {
                course = 3,
                number = 4,
                track = "ПОКС"
            };
            for (int i = 0; i < 10; ++i)
            {
                var student = new Student
                {
                    name = $"Студент {i}",
                    group = group,
                    marks = new List<byte> { 0, 1, 2, 3 }
                };
                students.Add(student);
            }
            var serializer = new XmlSerializer(typeof(List<Student>));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.Unicode;
            using (XmlWriter fs = XmlWriter.Create("students.xml", settings))
            {
                serializer.Serialize(fs, students);

                Debug.WriteLine("Объект сериализован");
            }
            using (FileStream fs = new FileStream("students.xml", FileMode.Open))
            {
                foreach (var student in serializer.Deserialize(fs) as List<Student>)
                {
                    Debug.WriteLine(student.name);
                }
            }
            Application.Run(new Form1());
        }
    }
}
