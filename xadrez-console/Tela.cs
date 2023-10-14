using xadrez_console.Tabuleiro;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partidaXadrez)
        {
            ImprimirTabuleiro(partidaXadrez.TabuleiroJogo);

            Console.WriteLine();
            ImprimirPecasCapturadas(partidaXadrez);

            Console.WriteLine();
            Console.WriteLine("Turno: " + partidaXadrez.Turno);
            Console.WriteLine("Aguardando jogada: " + partidaXadrez.JogadorAtual);
        }

        private static void ImprimirPecasCapturadas(PartidaXadrez partidaXadrez)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partidaXadrez.PecasCapturadas(Cor.Branca));

            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partidaXadrez.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;

            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca item in conjunto)
                Console.Write(item + " ");
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(TabuleiroJogo tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                    ImprimirPeca(tabuleiro.ObterPeca(i, j));

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(TabuleiroJogo tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;

                    ImprimirPeca(tabuleiro.ObterPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(linha, coluna);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}
