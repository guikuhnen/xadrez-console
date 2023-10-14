using xadrez_console.Tabuleiro;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Tabuleiro de Xadrez
                TabuleiroJogo tabuleiro = new TabuleiroJogo(8, 8);

                tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
                tabuleiro.ColocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(0, 2));

                tabuleiro.ColocarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(3, 5));
                Tela.ImprimirTabuleiro(tabuleiro);

                //PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);

                //Console.WriteLine(posicaoXadrez);

                //Console.WriteLine(posicaoXadrez.ToPosicao());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}