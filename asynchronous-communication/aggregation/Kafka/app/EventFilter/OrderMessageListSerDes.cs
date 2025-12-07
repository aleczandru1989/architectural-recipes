using System.Text.Json;
using Confluent.Kafka;
using Filter.Abstractions.Messages;
using Streamiz.Kafka.Net.SerDes;

namespace EventFilter;

public class OrderMessageListSerDes : AbstractSerDes<List<OrderMessage>>
{
    public override byte[] Serialize(List<OrderMessage> data, SerializationContext context)
    {
        if (data == null)
            return null;

        var json = JsonSerializer.Serialize(data);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    public override List<OrderMessage> Deserialize(byte[] data, SerializationContext context)
    {
        if (data == null || data.Length == 0)
            return null;

        var json = System.Text.Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<List<OrderMessage>>(json);
    }
}
