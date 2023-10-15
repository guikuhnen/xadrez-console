﻿using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class PartidaXadrez
    {
        public TabuleiroJogo TabuleiroJogo { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada {  get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;
        public bool Xeque { get; private set; }

        public PartidaXadrez()
        {
            TabuleiroJogo = new TabuleiroJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroJogo.RetirarPeca(origem);

            peca.IncrementarQteMovimentos();

            Peca pecaCapturada = TabuleiroJogo.RetirarPeca(destino);

            TabuleiroJogo.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
                pecasCapturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pecaAtual = TabuleiroJogo.RetirarPeca(destino);

            pecaAtual.DecrementarQteMovimentos();

            if (pecaCapturada != null)
            {
                TabuleiroJogo.ColocarPeca(pecaCapturada, destino);
                pecasCapturadas.Remove(pecaCapturada);
            }

            TabuleiroJogo.ColocarPeca(pecaAtual, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            Turno++;

            MudaJogador();
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            Peca pecaAtual = TabuleiroJogo.ObterPeca(posicao);

            if (pecaAtual == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (JogadorAtual != pecaAtual.Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");

            if (!pecaAtual.ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }
        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            Peca pecaAtual = TabuleiroJogo.ObterPeca(origem);

            if (!pecaAtual.PodeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca item in pecasCapturadas)
                if (item.Cor == cor)
                    aux.Add(item);

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca item in pecas)
                if (item.Cor == cor)
                    aux.Add(item);

            aux.ExceptWith(PecasCapturadas(cor));

            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca Rei (Cor cor)
        {
            foreach (Peca item in PecasEmJogo(cor))
            {
                if (item is Rei)
                    return item;
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);

            if (rei == null)
                throw new TabuleiroException(string.Format("Não tem Rei da cor {0} no tabuleiro!", cor));

            foreach (Peca item in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = item.MovimentosPossiveis();

                if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }

            return false;
        }

        public void ColocarNovaPeca(int linha, char coluna, Peca peca)
        {
            TabuleiroJogo.ColocarPeca(peca, new PosicaoXadrez(linha, coluna).ToPosicao());
            pecas.Add(peca);
        }
        
        private void ColocarPecas()
        {
            ColocarNovaPeca(1, 'c', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(2, 'c', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(2, 'd', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(2, 'e', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'e', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'd', new Rei(Cor.Branca, TabuleiroJogo));

            ColocarNovaPeca(7, 'c', new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'c', new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(7, 'd', new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(7, 'e', new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'e', new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'd', new Rei(Cor.Preta, TabuleiroJogo));
        }
    }
}
