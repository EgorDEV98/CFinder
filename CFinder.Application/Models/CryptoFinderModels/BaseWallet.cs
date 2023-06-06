using CFinder.Application.Models.Settings;
using CFinder.Domain.Log;

namespace CFinder.Application.Models.CryptoFinderModels;

public abstract class BaseWallet
{
    public readonly string Directory;
    public abstract Wallet.WalletType walletType { get; set; }
    protected BaseWallet(string directory)
    {
        Directory = directory;
    }
    
    public virtual async Task<string[]> GetOriginalAddresses()
    {
        await Task.CompletedTask;
        return Array.Empty<string>();
    }
    public abstract Task<string> GetEncrypted(DecryptorSettingsDto settings);
    public abstract Task<string> GetHashcat(string json);
    public abstract Task<(string decrypted, string password, string mnemonic)> Decrypt(string walletEncrypted,
        string?[] passwordList);
}