using CFinder.Application.Models.Result;

namespace CFinder.Application.ServerMethods.Base;

public class AddressGeneratorServerMethod
{
    /// <summary>
    /// Get ethereum AddressInfoDto with address
    /// </summary>
    /// <param name="mnemonic">mnemonic phrase</param>
    /// <param name="depth">number of account</param>
    /// <returns></returns>
    public async Task<IEnumerable<AddressInfoDto>?> GetEthereumAddressesInfoDto(string? mnemonic, int depth)
    {
        await Task.CompletedTask;
        if(string.IsNullOrWhiteSpace(mnemonic))
            return Array.Empty<AddressInfoDto>();

        return new Nethereum.HdWallet.Wallet(mnemonic, null)
            .GetAddresses(depth)
            .Select((value, position) => new AddressInfoDto()
            {
                Address = value,
                NumberOfAddress = position
            });
    } 
    
    /// <summary>
    /// Get ethereum addresses
    /// </summary>
    /// <param name="mnemonic">mnemonic phrase</param>
    /// <param name="depth">number of account</param>
    /// <returns></returns>
    public async Task<ICollection<string>?> GetEthereumAddressAsString(string? mnemonic, int depth = 3)
    {
        await Task.CompletedTask;
        if(string.IsNullOrWhiteSpace(mnemonic))
            return Array.Empty<string>();

        return new Nethereum.HdWallet.Wallet(mnemonic, null)
            .GetAddresses(depth);
    }
}