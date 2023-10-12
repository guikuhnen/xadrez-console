using xadrez_console.Tabuleiro;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TabuleiroJogo tabuleiro = new TabuleiroJogo(8,8);

            Tela.ImprimirTabuleiro(tabuleiro);


        }
    }
}