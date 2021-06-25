namespace P2PChat.Services.Serializing
{
	public interface IPacketSerializerService
	{
		byte[] Serialize<T>(T obj) where T : class;
		T Deserialize<T>(byte[] buffer) where T : class;
	}
}