using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;
public class LevelData {
	public float time_dead;
	public float time_star1;
	public float time_star2;
	public float time_star3;

	public ArrayList list_auto_gen 				 = new ArrayList();
	public ArrayList list_block_pokemon_fixed	 = new ArrayList();
	public ArrayList list_block_stone_fixed 	 = new ArrayList();
	public ArrayList list_block_stone_moving 	 = new ArrayList();
	public ArrayList list_block_frozen_fixed 	 = new ArrayList();
	public ArrayList list_block_stone_and_frozen = new ArrayList();
	public ArrayList list_constraint 			 = new ArrayList();
	public ArrayList list_reward 				 = new ArrayList();

	public LevelData(int level){
		_makeLevelData(level, GameStatic.currentMode);
	}

	public LevelData(int level, int mode) {
		_makeLevelData(level, mode);
	}

	public void _makeLevelData(int level, int mode) {
		string text = "";
		try{
//		text = File.ReadAllText(Application.dataPath + "/Resources/Level/level" + level + ".json");
			Debug.Log("level :: " + level + " - mode :: " + mode + " - maxLevel :: " + GameStatic.maxLevel);
			if (level <= GameStatic.maxLevel || level > 1000)
			{
				Debug.Log("currentLevel :: " + level);
				Debug.Log("currentMode :: " + GameStatic.currentMode);
				if (mode == Const.GAME_MODE_HARD1)
				{
					text = (Resources.Load("Level/HardMode1/level" + level) as TextAsset).text;
				}
				else if (mode == Const.GAME_MODE_HARD2)
				{
					text = (Resources.Load("Level/HardMode2/level" + level) as TextAsset).text;
				}
				else if (mode == Const.GAME_MODE_NORMAL)
				{
					text = (Resources.Load("Level/NormalMode/level" + level) as TextAsset).text;
				}
				else if (mode == Const.GAME_MODE_EVENT)
				{
					int lv = GameConfig.list_level_event[level - 1];
					text = (Resources.Load("Level/HardMode2/level" + lv) as TextAsset).text;
				}
			}
			else
			{
				//text = ImageDownloader.loadText("level_" +level+".json");
			}
			//			Debug.Log(text);
			JSONNode data = JSON.Parse(text);
			//clear old data
			list_auto_gen.Clear();
			list_block_pokemon_fixed.Clear ();
			list_block_stone_fixed.Clear ();
			list_block_stone_moving.Clear ();
			list_block_frozen_fixed.Clear ();
			list_block_stone_and_frozen.Clear ();
			list_constraint.Clear();
			list_reward.Clear();
			//time constraint
			time_dead  = float.Parse (data [StringUtils.time_dead]);
			time_star1 = float.Parse (data [StringUtils.time_star_1]);
			time_star2 = float.Parse (data [StringUtils.time_star_2]);
			time_star3 = float.Parse (data [StringUtils.time_star_3]);
			//pokemon fixed
			JSONArray block_pokemon_fixed = data[StringUtils.pokemon_fixed].AsArray;
			for (int i = 0; i < block_pokemon_fixed.Count; i++) {
				JSONClass pokemon = block_pokemon_fixed[i].AsObject;
				int row = pokemon[StringUtils.row].AsInt;
				int col = pokemon[StringUtils.col].AsInt;
				int id  = pokemon[StringUtils.id].AsInt;
				list_block_pokemon_fixed.Add(new Vector3(row, col, id));
			}
			//stone fixed
			JSONArray block_stone_fixed = data[StringUtils.stones_fixed].AsArray;
			for (int i = 0; i < block_stone_fixed.Count; i++) {
				JSONClass stone = block_stone_fixed[i].AsObject;
				int row = stone[StringUtils.row].AsInt;
				int col = stone[StringUtils.col].AsInt;
				list_block_stone_fixed.Add(new Vec2(row, col));
				list_block_stone_and_frozen.Add(new Vec2(row, col));
			}
			//stone moving
			JSONArray block_stone_moving = data[StringUtils.stones_moving].AsArray;
			Debug.Log(data);
			for (int i = 0; i < block_stone_moving.Count; i++) {
				JSONClass stone = block_stone_moving[i].AsObject;
				int row = stone[StringUtils.row].AsInt;
				int col = stone[StringUtils.col].AsInt;
				list_block_stone_moving.Add(new Vec2(row, col));
			}
			//frozen fixed
			JSONArray block_frozen_fixed = data[StringUtils.frozens_fixed].AsArray;
			for (int i = 0; i < block_frozen_fixed.Count; i++) {
				JSONClass stone = block_frozen_fixed[i].AsObject;
				int row = stone[StringUtils.row].AsInt;
				int col = stone[StringUtils.col].AsInt;
				list_block_frozen_fixed.Add(new Vec2(row, col));
				list_block_stone_and_frozen.Add(new Vec2(row, col));
			}
			//constraint
			JSONArray contraint = data [StringUtils.constraint].AsArray;
			for (int i = 0; i < contraint.Count; i++) {
				JSONClass option = contraint[i].AsObject;
				int direction = option[StringUtils.direction].AsInt;
				JSONClass json_cell1 = option[StringUtils.cell1].AsObject;
				JSONClass json_cell2 = option[StringUtils.cell2].AsObject;
				Vec2 cell1 = new Vec2 (json_cell1[StringUtils.row].AsInt, json_cell1[StringUtils.col].AsInt);
				Vec2 cell2 = new Vec2 (json_cell2[StringUtils.row].AsInt, json_cell2[StringUtils.col].AsInt);
				list_constraint.Add(new ConstraintData(direction, cell1, cell2));
			}
			//reward
			JSONArray rewards = data [StringUtils.reward].AsArray;
			for (int i = 0; i < rewards.Count; i++) {
				JSONClass reward = (JSONClass) rewards[i];
				list_reward.Add(new RewardData(reward));
			}
			//auto gen
			if (data[StringUtils.auto_gen] != null) {
				JSONArray auto_gens = data [StringUtils.auto_gen].AsArray;
				for (int i = 0; i < auto_gens.Count; i++) {
					JSONClass auto_gen = (JSONClass) auto_gens[i];
					int auto_gen_type = auto_gen [StringUtils.type].AsInt;
					float time_gen = auto_gen [StringUtils.time_gen].AsFloat;
					float time_gen_wait = auto_gen [StringUtils.time_gen_wait].AsFloat;
					AutoGenData auto_gen_data = new AutoGenData(auto_gen_type, time_gen_wait, time_gen);
					list_auto_gen.Add(auto_gen_data);
				}
			}
		}
		catch(IOException e){
			Debug.LogError(e.ToString());
		}
	}

	public void loadListBlockStoneAndFrozen(bool hasStone = true, bool hasFrozen = true) {
		list_block_stone_and_frozen.Clear ();
		if (hasStone) {
			list_block_stone_and_frozen.AddRange (list_block_stone_fixed);
		}
		if (hasFrozen) {
			list_block_stone_and_frozen.AddRange (list_block_frozen_fixed);
		}
	}
}
