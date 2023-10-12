using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, TabuleiroJogo tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString()
        {
            return "R";
        }
    }
}
