using System;
using System.Buffers;
using System.IO;
using MessagePack;

namespace P2PChat.Services.Serializing
{
	public class MsgPackSerializerService : IPacketSerializerService
	{
		public byte[] Serialize<T>(T obj) where T : class
		{
			var bin = MessagePackSerializer.Serialize(typeof(T), obj,
				MessagePack.Resolvers.ContractlessStandardResolver.Options);
			return bin;
		}

		public T Deserialize<T>(byte[] buffer) where T : class
		{
			ReadOnlyMemory<byte> bin = new ReadOnlyMemory<byte>(buffer);
			var obj = MessagePackSerializer.Deserialize(typeof(T), bin,
				MessagePack.Resolvers.ContractlessStandardResolver.Options);
			return obj as T;
		}
	}
}