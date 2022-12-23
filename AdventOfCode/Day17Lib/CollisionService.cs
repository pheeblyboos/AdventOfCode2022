namespace AdventOfCode.Day17Lib
{
    internal class CollisionService
    {
        private const int MAX_FIELD_WIDTH = 8;
        
        /// <summary>
        /// This method checks collision for a specific direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static bool HasCollision(Direction direction, Grid grid)
        {
            return direction switch
            {
                Direction.Left => CheckCols(Direction.Left, grid),
                Direction.Right => CheckCols(Direction.Right, grid),
                Direction.Down => CheckRows(grid),
                _ => throw new ArgumentException("this value does not exist")
            };
        }

        private static bool CheckCols(Direction direction, Grid grid)
        {
            var dir = direction == Direction.Left ? -1 : 1;
            var result = true;
            var nextLocationX = (grid.CurrentRock.X + dir);
            if (nextLocationX >= 1 && nextLocationX + grid.CurrentRock.GetWidth() <= MAX_FIELD_WIDTH && !CollisionSides(nextLocationX, dir, grid))
            {
                result = false;
            }
            
            return result;
        }
        
        private static bool CollisionSides(int nextLocationX, int dir, Grid grid)
        {
            var result = false;
            var sideRow = grid.CurrentRock.RockShape.GroupBy(x => x.y).Select(x => x.First(r => r.x == SelectSide(x, dir))).ToList();
            for (int i = 0; i < sideRow.Count; i++)
            {
                if (grid.GridField[grid.CurrentRock.Y + sideRow[i].y][nextLocationX + sideRow[i].x] == '#')
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private static int SelectSide(IGrouping<int, (int x, int y)> x, int dir)
        {
            return dir == -1 ? x.Min(r => r.x) : x.Max(r => r.x);
        }

        private static bool CheckRows(Grid grid)
        {
            var result = true;
            var nextLocationY = (grid.CurrentRock.Y + grid.CurrentRock.GetLength());
            var oneAboveFloorY = grid.GridField.Count - 1; // Not super clear, but essentially we have a floor row on line 7 so the bottom is one above otherwise we remove the floor.

            if (nextLocationY < oneAboveFloorY && !CollisionLowestRow(nextLocationY, grid))
            {
                result = false;
            }

            return result;
        }

        private static bool CollisionLowestRow(int nextLocationY, Grid grid)
        {
            // Check for all the @ in the bottom row of the shape, whether it overlaps with a # or not
            var result = false;
            var bottomShapeRow = grid.CurrentRock.RockShape.GroupBy(x => x.x).Select(x => x.First(r => r.y == x.Max(c => c.y))).ToList();
            for (int i = 0; i < bottomShapeRow.Count; i++)
            {
                if (grid.GridField[grid.CurrentRock.Y + bottomShapeRow[i].y + 1][grid.CurrentRock.X + bottomShapeRow[i].x] == '#')
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
