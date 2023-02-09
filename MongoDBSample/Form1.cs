using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;//referance for to get the connection string from app.config file using configuration manager class
using MongoDBSample;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Linq;

namespace MongoDBSample
{
    public partial class Form1 : Form
    {   
        //private variable <modal class>
        IMongoCollection<Product> productCollection;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //connection string reside in app.config                     //connection string name
            var connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;


            //use this function to connect to database
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            productCollection = database.GetCollection<Product>("product");

            LoadProductData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        //code for insertion the new record
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Product>.Filter.Eq(a => a.ProductCode, txtProductCode.Text);
            productCollection.DeleteOne(filterDefinition);
            LoadProductData();
        }


        //code for inserting the data
        private void btnInsert_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                ProductCode = txtProductCode.Text,
                ProductName = txtProductName.Text,
                Price = decimal.Parse(txtPrice.Text) //to convert text to decimal
            };
            productCollection.InsertOne(product);
            LoadProductData();
        }




        //for all updates after that display the updated data use load product data
        private void LoadProductData() //to load the product data in database
        {
            var filterDefinition = Builders<Product>.Filter.Empty; //noneed filterdefinition thats wht it is empty
            var products = productCollection.Find(filterDefinition).ToList();
            dataGridView1.DataSource = products; //load all the data in data gride
        }



        //bulk insert all the recort from csv file
        private void btnBulkInsert_Click(object sender, EventArgs e)
        {                                      //fetch the data from file
            var csvLines = File.ReadAllLines(@"C:\Users\ADMIN\Desktop\Product.csv").ToList(); 
            var products = csvLines.Select(a => new Product
            {
                ProductName = a.Split(',')[0],
                ProductCode = a.Split(',')[1],
                Price = decimal.Parse(a.Split(',')[2])

            }).ToList();
            productCollection.InsertMany(products);
            LoadProductData();


        }


        //update the record
        private void BtnUpdate_Click(object sender, EventArgs e)
        {                                                  //equal to
            var filterDefinition = Builders<Product>.Filter.Eq(a => a.ProductCode, txtProductCode.Text);
            var UpdateDefinition = Builders<Product>.Update.Set(a => a.ProductName, txtProductName.Text).Set(a => a.Price, decimal.Parse(txtPrice.Text));

            productCollection.UpdateMany(filterDefinition, UpdateDefinition);
            LoadProductData();
        }





        //Bulk update the product whose price 50 --> 400
        private void button2_Click(object sender, EventArgs e)
        {                                        //greater than equal to pice 50
            var filterDefination = Builders<Product>.Filter.Gte(a => a.Price, 50);
            var updateDEfinition = Builders<Product>.Update.Set(a => a.Price, 400);
            productCollection.UpdateMany(filterDefination, updateDEfinition);
            LoadProductData();
        }






        //code for bulk delete button delete the product whose price 190
        private void button3_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Product>.Filter.Eq(a => a.Price, 190);
            productCollection.DeleteMany(filterDefinition);
            LoadProductData();
        }
    }
}

















