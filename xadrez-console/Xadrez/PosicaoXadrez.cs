using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(int linha, char coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
