using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBXplorer
{
    public partial class NBXplorerNetworkProvider
    {
		private void InitBuyCoinPos(NetworkType networkType)
		{
			Add(new NBXplorerNetwork(NBitcoin.Altcoins.BuyCoinPos.Instance, networkType)
			{
				MinRPCVersion = 140200,
				CoinType = networkType == NetworkType.Mainnet ? new KeyPath("65'") : new KeyPath("1'"),
			});
		}

		public NBXplorerNetwork GetBCP()
		{
			return GetFromCryptoCode(NBitcoin.Altcoins.BuyCoinPos.Instance.CryptoCode);
		}
	}
}
