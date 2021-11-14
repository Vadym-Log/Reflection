using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Task_03
{
    public partial class Form1 : Form
    {
        Assembly assembly = null;
        Type[] types;
        FieldInfo[] fields;
        Type[] interfaces;
        MemberInfo[] members;
        ConstructorInfo[] constructors;
        PropertyInfo[] properties;
        MethodInfo[] methods;
        ParameterInfo[] parameters;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                treeView1.Nodes.Clear();
                textBox1.Clear();
                string filestr = openFileDialog1.FileName;
                assembly = Assembly.LoadFile(filestr);
                textBox1.Text += "СБОРКА    " + filestr + "  -  УСПЕШНО ЗАГРУЖЕНА" + Environment.NewLine + Environment.NewLine;

                types = assembly.GetTypes();
                foreach (Type t in types)
                {
                    treeView1.Nodes.Add(t.ToString());
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string nameclass = treeView1.SelectedNode.Text;
			if (e.Node.Parent == null)
			{
				textBox1.Clear();
				for (int i = 0; i < types.Length; ++i)
					if (nameclass == types[i].FullName)
					{
						if (types[i].BaseType != null)
							textBox1.Text += "     Для данного класса базовым является " + types[i].BaseType + Environment.NewLine;
						if (types[i].IsAbstract)
							textBox1.Text += "     Данный класс является абстрактным" + Environment.NewLine;

						fields = types[i].GetFields();
						interfaces = types[i].GetInterfaces();
						members = types[i].GetMembers();
						constructors = types[i].GetConstructors();
						properties = types[i].GetProperties();
						methods = types[i].GetMethods();
					}
				textBox1.Text += Environment.NewLine;
				textBox1.Text += "       Список всех интерфейсов класса: " + Environment.NewLine;
				foreach (Type t in interfaces)
					textBox1.Text += t.Name + Environment.NewLine;
				textBox1.Text += Environment.NewLine;

				textBox1.Text += "       Список всех полей класса: " + Environment.NewLine;
				foreach (FieldInfo f in fields)
					textBox1.Text += f.Name + Environment.NewLine;
				textBox1.Text += Environment.NewLine;

				textBox1.Text += "       Список всех свойств класса: " + Environment.NewLine;
				foreach (PropertyInfo p in properties)
					textBox1.Text += p.Name + Environment.NewLine;
				textBox1.Text += Environment.NewLine;

				textBox1.Text += "       Список всех членов класса: " + Environment.NewLine;
				foreach (MemberInfo m in members)
					textBox1.Text += m.Name + Environment.NewLine;

				if (treeView1.SelectedNode.Nodes.Count == 0)
				{
					foreach (ConstructorInfo c in constructors)
						treeView1.SelectedNode.Nodes.Add("Конструктор " + c.Name);
					foreach (MethodInfo m in methods)
						treeView1.SelectedNode.Nodes.Add("Метод " + m.Name);
				}
			}

			for (int i = 0; i < constructors.Length; ++i)
			{
				if (nameclass == ("Конструктор " + constructors[i].Name))
				{
					textBox1.Clear();
					textBox1.Text += "         Параметры конструктора" + Environment.NewLine;
					parameters = constructors[i].GetParameters();
					foreach (ParameterInfo p in parameters)
					{
						textBox1.Text += "Имя: " + p.Name + Environment.NewLine;
						textBox1.Text += "Позиция: " + p.Position + Environment.NewLine;
						textBox1.Text += "Тип: " + p.ParameterType + Environment.NewLine + Environment.NewLine;
					}
					textBox1.Text += Environment.NewLine;
					textBox1.Text += "        Тело конструктора" + Environment.NewLine;
					MethodBody methbody = constructors[i].GetMethodBody();
					textBox1.Text += methbody.ToString();
				}
			}
			for (int i = 0; i < methods.Length; ++i)
			{
				if (nameclass == ("Метод " + methods[i].Name))
				{
					textBox1.Clear();
					textBox1.Text += "         Параметры метода" + Environment.NewLine;
					parameters = methods[i].GetParameters();
					foreach (ParameterInfo p in parameters)
					{
						textBox1.Text += "Имя: " + p.Name + Environment.NewLine;
						textBox1.Text += "Позиция: " + p.Position + Environment.NewLine;
						textBox1.Text += "Тип: " + p.ParameterType + Environment.NewLine + Environment.NewLine;
					}
					textBox1.Text += "Тип возвращаемого значения: " + methods[i].ReturnType;
					textBox1.Text += Environment.NewLine + Environment.NewLine;
					textBox1.Text += "        Тело метода" + Environment.NewLine;
					MethodBody methbody = methods[i].GetMethodBody();
					textBox1.Text += methbody.ToString();
				}
			}
		}
	}
}
