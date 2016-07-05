using System;
using System.Data;
using TShockAPI;
using TShockAPI.DB;

namespace ItemDropLog
{
	public static class ItemDropLogger
	{
        
		public static void CreateEntry(ItemDropLogInfo info)
		{
			if (!info.IsValid)
			{
				TShock.Log.ConsoleError("ItemDropLogger tried to create an entry based on invalid info.");
				return;
			}
            ItemDropLogPlugin.db.Query("INSERT INTO `ItemLog` (`Timestamp`,`ServerName`,`SourcePlayerName`,`SourceIP`,`TargetPlayerName`,`TargetIP`,`Action`,`DropX`,`DropY`,`ItemNetId`,`ItemName`,`ItemStack`,`ItemPrefix`) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12)", new object[]
			{
				info.Timestamp.ToString("s"),
				info.ServerName,
				info.SourcePlayerName,
				info.SourceIP,
				info.TargetPlayerName,
				info.TargetIP,
				info.Action,
				info.DropX,
				info.DropY,
				info.ItemNetId,
				info.ItemName,
				info.ItemStack,
				info.ItemPrefix
			});
		}

		internal static void CreateItemEntry(ItemDropLogInfo info)
		{
			ItemDropLogPlugin.db.Query("INSERT INTO `ItemLog` (`Timestamp`,`ServerName`,`SourcePlayerName`,`SourceIP`,`Action`,`DropX`,`DropY`,`ItemNetId`,`ItemName`,`ItemStack`,`ItemPrefix`) VALUES (@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)", new object[]
			{
				info.Timestamp.ToString("s"),
				info.ServerName,
				info.SourcePlayerName,
				info.SourceIP,
				info.Action,
				info.DropX,
				info.DropY,
				info.ItemNetId,
				info.ItemName,
				info.ItemStack,
				info.ItemPrefix
			});
		}

		internal static void UpdateItemEntry(ItemDropLogInfo info)
		{
			ItemDropLogPlugin.db.Query("UPDATE `ItemLog` SET `TargetPlayerName`=@0, `TargetIP`=@1, `Action`=@2 WHERE `ServerName`=@3 AND `Action`=@4 AND `SourcePlayerName`=@5 AND `ItemNetId`=@6 AND `ItemStack`=@7 AND `ItemPrefix`=@8", new object[]
			{
				info.TargetPlayerName,
				info.TargetIP,
				info.Action,
				info.ServerName,
				"PlayerDrop",
				info.SourcePlayerName,
				info.ItemNetId,
				info.ItemStack,
				info.ItemPrefix
			});
		}
	}
}
