using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMatrixGame.Client.Pages
{
    public partial class Game
    {
        public int Length { get; set; }
        public bool isVertClicked { get; set; }

        public string PlayerOne = "X";
        public string PlayerTwo = "O";
        public string CurentPlayer = "";

        public int DuplaProvjera = 0;

        public int[,] HorMatrix;
        public int[,] VertMatrix;
        public int[,] PoljeMatrix;
        public string[,] PoljeMatrixString;

        protected override Task OnParametersSetAsync()
        {
            CurentPlayer = PlayerOne;
            Length = 5;
            HorMatrix = new int[Length + 1, Length];
            VertMatrix = new int[Length, Length + 1];
            PoljeMatrix = new int[Length, Length];
            PoljeMatrixString = new string[Length, Length];

            PrvoPopunjavanjeMatrice();
            IspisMatrice();

            return base.OnParametersSetAsync();
        }

        public void NaKojeVrijednostiDjelujeCrta(int VertOrHor, int HorValue, int VertValue)
        {
            DuplaProvjera = 0;

            if (VertOrHor == 1)
            {
                isVertClicked = true;
                VertMatrix[HorValue - 1, VertValue - 1] = 1;
            }
            else
            {
                isVertClicked = false;
                HorMatrix[HorValue - 1, VertValue - 1] = 1;
            }

            Console.WriteLine("Kliknuta je linija: " + HorValue + " " + VertValue + " , " + VertOrHor);


            if (isVertClicked) 
            { 
                Console.WriteLine("Linija djeluje na polje: " + HorValue + " " + VertValue + " & " + HorValue + " " + (VertValue - 1));
                ProvjeriVrijednostiPolja(HorValue, VertValue);
                ProvjeriVrijednostiPolja(HorValue, VertValue - 1);
            }
            else
            {
                Console.WriteLine("Linija djeluje na polje: " + HorValue + " " + VertValue + " & " + (HorValue - 1) + " " + VertValue);
                ProvjeriVrijednostiPolja(HorValue, VertValue);
                ProvjeriVrijednostiPolja(HorValue - 1, VertValue);
            }

            if(DuplaProvjera == 0)
                ChangePlayer();

            StateHasChanged();
        }

        public void IspisMatrice()
        {
            Console.WriteLine("Horizontalna: ");

            int rowLengthH = HorMatrix.GetLength(0);
            int colLengthH = HorMatrix.GetLength(1);
            for (int i = 0; i < rowLengthH; i++)
            {
                for (int j = 0; j < colLengthH; j++)
                {
                    Console.Write(string.Format("{0} ", HorMatrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine("Vertikalna: ");

            int rowLengthV = VertMatrix.GetLength(0);
            int colLengthV = VertMatrix.GetLength(1);
            for (int i = 0; i < rowLengthV; i++)
            {
                for (int j = 0; j < colLengthV; j++)
                {
                    Console.Write(string.Format("{0} ", VertMatrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine("Polja: ");

            int rowLengthP = PoljeMatrix.GetLength(0);
            int colLengthP = PoljeMatrix.GetLength(1);
            for (int i = 0; i < rowLengthP; i++)
            {
                for (int j = 0; j < colLengthP; j++)
                {
                    Console.Write(string.Format("{0} ", PoljeMatrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public void PrvoPopunjavanjeMatrice()
        {
            int rowLengthH = HorMatrix.GetLength(0);
            int colLengthH = HorMatrix.GetLength(1);
            for (int i = 0; i < rowLengthH; i++)
            {
                for (int j = 0; j < colLengthH; j++)
                {
                    if (i == 0 || i == rowLengthH - 1)
                    {
                        HorMatrix[i, j] = 1;
                    }
                }
            }

            int rowLengthV = VertMatrix.GetLength(0);
            int colLengthV = VertMatrix.GetLength(1);
            for (int i = 0; i < rowLengthV; i++)
            {
                for (int j = 0; j < colLengthV; j++)
                {
                    if (j == 0 || j == colLengthV - 1)
                    {
                        VertMatrix[i, j] = 1;
                    }
                }
            }
        }

        public void ProvjeriVrijednostiPolja(int HorValue, int VertValue)
        {
            Console.WriteLine(HorValue + " | " + VertValue);
            Console.WriteLine("Ovo polje gleda vrijednosti:");
            Console.WriteLine("Left: " + HorValue + " | " + VertValue);
            Console.WriteLine("Top: " + HorValue + " | " + VertValue);
            Console.WriteLine("Right: " + HorValue + " | " + (VertValue + 1));
            Console.WriteLine("Bottom: " + (HorValue + 1) + " | " + VertValue);

            int counter = 0;
            if (VertMatrix[HorValue - 1, VertValue - 1] == 1)
                counter++;
            if (HorMatrix[HorValue - 1, VertValue - 1] == 1)
                counter++;
            if (VertMatrix[HorValue - 1, VertValue] == 1)
                counter++;
            if (HorMatrix[HorValue, VertValue - 1] == 1)
                counter++;
            
            Console.WriteLine("Value: " + counter);

            if (counter == 4)
            {
                PoljeMatrix[HorValue - 1, VertValue - 1] = 1;
                PoljeMatrixString[HorValue - 1, VertValue - 1] = CurentPlayer;
                DuplaProvjera++;
            }

            IspisMatrice();
        }

        public void GetValueFromMatrix()
        {
            int rowLengthP = PoljeMatrix.GetLength(0);
            int colLengthP = PoljeMatrix.GetLength(1);
            for (int i = 0; i < rowLengthP; i++)
            {
                for (int j = 0; j < colLengthP; j++)
                {
                    Console.Write(string.Format("{0} ", PoljeMatrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public void ChangePlayer()
        {
            if (CurentPlayer == PlayerOne)
                CurentPlayer = PlayerTwo;
            else
                CurentPlayer = PlayerOne;
        }
    }
}
