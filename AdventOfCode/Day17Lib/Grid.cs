namespace AdventOfCode.Day17Lib
{
    internal class Grid
    {
        private const int DEFAULT_OFFSET = 3;
        private const int DEFAULT_EXTRA_ROWS = 3;
        
        public List<List<char>> GridField { get; set; }
        public Rock CurrentRock { get; set; }
        public List<Rock> OldRocks { get; set; }
        public long RockCounter { get; set; } = 0;


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
            if (!CollisionService.HasCollision(direction, this))
            {
                ClearGrid();
                MoveRock(CurrentRock, direction);
            }
            InsertRock();
        }

        internal bool MoveDown()
        {
            var hasCollision = CollisionService.HasCollision(Direction.Down, this);
            // if there is no down collision, move the rock
            
            if (!hasCollision)
            {
                ClearGrid();
                RemoveTop();
                MoveRock(CurrentRock, Direction.Down);
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
            var shapeCountY = CurrentRock.Y + CurrentRock.GetLength();
            for (int i = CurrentRock.Y; i < shapeCountY; i++)
            {
                for (int j = 0; j < GridField[i].Count; j++)
                {
                    if (GridField[i][j] == '@')
                    {
                        GridField[i][j] = '.';
                    }
                }
            }
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
            if (!GridField[0].Contains('#'))
            {
                GridField.RemoveAt(0);
                return;
            }

            CurrentRock.Y++;

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
            }
        }
    }
}
