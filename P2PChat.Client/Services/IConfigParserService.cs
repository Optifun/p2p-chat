namespace P2PChat.Client.Services
{
    public interface IConfigParserService
    {
        string Ip { get; }
        string Host { get; }
        void Parse();
    }
}