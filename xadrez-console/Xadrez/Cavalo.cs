using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class Cavalo : Peca
    {
        public Cavalo(Cor cor, TabuleiroJogo tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString()
        {
            return "C";
        }
    }
}
