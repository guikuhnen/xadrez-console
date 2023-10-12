using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class Peao : Peca
    {
        public Peao(Cor cor, TabuleiroJogo tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString()
        {
            return "P";
        }
    }
}
