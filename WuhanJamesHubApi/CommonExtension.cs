﻿using Newtonsoft.Json;

namespace WuhanJamesHubApi
{
    public static class CommonExtension
    {
        public static T DeepClone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
