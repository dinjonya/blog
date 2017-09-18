using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Newtonsoft.Json;
using CorePlugs20.CommonCore;
using CorePlugs20.OdinString;
using System.Reflection;
namespace CorePlugs20.OdinMongo
{
    public class MongoHelper
    {
        public static IMongoDatabase db { get;set; } 
        public static GridFSBucket FS { get; set; }
        public MongoHelper(string _connectionString,string _dbName)
        {
            db = new MongoClient(_connectionString).GetDatabase(_dbName);
            FS = new GridFSBucket(db);
        }
        /*
        清空集合
         */
        public void ClearCollection(string collectionName)
        {
            db.DropCollection(collectionName);
        }

        /// <summary>
        /// 返回document集合 可以接 其他mongo方法 
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IFindFluent<BsonDocument,BsonDocument> SelectBsons(string collectionName,FilterDefinition<BsonDocument> filter)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter);
        }

        public IFindFluent<BsonDocument,BsonDocument> SelectBsonsAnd(string collectionName,params FilterDefinition<BsonDocument>[] filters)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.And(filters);
            return collection.Find(filter);
             
        }

        public List<BsonDocument> SelectListBson(string collectionName,FilterDefinition<BsonDocument> filter)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter).ToList();
        }

        public BsonDocument SelectBsonModel(string collectionName,FilterDefinition<BsonDocument> filter)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            var result = collection.Find(filter);
            return result.SingleOrDefault();
        }

        /*
        批量添加对象
        */
        public void AddModels<T>(string collectionName,List<T> models) where T : class
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            List<BsonDocument> lstBsons = new List<BsonDocument>();
            foreach (var item in models)
            {
                string jsonStr = JsonConvert.SerializeObject(item);
                var bson =  BsonDocument.Parse(jsonStr);
                lstBsons.Add(bson);
            }
            collection.InsertManyAsync(lstBsons);
        }
        /*
        添加对象
        */
        public void AddModel<T>(string collectionName,T model) where T: class
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            string jsonStr = JsonConvert.SerializeObject(model);
            var bson =  BsonDocument.Parse(jsonStr);
            collection.InsertOne(bson);
        }

        public void AddBsonModel(string collectionName,BsonDocument model)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            collection.InsertOne(model);
        }

        /*
        更新对象
        */
        public void UpdateModel<T>(string collectionName, T model) where T : class
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            string _id = model.GetType().GetRuntimeProperty("_id").GetValue(model).ToString();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",new ObjectId(_id));
            var bsonModel = BsonExtensionMethods.ToBsonDocument(model);
            var updateFilter =  new BsonDocument { { "$set", new BsonDocument(bsonModel) } };
            collection.UpdateOne(filter, updateFilter);
        }

        public void UpdateBsonModel(string collectionName, BsonDocument model)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            string _id = model.GetValue("_id").ToString();
            var filter = Builders<BsonDocument>.Filter.Eq("_id",new ObjectId(_id));
            var updateFilter =  new BsonDocument { { "$set", model } };
            collection.UpdateOne(filter, updateFilter);
        }

        public void FindUpdateModel(string collectionName, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> updateFilter)
        {
            var collection = db.GetCollection<BsonDocument>(collectionName);
            collection.FindOneAndUpdate(filter,updateFilter);
        }

        /*
        删除对象
        */
		public DeleteResult RemoveModel(string collectionName, FilterDefinition<BsonDocument> filter)
		{
			var collection = db.GetCollection<BsonDocument>(collectionName);
			return collection.DeleteOne(filter);
		}

		public DeleteResult RemoveModel(string collectionName, string mongoId)
		{
			var collection = db.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(mongoId));
			return collection.DeleteOne(filter);
		}

        /// <summary>
        /// BsonModel to JsonModel    
        /// </summary>
        /// <param name="bson">BsonModel</param>
        /// <param name="elesName">需要移除的bson的属性</param>
        /// <returns></returns>
        public T ConvertMongoObjectToObject<T>(BsonDocument bson,IList<string> elesName) where T : class
        {
            foreach (var item in elesName)
            {
                bson.Remove(item);
            }
            return JsonConvert.DeserializeObject<T>(bson.ToJson());
        }

        /// <summary>
        /// BsonModels to JsonModels  
        /// </summary>
        /// <param name="bson">BsonModels</param>
        /// <param name="elesName">需要移除的bson的属性</param>
        /// <returns></returns>
        public List<T> ConvertMongoObjectsToObjects<T>(List<BsonDocument> bsons,IList<string> elesName) where T : class
        {
            List<T> lst =  new List<T>();
            foreach (var item in bsons)
            {
                foreach (var ele in elesName)
                {
                    item.Remove(ele);
                }
                lst.Add(JsonConvert.DeserializeObject<T>(item.ToJson()));
            }
            
            return lst;
        }
    }
}