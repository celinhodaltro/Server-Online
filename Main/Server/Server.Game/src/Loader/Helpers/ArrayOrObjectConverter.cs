﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Loader.Helpers;

internal class ArrayOrObjectConverter<T> : JsonConverter
{
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        return token.Type == JTokenType.Array
            ? token.ToObject<List<T>>()
            : new List<T> { token.ToObject<T>() };
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<T>);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}