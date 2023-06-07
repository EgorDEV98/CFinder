using CFinder.Application.Models.Result;

namespace CFinder.Application.Models.AddressCheckerModels;

public class TupleBalances
{
    public ICollection<NFTDto>? NFTs { get; set; }
    public ICollection<TokenDto>? Tokens { get; set; }
}