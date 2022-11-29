using System.ComponentModel.Design;

namespace MineSweeper.Core
{
    public class Field
    {
        public const int LengthOfField = 14;
        public const int HeightOfField = 18;
        private readonly int _countOfHidedMines = 40;
        private int _countOfFoundMines = 0;
        private readonly Random _random;

        public string[,] MineSweeperField { get; }
        public string[,] RevealedMineSweeperField { get; }
        public bool GameOver { get; set; }
        public bool GameIsWinner { get; set; }

        public Field()
        {
            _random = new Random();
            MineSweeperField = new string[LengthOfField, HeightOfField];

            for (var i = 0; i < LengthOfField; i++)
            {
                for (var j = 0; j < HeightOfField; j++)
                {
                    MineSweeperField[i, j] = "*";
                }
            }
            RevealedMineSweeperField = new string[LengthOfField, HeightOfField];
            CreateRevealedField();
        }

        private void CreateRevealedField()
        {
            Array.Copy(MineSweeperField, RevealedMineSweeperField, LengthOfField * HeightOfField);
            HideMines();
            EnterCountOfMinesAround();
        }

        public Field(Random random) : this()
        {
            _random = random;
            _countOfHidedMines = 1;
            CreateRevealedField();
        }

        private void EnterCountOfMinesAround()
        {
            for (var i = 0; i < LengthOfField; i++)
            {
                for (var j = 0; j < HeightOfField; j++)
                {
                    if (!FieldIsAMine(i, j))
                        RevealedMineSweeperField[i, j] = GetCountOfMinesAround(i, j).ToString();
                }
            }
        }

        private void HideMines()
        {
            var countOfAlreadyHidedMines = 0;

            while (_countOfHidedMines != countOfAlreadyHidedMines)
            {
                var lengthRandom = _random.Next(0, LengthOfField - 1);
                var heightRandom = _random.Next(0, HeightOfField - 1);
                if (FieldIsAMine(lengthRandom, heightRandom))
                    continue;

                SetMine(lengthRandom, heightRandom);
                
                countOfAlreadyHidedMines++;
            }
        }

        private void SetMine(int length, int height)
        {
            RevealedMineSweeperField[length, height] = "+";
        }


        public void MarkOneFieldAsAMine(int length, int height)
        {
            if (MineSweeperField[length, height] == "+")
            {
                MineSweeperField[length, height] = "*";

                if (FieldIsAMine(length, height)) _countOfFoundMines--;
            }
            else
            {
                MineSweeperField[length, height] = "+";

                if (FieldIsAMine(length, height)) _countOfFoundMines++;

                if (_countOfFoundMines == _countOfHidedMines) GameIsWinner = true;
            }
        }

        public void SweepOneField(int length, int height)
        {
            if (FieldIsAMine(length, height)) GameOver = true;
            else MineSweeperField[length, height] = RevealedMineSweeperField[length, height];
        }

        private bool FieldIsAMine(int length, int height)
        {
            return RevealedMineSweeperField[length, height] == "+";
        }

        private int GetCountOfMinesAround(int length, int height)
        {
            var neighbours = new string[8];

            neighbours[0] = IsMineInitialized(length - 1, height - 1) ? RevealedMineSweeperField[length - 1, height - 1] : "*";
            neighbours[1] = IsMineInitialized(length - 1, height) ? RevealedMineSweeperField[length - 1, height] : "*";
            neighbours[2] = IsMineInitialized(length - 1, height + 1) ? RevealedMineSweeperField[length - 1, height + 1] : "*";
            neighbours[3] = IsMineInitialized(length, height - 1) ? RevealedMineSweeperField[length, height - 1] : "*";
            neighbours[4] = IsMineInitialized(length, height + 1) ? RevealedMineSweeperField[length, height + 1] : "*";
            neighbours[5] = IsMineInitialized(length + 1, height - 1) ? RevealedMineSweeperField[length + 1, height - 1] : "*";
            neighbours[6] = IsMineInitialized(length + 1, height) ? RevealedMineSweeperField[length + 1, height] : "*";
            neighbours[7] = IsMineInitialized(length + 1, height + 1) ? RevealedMineSweeperField[length + 1, height + 1] : "*";

            return neighbours.Count(neighbour => neighbour == "+");
        }

        private static bool IsMineInitialized(int length, int height) =>
            !(length is >= LengthOfField or < 0 || height is >= HeightOfField or < 0);
    }
}
