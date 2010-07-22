using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCell.RealmServer.Lang
{
	/// <summary>
	/// Keys for strings used in commands
	/// </summary>
	public enum LangKey
	{
		None = 0,
		Done,
		Custom,
		Addon,
		Library,
		PlayerNotOnline,

		// #######################################################################
		// Commands
		SubCommandNotFound,
		MustNotUseCommand,

		CmdSummonPlayerNotOnline,

		CmdKickMustProvideName,

		CmdLocalizerDescription,
		CmdLocalizerReloadDescription,
		CmdLocalizerSetLocaleDescription,
		CmdLocalizerSetLocaleParamInfo,
		LocaleSet,
		UnableToSetUserLocale,

		CmdSpellGetDescription,
		CmdSpellGetParamInfo,

		GossipOptionBanker,
		GossipOptionFlightMaster,
		GossipOptionTrainer,
		GossipOptionVendor
	}

	public class TranslatableItem : Util.Lang.TranslatableItem<LangKey>
	{
		public TranslatableItem(LangKey key, params object[] args) : base(key, args)
		{
		}
	}
}