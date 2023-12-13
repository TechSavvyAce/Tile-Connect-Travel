using UnityEngine;
using System.Collections;

public class ConstraintData {
	public int direction;
	public Vec2 cell1;
	public Vec2 cell2;
	public ConstraintData(int direction, Vec2 cell1, Vec2 cell2) {
		this.direction = direction;
		this.cell1 = new Vec2(cell1);
		this.cell2 = new Vec2(cell2);
	}
}
