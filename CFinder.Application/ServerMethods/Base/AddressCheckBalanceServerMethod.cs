using CFinder.Application.Models.Result;

namespace CFinder.Application.ServerMethods.Base;

public class AddressCheckBalanceServerMethod
{
    public virtual async Task<List<TokenDto>> CheckToken(string address, bool onlyWhiteListed = true)
    {
        await Task.CompletedTask;
        return new List<TokenDto>();
    }
    
    public virtual async Task<List<NFTDto>> CheckNFT(string address)
    {
        await Task.CompletedTask;
        return new List<NFTDto>();
    }
}