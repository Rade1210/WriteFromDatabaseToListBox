using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Klasican_ispis_iz_baze_u_listu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string conectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FacultyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection()) 
            {
                sqlConnection.ConnectionString = conectionString; //SqlConnection object is used to initiate a connection string 
                List<Student> list = new List<Student>();
                sqlConnection.Open(); //we are openning connection when we define List
                SqlCommand sqlCommand = new SqlCommand(); //SqlCommand allows manipulation of SQL data
                sqlCommand.Connection = sqlConnection; //Connection is a premade property
                sqlCommand.CommandText = "SELECT * FROM tableStudents"; //Command text returns the result of SQL statement
                SqlDataReader dataReader = sqlCommand.ExecuteReader(); //Reading a result of the SqlCommand
                while (dataReader.Read()) //while the object "dataReader" is reading output
                {
                    Student student = new Student();
                    student.id = dataReader.GetInt32(0); //we are adding the collumns from our database to the List
                    student.Name = dataReader.GetString(1);
                    student.Surname = dataReader.GetString(2);
                    student.Age = dataReader.GetInt32(3);
                    student.IndexNumber = dataReader.GetString(4);
                    list.Add(student);
                }
                sqlConnection.Close();
                foreach (Student s in list) //for each student that is in the list
                {
                    lbWrite.Items.Add(s.id + ", " + s.Name + ", " + s.Surname + ", " + s.IndexNumber);
                }
            }
        }
    }
}
