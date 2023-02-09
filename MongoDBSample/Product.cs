using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBSample
{
    [Serializable]
    public class Product
    { //id field of collection //name of field  //datatype of field in database in mongodb 
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)] 
        public string ProductId { get; set; } //in applicaton datatype


        [BsonElement("Product_code"), BsonRepresentation(BsonType.String)]

        public string ProductCode { get; set; } //same as getter and setter


        [BsonElement("Product_name"), BsonRepresentation(BsonType.String)]
        public string ProductName { get; set; }


        [BsonElement("Price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }






    }
}