using xadrez_console.Tabuleiro;

namespace xadrez_console
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(TabuleiroJogo tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    Peca peca = tabuleiro.ObterPeca(i, j);

                    if (peca == null)
                        Console.Write("- ");
                    else
                        Console.Write(tabuleiro.ObterPeca(i,j) + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
