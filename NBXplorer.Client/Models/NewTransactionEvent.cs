﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NBitcoin;
using NBXplorer.DerivationStrategy;
using Newtonsoft.Json;

namespace NBXplorer.Models
{
	public class NewTransactionEvent : NewEventBase
	{
		public uint256 BlockId
		{
			get; set;
		}

		public TrackedSource TrackedSource { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]

		public DerivationStrategyBase DerivationStrategy
		{
			get; set;
		}

		public TransactionResult TransactionData
		{
			get; set;
		}

		public List<MatchedOutput> Outputs
		{
			get; set;
		} = new List<MatchedOutput>();

		public Coin[] GetReceivedCoins()
		{
			return Outputs.Select(o => o.AsCoin(TransactionData.TransactionHash)).ToArray();
		}
	}

	public class MatchedOutput
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public KeyPath KeyPath { get; set; }
		public Script ScriptPubKey { get; set; }
		public Script Redeem { get; set; }
		public int Index { get; set; }
		public Money Value { get; set; }
		public Coin AsCoin(uint256 txId)
		{
			var coin = new Coin(new OutPoint(txId, (int)Index), new TxOut() { ScriptPubKey = ScriptPubKey, Value = Value });
			return Redeem == null ? coin : coin.ToScriptCoin(Redeem);
		}
	}
}