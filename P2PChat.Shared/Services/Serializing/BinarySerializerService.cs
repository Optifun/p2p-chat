using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace P2PChat.Services.Serializing
{
	public class BinarySerializerService : IPacketSerializerService
	{
		private BinaryFormatter _fmt;

		public BinarySerializerService()
		{
			_fmt = new BinaryFormatter();
		}

		public byte[] Serialize<T>(T obj) where T: class
		{
			var stream = new MemoryStream();
			_fmt.Serialize(stream, obj);
			return stream.ToArray();
		}

		public T Deserialize<T>(byte[] buffer) where T: class
		{
			var stream = new MemoryStream(buffer);
			return _fmt.Deserialize(stream) as T;
		}
	}
}