namespace AdventOfCode.Day17Lib
{
    internal class Grid
    {
        private const int DEFAULT_OFFSET = 3;
        private const int MAX_FIELD_WIDTH = 8;
        private const int MIN_FIELD_WIDTH = 3;
        private const int DEFAULT_EXTRA_ROWS = 3;
        
        public List<List<char>> GridField { get; set; }
        public Rock CurrentRock { get; set; }
        public List<Rock> OldRocks { get; set; }
        public int RockCounter { get; set; } = 0;


        public Grid()
        {
            GridField =  new List<List<char>>();
            
            CurrentRock = Rock.GetNextRock(RockCounter);
            GridField.Add(GenerateFloor());
            MakeSpace(CurrentRock.GetLength() + DEFAULT_EXTRA_ROWS, 0);
            InsertRock();

        }

        public void NextActiveRock() 
        {
            RockCounter++;
            CurrentRock = Rock.GetNextRock(RockCounter);
            MakeSpace(CurrentRock.GetLength() + DEFAULT_EXTRA_ROWS, 0);
            CurrentRock.X = DEFAULT_OFFSET;
        }
            
        
        /// <summary>
        /// Inserts a rock at the specified offset from the left
        /// </summary>
        /// <param name="offset"></param>
        private void InsertRock()
        {
            for (int i = 0; i < CurrentRock.RockShape.Count; i++)
            {
                var gridRowOffset = CurrentRock.RockShape[i].y + CurrentRock.Y;
                var gridColOffset = CurrentRock.RockShape[i].x + CurrentRock.X;
                GridField[gridRowOffset][gridColOffset] = CurrentRock.Material;
                
            }
        }

        public void MakeSpace(int rowAmount, int index)
        {
            for (int j = 0; j < rowAmount; j++)
            {
                var layer = new List<char>() {'|'};
                for (int i = 0; i < 7; i++)
                {
                    layer.Add('.');
                }

                layer.Add('|');
            
                GridField.Insert(index, layer);
            }
        }

        private static List<char> GenerateFloor()
        {
            var floor = new List<char>();
            floor.Add('+');
            for (int i = 0; i < 7; i++)
            {
                floor.Add('-');
            }
            floor.Add('+');
            return floor;
        }

        internal void BlowJet(char chevron)
        {
            // if there is no left or right collision, move the rock
            var direction = chevron == '>' ? Direction.Right : Direction.Left;
            if (!HasCollision(direction))
            {
                ClearGrid();
                MoveRock(CurrentRock, direction);
            }
            InsertRock();
        }

        internal bool MoveDown()
        {
            var hasCollision = HasCollision(Direction.Down);
            // if there is no down collision, move the rock
            
            if (!hasCollision)
            {
                ClearGrid();
                MoveRock(CurrentRock, Direction.Down);
                RemoveTop();
            } else
            {
                CurrentRock.Material = '#';
            }
            InsertRock();
            return hasCollision;
        }

        /// <summary>
        /// Based on the location on the grid, delete the rock
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ClearGrid()
        {
            var shapeCountY = CurrentRock.RockShape.Select((x) => x.y).Distinct().Count();
            GridField.RemoveRange(CurrentRock.Y, shapeCountY);
            MakeSpace(shapeCountY, 0);
        }

        internal void Draw()
        {
            Console.Clear();
            for (int i = 0; i < GridField.Count; i++)
            {
                for (int j = 0; j < GridField[i].Count; j++)
                {
                    Console.Write(GridField[i][j]);
                }
                Console.WriteLine();
            }
        }

        public void RemoveTop()
        {
            GridField.RemoveAt(0);
        }

        private void MoveRock(Rock rock, Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    rock.X--;
                    break;
                case Direction.Right:
                    rock.X++;
                    break;
                case Direction.Up:
                    rock.Y--;
                    break;
                case Direction.Down:
                    //rock.Y;
                    break;
            }
        }

        /// <summary>
        /// This method checks collision for a specific direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool HasCollision(Direction direction)
        {
            return direction switch
            {
                Direction.Left => CheckCols(Direction.Left),
                Direction.Right => CheckCols(Direction.Right),
                Direction.Down => CheckRows(),
                _ => throw new ArgumentException("this value does not exist")
            };
        }

        private bool CheckCols(Direction direction)
        {
            var dir = direction == Direction.Left ? 0 : CurrentRock.GetWidth() + 1;
            var result = true;
            var nextLocationX = (CurrentRock.X + dir);
            if (nextLocationX > 1 && nextLocationX <= MAX_FIELD_WIDTH && GridField[CurrentRock.Y + CurrentRock.GetLength() - 1][CurrentRock.X + dir - 1] != '#')
            {
                result = false;
            }

            return result;
        }

        private bool CheckRows()
        {
            var result = true;
            var nextLocationY = (CurrentRock.Y + CurrentRock.GetLength());
            var oneAboveFloorY = GridField.Count - 1; // Not super clear, but essentially we have a floor row on line 7 so the bottom is one above otherwise we remove the floor.
        
            if (nextLocationY < oneAboveFloorY && !CollisionLowestRow(nextLocationY))
            {
                result = false;
            }

            return result;
        }

        private bool CollisionLowestRow(int nextLocationY)
        {
            // Check for all the @ in the bottom row of the shape, whether it overlaps with a # or not
            var result = false;
            var maxValue = CurrentRock.RockShape.Max(c => c.y);
            var bottomShapeRow = CurrentRock.RockShape.Where(c => c.y == maxValue).ToList();
            for (int i = 0; i < bottomShapeRow.Count; i++)
            {
                if (GridField[nextLocationY][CurrentRock.X + bottomShapeRow[i].x] == '#')
                {
                    result = true;
                    break;
                }
            }
            //return GridField[nextLocationY][CurrentRock.X + (CurrentRock.GetWidth() - 1)] != '#';
            return result;
        }
    }
}
