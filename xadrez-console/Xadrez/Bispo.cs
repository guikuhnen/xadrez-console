using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class Bispo : Peca
    {
        public Bispo(Cor cor, TabuleiroJogo tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString()
        {
            return "B";
        }
    }
}
