using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, TabuleiroJogo tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString()
        {
            return "T";
        }
    }
}
