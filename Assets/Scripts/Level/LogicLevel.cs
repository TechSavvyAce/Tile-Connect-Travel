using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
public class LogicLevel : MonoBehaviour {

	private int score;
	private int max_level = GameConfig.num_level;
	private int level = 5;
	private JSONNode data;
	private float time_move = 0.1f;

	public Map map;
	public InfoBar infoBar;
	public ScoreBar scoreBar;
	public TimeBar timeBar;
	public ResultBar resultBar;

	public LevelData levelData;

	public ArrayList list_pos_need_update  = new ArrayList();

	public void setLevel(int level) {
		max_level = GameStatic.maxLevel;
		this.score = GameStatic.currentScore;
		this.list_pos_need_update.Clear();
		// ListLevel.CurrentLevel = level;
		map.changeState (Map.MapState.playing);
		if (level > max_level) {
			level = max_level;
		}
		levelData = null;
		levelData = new LevelData (level);
		if (timeBar) {
			if (GameStatic.mapData != null) {
				float currentTime = GameStatic.mapData ["current_time"].AsFloat;
				timeBar.startBar (levelData.time_dead, currentTime);
			} else {
				timeBar.startBar (levelData.time_dead);
			}
			timeBar.setTarget (levelData.time_star3, levelData.time_star2, levelData.time_star1);
		}
		this.level = level;
		GameStatic.currentLevel = level;
		if(infoBar)
		infoBar.SetLevel (level);
		if(scoreBar) {
			scoreBar.setInfo(level, GameStatic.currentScore);
		}
	}

	public void setLevelForTutorial() {
		int level = 9001;
		GameStatic.currentMode = Const.GAME_MODE_NORMAL;
		GameStatic.maxLevel = 1;
		map.changeState (Map.MapState.playing);
		levelData = null;
		levelData = new LevelData (level);
		this.level = level;
	}

	public void changeLevelValue(int level){
		this.level = level;
	}

	public void nextLevel() {
		setLevel (this.level + 1);
	}

	public void replay(){
		setLevel (this.level);
	}

	public int getLevel () {
		return level;
	}

	public void updateScore(int score) {
		setScore (this.score + score);
	}

	public void setScore(int score){
		GameStatic.currentScore = score;
		this.score = score;
		if(infoBar)
		infoBar.SetScore (this.score);
		if (scoreBar)
		scoreBar.SetScore (this.score);
	}

	public int getScore() {
		return score;
	}

	public int getScoreBonus() {
		float score = 0;
		float timeRemain = timeBar.getTimeRemain ();
		if (timeRemain <= 0) {
			return 0;
		}
		if (timeRemain >= levelData.time_star3) {
			score += (timeRemain - levelData.time_star3) * GameConfig.bonus_score_star3;
			timeRemain = levelData.time_star3;
		}
		if (timeRemain >= levelData.time_star2) {
			score += (timeRemain - levelData.time_star2) * GameConfig.bonus_score_star2;
			timeRemain = levelData.time_star2;
		}
		if (timeRemain >= levelData.time_star1) {
			score += (timeRemain - levelData.time_star1) * GameConfig.bonus_score_star1;
			timeRemain = levelData.time_star1;
		}
		return (int) score;
	}

	public void collectReward() {
		int star = timeBar.getNumStar ();
		for (int i = 0; i < levelData.list_reward.Count; i++) {
			RewardData reward = (RewardData) levelData.list_reward [i];
			if (reward.type == Const.REWARD_HINT) {
				int reward_hint = reward.getReward (star);
				ItemController.addHintItem (reward_hint);
				if (resultBar) {
					resultBar.txtHint.text = "+" + reward_hint;
				}
			} else if (reward.type == Const.REWARD_RANDOM) {
				int reward_random = reward.getReward (star);
				ItemController.addRandomItem (reward_random);
				if (resultBar) {
					resultBar.txtRandom.text = "+" + reward_random;
				}
			} else if (reward.type == Const.REWARD_ENERGY) {
				int reward_energy = reward.getReward (star);
				ItemController.addEnergyItem (reward_energy);
				if (resultBar) {
					//resultBar.txtEnergy.text = "+" + reward_energy;
				}
			}
		}
	}

	public void test() {
		Debug.Log("level test");
		map.test ();
	}

	public void updateMap(Vec2 POS1, Vec2 POS2) {
		for (int i = 0; i < levelData.list_constraint.Count; i++) {
			ConstraintData constraint = (ConstraintData) levelData.list_constraint [i];
			handleLogic (POS1, POS2, constraint.cell1, constraint.cell2, constraint.direction, levelData.list_block_stone_and_frozen);
		}
	}

	public void updateMap() {
		for (int j = 0; j < list_pos_need_update.Count; j++) {
			for (int i = 0; i < levelData.list_constraint.Count; i++) {
				Vec2 POS = (Vec2) list_pos_need_update[j];
				ConstraintData constraint = (ConstraintData) levelData.list_constraint [i];
				handleLogic (POS, constraint.cell1, constraint.cell2, constraint.direction, levelData.list_block_stone_and_frozen);
			}
		}
		list_pos_need_update.Clear();
	}

	public void updateMap(Vec2 POS) {
		time_move = 0.25f;
		for (int i = 0; i < levelData.list_constraint.Count; i++) {
			ConstraintData constraint = (ConstraintData) levelData.list_constraint [i];
			handleLogic (POS, constraint.cell1, constraint.cell2, constraint.direction, levelData.list_block_stone_and_frozen);
		}
		time_move = 0.1f;
	}

	void handleLogic(Vec2 POS, Vec2 cell1, Vec2 cell2, int direction, ArrayList list_block_stone_and_frozen) {
		if (isInBound (POS, cell1, cell2)) {
			if (direction == Const.DIRECTION_LEFT) {
				Vec2 boundLeft = getBoundLeft(list_block_stone_and_frozen, POS, cell1, cell2);
				Vec2 boundRight = getBoundRight(list_block_stone_and_frozen, POS, cell1, cell2);
				logicMoveLeft (boundLeft, boundLeft.C, boundRight.C);
			} else if (direction == Const.DIRECTION_RIGHT) {
				Vec2 boundLeft = getBoundLeft(list_block_stone_and_frozen, POS, cell1, cell2);
				Vec2 boundRight = getBoundRight(list_block_stone_and_frozen, POS, cell1, cell2);
				logicMoveRight (boundRight, boundLeft.C, boundRight.C);
			} else if (direction == Const.DIRECTION_UP) {
				Vec2 boundBottom = getBoundBottom(list_block_stone_and_frozen, POS, cell1, cell2);
				Vec2 boundTop = getBoundTop(list_block_stone_and_frozen, POS, cell1, cell2);
				logicMoveUp (boundTop, boundBottom.R, boundTop.R);
			} else if (direction == Const.DIRECTION_DOWN) {
				Vec2 boundBottom = getBoundBottom(list_block_stone_and_frozen, POS, cell1, cell2);
				Vec2 boundTop = getBoundTop(list_block_stone_and_frozen, POS, cell1, cell2);
				logicMoveDown (boundBottom, boundBottom.R, boundTop.R);
			}
		}
	}

	void handleLogic(Vec2 POS1, Vec2 POS2, Vec2 cell1, Vec2 cell2, int direction, ArrayList list_block_stone_and_frozen) {
		if (direction == Const.DIRECTION_LEFT) {
			logicMoveLeftByDirection (POS1, POS2, cell1, cell2, list_block_stone_and_frozen);
		} else if (direction == Const.DIRECTION_RIGHT) {
			logicMoveRightByDirection (POS1, POS2, cell1, cell2, list_block_stone_and_frozen);
		} else if (direction == Const.DIRECTION_UP) {
			logicMoveUpByDirection (POS1, POS2, cell1, cell2, list_block_stone_and_frozen);
		} else if (direction == Const.DIRECTION_DOWN) {
			logicMoveDownByDirection (POS1, POS2, cell1, cell2, list_block_stone_and_frozen);
		}
	}

	void logicMoveLeftByDirection(Vec2 POS1, Vec2 POS2, Vec2 cell1, Vec2 cell2, ArrayList list_block_stone_and_frozen) {
		bool isInBoundPos1 = isInBound(POS1, cell1, cell2);
		bool isInBoundPos2 = isInBound(POS2, cell1, cell2);
		if (isInBoundPos1 && isInBoundPos2) {
			Vec2 boundLeft1 = getBoundLeft(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundLeft2 = getBoundLeft(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundRight1 = getBoundRight(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundRight2 = getBoundRight(list_block_stone_and_frozen, POS2, cell1, cell2);

			if (boundRight1.C < POS2.C || boundRight2.C < POS1.C) {//2 pokemon o 2 khoang
				logicMoveLeft(POS1, boundLeft1.C, boundRight1.C);
				logicMoveLeft(POS2, boundLeft2.C, boundRight2.C);
			} else {
				if (POS1.C < POS2.C) {
					logicMoveLeft (POS1, boundLeft1.C, boundRight1.C);
					if (POS2.R == POS1.R) {
						return;
					}
					logicMoveLeft (POS2, boundLeft2.C, boundRight2.C);
				} else {
					logicMoveLeft (POS2, boundLeft2.C, boundRight2.C);
					if (POS2.R == POS1.R) {
						return;
					}
					logicMoveLeft (POS1, boundLeft1.C, boundRight1.C);
				}
			}
		} else if (isInBoundPos1) {
			Vec2 boundLeft1 = getBoundLeft(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundRight1 = getBoundRight(list_block_stone_and_frozen, POS1, cell1, cell2);
			logicMoveLeft(POS1, boundLeft1.C, boundRight1.C);
		} else if (isInBoundPos2) {
			Vec2 boundLeft2 = getBoundLeft(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundRight2 = getBoundRight(list_block_stone_and_frozen, POS2, cell1, cell2);
			logicMoveLeft(POS2, boundLeft2.C, boundRight2.C);
		}
	}

	void logicMoveRightByDirection(Vec2 POS1, Vec2 POS2, Vec2 cell1, Vec2 cell2, ArrayList list_block_stone_and_frozen) {
		bool isInBoundPos1 = isInBound(POS1, cell1, cell2);
		bool isInBoundPos2 = isInBound(POS2, cell1, cell2);
		if (isInBoundPos1 && isInBoundPos2) {
			Vec2 boundLeft1 = getBoundLeft(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundLeft2 = getBoundLeft(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundRight1 = getBoundRight(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundRight2 = getBoundRight(list_block_stone_and_frozen, POS2, cell1, cell2);

			if (boundRight1.C < POS2.C || boundRight2.C < POS1.C) {//2 pokemon o 2 khoang
				logicMoveRight(POS1, boundLeft1.C, boundRight1.C);
				logicMoveRight(POS2, boundLeft2.C, boundRight2.C);
			} else {
				if (POS1.C > POS2.C) {
					logicMoveRight (POS1, boundLeft1.C, boundRight1.C);
					if (POS2.R == POS1.R) {
						return;
					}
					logicMoveRight (POS2, boundLeft2.C, boundRight2.C);
				} else {
					logicMoveRight (POS2, boundLeft2.C, boundRight2.C);
					if (POS2.R == POS1.R) {
						return;
					}
					logicMoveRight (POS1, boundLeft1.C, boundRight1.C);
				}
			}
		} else if (isInBoundPos1) {
			Vec2 boundLeft1 = getBoundLeft(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundRight1 = getBoundRight(list_block_stone_and_frozen, POS1, cell1, cell2);
			logicMoveRight(POS1, boundLeft1.C, boundRight1.C);
		} else if (isInBoundPos2) {
			Vec2 boundLeft2 = getBoundLeft(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundRight2 = getBoundRight(list_block_stone_and_frozen, POS2, cell1, cell2);
			logicMoveRight(POS2, boundLeft2.C, boundRight2.C);
		}
	}

	void logicMoveUpByDirection(Vec2 POS1, Vec2 POS2, Vec2 cell1, Vec2 cell2, ArrayList list_block_stone_and_frozen) {
		bool isInBoundPos1 = isInBound(POS1, cell1, cell2);
		bool isInBoundPos2 = isInBound(POS2, cell1, cell2);
		if (isInBoundPos1 && isInBoundPos2) {
			Vec2 boundBottom1 = getBoundBottom(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundBottom2 = getBoundBottom(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundTop1 = getBoundTop(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundTop2 = getBoundTop(list_block_stone_and_frozen, POS2, cell1, cell2);

			if (boundTop1.R < POS2.R || boundTop2.R < POS1.R) {//2 pokemon o 2 khoang
				logicMoveUp(POS1, boundBottom1.R, boundTop1.R);
				logicMoveUp(POS2, boundBottom2.R, boundTop2.R);
			} else {
				if (POS1.R > POS2.R) {
					logicMoveUp (POS1, boundBottom1.R, boundTop1.R);
					if (POS2.C == POS1.C) {
						return;
					}
					logicMoveUp (POS2, boundBottom2.R, boundTop2.R);
				} else {
					logicMoveUp (POS2, boundBottom2.R, boundTop2.R);
					if (POS2.C == POS1.C) {
						return;
					}
					logicMoveUp (POS1, boundBottom1.R, boundTop1.R);
				}
			}
		} else if (isInBoundPos1) {
			Vec2 boundBottom1 = getBoundBottom(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundTop1 = getBoundTop(list_block_stone_and_frozen, POS1, cell1, cell2);
			logicMoveUp(POS1, boundBottom1.R, boundTop1.R);
		} else if (isInBoundPos2) {
			Vec2 boundBottom2 = getBoundBottom(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundTop2 = getBoundTop(list_block_stone_and_frozen, POS2, cell2, cell2);
			logicMoveUp(POS2, boundBottom2.R, boundTop2.R);
		}
	}

	void logicMoveDownByDirection(Vec2 POS1, Vec2 POS2, Vec2 cell1, Vec2 cell2, ArrayList list_block_stone_and_frozen) {
		bool isInBoundPos1 = isInBound(POS1, cell1, cell2);
		bool isInBoundPos2 = isInBound(POS2, cell1, cell2);
		if (isInBoundPos1 && isInBoundPos2) {
			Vec2 boundBottom1 = getBoundBottom(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundBottom2 = getBoundBottom(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundTop1 = getBoundTop(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundTop2 = getBoundTop(list_block_stone_and_frozen, POS2, cell1, cell2);

			if (boundTop1.R < POS2.R || boundTop2.R < POS1.R) {//2 pokemon o 2 khoang
				logicMoveDown(POS1, boundBottom1.R, boundTop1.R);	
				logicMoveDown(POS2, boundBottom2.R, boundTop2.R);
			} else {
				if (POS1.R < POS2.R) {
					logicMoveDown (POS1, boundBottom1.R, boundTop1.R);
					if (POS2.C == POS1.C) {
						return;
					}
					logicMoveDown (POS2, boundBottom2.R, boundTop2.R);
				} else {
					logicMoveDown (POS2, boundBottom2.R, boundTop2.R);
					if (POS2.C == POS1.C) {
						return;
					}
					logicMoveDown (POS1, boundBottom1.R, boundTop1.R);
				}
			}
		} else if (isInBoundPos1) {
			Vec2 boundBottom1 = getBoundBottom(list_block_stone_and_frozen, POS1, cell1, cell2);
			Vec2 boundTop1 = getBoundTop(list_block_stone_and_frozen, POS1, cell1, cell2);
			logicMoveDown(POS1, boundBottom1.R, boundTop1.R);
		} else if (isInBoundPos2) {
			Vec2 boundBottom2 = getBoundBottom(list_block_stone_and_frozen, POS2, cell1, cell2);
			Vec2 boundTop2 = getBoundTop(list_block_stone_and_frozen, POS2, cell1, cell2);
			logicMoveDown(POS2, boundBottom2.R, boundTop2.R);
		}
	}

	bool isInBound (Vec2 POS, Vec2 cell1, Vec2 cell2) {
		if (POS == null) {
			return false;
		}
		if (Mathf.Min (cell1.C, cell2.C) <= POS.C && POS.C <= Mathf.Max (cell1.C, cell2.C) 
			&& Mathf.Min (cell1.R, cell2.R) <= POS.R && POS.R <= Mathf.Max (cell1.R, cell2.R)) {
			return true;
		}
		return false;
	}

	Vec2 getBoundLeft (ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2) {
		int temp = Mathf.Min(cell1.C, cell2.C) - 1;
		for (int i = 0; i < list_pos.Count; i++) {
			Vec2 pos = (Vec2) list_pos[i];
			if (pos.R == POS.R && pos.C < POS.C && pos.C > temp) {
				temp = pos.C;
			}
		}
		return new Vec2(POS.R, temp + 1);
	}

	Vec2 getBoundRight (ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2) {
		int temp = Mathf.Max(cell1.C, cell2.C) + 1;

		for (int i = 0; i < list_pos.Count; i++) {
			Vec2 pos = (Vec2) list_pos[i];
			if (pos.R == POS.R && pos.C > POS.C && pos.C < temp) {
				temp = pos.C;
			}
		}
		return new Vec2(POS.R, temp - 1);
	}

	Vec2 getBoundTop (ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2) {
		int temp = Mathf.Max(cell1.R, cell2.R) + 1;

		for (int i = 0; i < list_pos.Count; i++) {
			Vec2 pos = (Vec2) list_pos[i];
			if (pos.C == POS.C && pos.R > POS.R && pos.R < temp) {
				temp = pos.R;
			}
		}
		return new Vec2(temp - 1, POS.C);
	}

	Vec2 getBoundBottom (ArrayList list_pos, Vec2 POS, Vec2 cell1, Vec2 cell2) {
		int temp = Mathf.Min(cell1.R, cell2.R) - 1;

		for (int i = 0; i < list_pos.Count; i++) {
			Vec2 pos = (Vec2) list_pos[i];
			if (pos.C == POS.C && pos.R < POS.R && pos.R > temp) {
				temp = pos.R;
			}
		}
		return new Vec2(temp + 1, POS.C);
	}

	void logicMoveUp(Vec2 POS, int row_down, int row_up) {
		ArrayList list_pokemon_col1 = new ArrayList ();
		for (int i = POS.R; i >= row_down; i--) {
			if(map.MAP[i][POS.C] != -1 && map.MAP[i][POS.C] != Const.STONE_FIXED_ID && map.MAP_FROZEN[i][POS.C] == -1) {
				list_pokemon_col1.Add(map.getPokemon(i, POS.C));
			}
		}
		int size1 = list_pokemon_col1.Count;
		int count1 = list_pokemon_col1.Count;
		bool hasDrop = false;
		for (int i = POS.R; i >= row_down; i--) {
			int type = -1;
			if(count1 > 0) {
				GameObject obj = (GameObject)list_pokemon_col1 [size1 - count1];
				Pokemon pokemon = obj.GetComponent<Pokemon>();
				type = pokemon.id;
				if (map.changePokemon (obj, pokemon, new Vec2 (i, POS.C), time_move))
					hasDrop = true;
				count1--;
			}
			map.MAP[i][POS.C] = type;
		}
		if (hasDrop) {
			StartCoroutine (playSoundDrop (time_move));
		}
	}

	void logicMoveDown(Vec2 POS, int row_down, int row_up) {
		ArrayList list_pokemon_col1 = new ArrayList ();
		for (int i = POS.R; i <= row_up; i++) {
			if(map.MAP[i][POS.C] != -1 && map.MAP[i][POS.C] != Const.STONE_FIXED_ID && map.MAP_FROZEN[i][POS.C] == -1) {
				list_pokemon_col1.Add(map.getPokemon(i, POS.C));
			}
		}
		int size1 = list_pokemon_col1.Count;
		int count1 = list_pokemon_col1.Count;
		bool hasDrop = false;
		for (int i = POS.R; i <= row_up; i++) {
			int type = -1;
			if(count1 > 0) {
				GameObject obj = (GameObject)list_pokemon_col1 [size1 - count1];
				Pokemon pokemon = obj.GetComponent<Pokemon>();
				type = pokemon.id;
				if (map.changePokemon (obj, pokemon, new Vec2 (i, POS.C), time_move)) {
					hasDrop = true;
				}
				count1--;
			}
			map.MAP[i][POS.C] = type;
		}
		if (hasDrop) {
			StartCoroutine (playSoundDrop (time_move));
		}
	}

	void logicMoveLeft(Vec2 POS, int col_left, int col_right) {
		ArrayList list_pokemon_row1 = new ArrayList ();
		for (int i = POS.C; i <= col_right; i++) {
			if(map.MAP[POS.R][i] != -1 && map.MAP[POS.R][i] != Const.STONE_FIXED_ID && map.MAP_FROZEN[POS.R][i] == -1) {
				list_pokemon_row1.Add(map.getPokemon(POS.R, i));
			}
		}
		int size1 = list_pokemon_row1.Count;
		int count1 = list_pokemon_row1.Count;
		bool hasDrop = false;
		for (int i = POS.C; i <= col_right; i++) {
			int type = -1;
			if(count1 > 0) {
				GameObject obj = (GameObject)list_pokemon_row1 [size1 - count1];
				Pokemon pokemon = obj.GetComponent<Pokemon>();
				type = pokemon.id;
				if(map.changePokemon(obj, pokemon, new Vec2(POS.R, i), time_move))
					hasDrop = true;
				count1--;
			}
			map.MAP[POS.R][i] = type;
		}
		if (hasDrop) {
			StartCoroutine (playSoundDrop (time_move));
		}
	}

	void logicMoveRight(Vec2 POS, int col_left, int col_right) {
		ArrayList list_pokemon_row1 = new ArrayList ();
		for (int i = POS.C; i >= col_left; i--) {
			if(map.MAP[POS.R][i] != -1 && map.MAP[POS.R][i] != Const.STONE_FIXED_ID && map.MAP_FROZEN[POS.R][i] == -1) {
				list_pokemon_row1.Add(map.getPokemon(POS.R, i));
			}
		}
		int size1 = list_pokemon_row1.Count;
		int count1 = list_pokemon_row1.Count;
		bool hasDrop = false;
		for (int i = POS.C; i >= col_left; i--) {
			int type = -1;
			if(count1 > 0) {
				GameObject obj = (GameObject)list_pokemon_row1 [size1 - count1];
				Pokemon pokemon = obj.GetComponent<Pokemon>();
				type = pokemon.id;
				if (map.changePokemon (obj, pokemon, new Vec2 (POS.R, i), time_move))
					hasDrop = true;
				count1--;
			}
			map.MAP[POS.R][i] = type;
		}
		if (hasDrop) {
			StartCoroutine (playSoundDrop (time_move));
		}
	}

	public void logicMoveRound(Vec2 POS, int offset, int num_step) {
		//dam bao POS thoa man voi offset---chua check TH ko thoa man
//		int num_step = 1;
		for (int i = offset; i > 0 ; i--) {
			ArrayList list_pos = new ArrayList();
			ArrayList list_pokemon = new ArrayList();
			for(int j = POS.R - i; j < POS.R + i; j++) {
				GameObject obj = map.getPokemon(j, POS.C - i);
				list_pos.Add(new Vec2(j, POS.C - i));
				list_pokemon.Add(obj);
			}

			for (int j = POS.C - i; j < POS.C + i; j++) {
				GameObject obj = map.getPokemon(POS.R + i, j);
				list_pos.Add(new Vec2(POS.R + i, j));
				list_pokemon.Add(obj);
			}
			for (int j = POS.R + i; j > POS.R - i; j--) {
				GameObject obj = map.getPokemon(j, POS.C + i);
				list_pos.Add(new Vec2(j, POS.C + i));
				list_pokemon.Add(obj);
			}
			for (int j = POS.C + i; j > POS.C - i; j--) {
				GameObject obj = map.getPokemon(POS.R - i, j);
				list_pos.Add(new Vec2(POS.R - i, j));
				list_pokemon.Add(obj);
			}

			int count = list_pos.Count;
			int index = num_step;

			for(int j = 0; j < list_pokemon.Count; j++) {
				GameObject obj = (GameObject) list_pokemon[j];
				int idx = index % count;
				Vec2 pos = (Vec2) list_pos[idx];
				if(obj == null) {
					map.MAP[pos.R][pos.C] = -1;
				} else {
					Pokemon pokemon = obj.GetComponent<Pokemon>();
					map.changePokemon(obj, pokemon, pos, 0.5f);
				}
				index ++;
			}
		}
	}

	IEnumerator logicLevel16_Down (Vec2 POS1, Vec2 POS2) {
		yield return new WaitForSeconds(0.6f);
		for (int i = 1; i < map.col - 1; i++) {
			logicMoveDown(new Vec2 (1, i), map.row - 2, 1);
		}
//		logicLevel3 (POS1, POS2);
	}

	//map dung yen, vi tri di chuyen o giua man hinh
	void logicLevel20(Vec2 POS1, Vec2 POS2) {
		Vec2 center = new Vec2 (4, 8);
		int offset = 2;
		int num_step = 1;
		logicMoveRound (center, offset, num_step);
	}

	IEnumerator playSoundDrop(float time){
		yield return new WaitForSeconds (time);
		SoundSystem.ins.play_sound_fall_down ();
	}
}
