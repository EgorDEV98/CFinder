namespace CFinder.Application.ServerMethods.Base;

public class AddressGeneratorServerMethod
{
    public ICollection<string> GetEthereumAddress(string mnemonic, int depth = 3)
    {
        if(string.IsNullOrWhiteSpace(mnemonic))
            return Array.Empty<string>();
        
        return new Nethereum.HdWallet.Wallet(mnemonic, null)
            .GetAddresses(depth);
    }
}