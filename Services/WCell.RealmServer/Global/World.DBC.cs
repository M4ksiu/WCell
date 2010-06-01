﻿using System;
using WCell.Constants;
using WCell.Constants.World;
using WCell.Core.DBC;
using WCell.Util;
using WCell.Util.Graphics;
using NLog;

namespace WCell.RealmServer.Global
{
	public class MapDifficultyConverter : DBCRecordConverter
	{
		public override void Convert(byte[] rawData)
		{
			var entry = new MapDifficultyEntry();

			entry.Id = (uint)GetInt32(rawData, 0);
			entry.MapId = (MapId)GetUInt32(rawData, 1);
			entry.Index = GetUInt32(rawData, 2);
			entry.RequirementString = GetString(rawData, 3);
			//info.TextFlags = GetUInt32(rawData, 4);
			entry.ResetTime = GetInt32(rawData, 4);
			entry.MaxPlayerCount = GetInt32(rawData, 5);

			var map = World.GetRegionInfo(entry.MapId);
			if (map != null)
			{
				if (entry.Index >= (double) RaidDifficulty.End)
				{
					LogManager.GetCurrentClassLogger().Warn("Invalid MapDifficulty for {0} with Index {1}.", entry.MapId, entry.Index);
					return;
				}

				if (entry.MaxPlayerCount == 0)
				{
					entry.MaxPlayerCount = map.MaxPlayerCount;
				}

				if (map.Difficulties == null)
				{
					map.Difficulties = new MapDifficultyEntry[(int) RaidDifficulty.End];
				}
				map.Difficulties[entry.Index] = entry;

				entry.Finalize(map);
			}
		}
	}

	public class MapConverter : DBCRecordConverter
	{
		public override void Convert(byte[] rawData)
		{
			var rgn = new RegionInfo();

            int i = 0;
			rgn.Id = (MapId)GetUInt32(rawData, i++);


            i++; //rgn.InternalName = GetString(rawData, 1);
			rgn.Type = (MapType)GetUInt32(rawData, i++);
			i++; //mapFlags

			rgn.HasTwoSides = GetUInt32(rawData, i++) != 0; //isPVP


			rgn.Name = GetString(rawData, i++);

            rgn.AreaTableId = GetUInt32(rawData, i++);	//linked Zone
            i++; //rgn.HordeText = GetString(rawData, i++);
            i++; //rgn.AllianceText = GetString(rawData, i++);
            rgn.ParentMapId = (MapId)GetUInt32(rawData, i++); //multi map id
			rgn.RepopRegionId = rgn.ParentMapId;
            i++; //battlefield map icon scale
            i++; //Repop map id
            rgn.RepopPosition = new Vector3(GetFloat(rawData, i++), GetFloat(rawData, i++), 500);
            i++; //time of day override
            rgn.RequiredClientId = (ClientId)GetUInt32(rawData, i++);
            rgn.DefaultResetTime = GetInt32(rawData, i++);
            rgn.MaxPlayerCount = GetInt32(rawData, i++);
            i++; //unk 4.0.0

			//rgn.HeroicResetTime = GetUInt32(rawData, 113);
			//rgn.RaidResetTime = GetUInt32(rawData, 112);

			ArrayUtil.Set(ref World.s_regionInfos, (uint)rgn.Id, rgn);
		}
	}
}
