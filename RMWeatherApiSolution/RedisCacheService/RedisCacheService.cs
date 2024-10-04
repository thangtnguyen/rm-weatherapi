using StackExchange.Redis;
using System.Text.Json;
using WeatherApi.Models.Interfaces;

namespace WeatherApi.RedisCacheService
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _redis;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _redis.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public bool RemoveData(string key)
        {
            bool _isKeyExist = _redis.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _redis.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _redis.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
            return isSet;
        }
    }
}
