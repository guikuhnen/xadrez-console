using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class Dama : Peca
    {
        public Dama(Cor cor, TabuleiroJogo tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString()
        {
            return "D";
        }
    }
}
