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

        public override bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.ObterPeca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            throw new NotImplementedException();
        }
    }
}
