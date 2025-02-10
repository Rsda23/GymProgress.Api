using MongoDB.Bson;
using MongoDB.Driver;

namespace GymProgress.Api.MongoHelpers
{
    public static class MongoHelper
    {
        /// <summary>
        ///  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FilterDefinition<T> BuildFindByIdRequest<T>(string id)
        {
            var builder = Builders<T>.Filter;
            ObjectId objectId = ObjectId.Parse(id);
            var filter = builder.Eq("_id", objectId);
            return filter;
        }
    }
}
