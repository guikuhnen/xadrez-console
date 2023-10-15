using xadrez_console.Tabuleiro;

namespace xadrez_console.Xadrez
{
    internal class PartidaXadrez
    {
        public TabuleiroJogo TabuleiroJogo { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            TabuleiroJogo = new TabuleiroJogo(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
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

            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = TabuleiroJogo.RetirarPeca(origemT);
                T.IncrementarQteMovimentos();
                TabuleiroJogo.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = TabuleiroJogo.RetirarPeca(origemT);
                T.IncrementarQteMovimentos();
                TabuleiroJogo.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (peca.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = TabuleiroJogo.RetirarPeca(posP);
                    pecasCapturadas.Add(pecaCapturada);
                }
            }

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

            // #jogadaespecial roque pequeno
            if (pecaAtual is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = TabuleiroJogo.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                TabuleiroJogo.ColocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (pecaAtual is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = TabuleiroJogo.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                TabuleiroJogo.ColocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (pecaAtual is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = TabuleiroJogo.RetirarPeca(destino);
                    Posicao posP;
                    if (pecaAtual.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    TabuleiroJogo.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            Peca peca = TabuleiroJogo.ObterPeca(destino);

            // #jogadaespecial promocao
            if (peca is Peao)
            {
                if ((peca.Cor == Cor.Branca && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7))
                {
                    peca = TabuleiroJogo.RetirarPeca(destino);
                    pecas.Remove(peca);
                    Peca dama = new Dama(peca.Cor, TabuleiroJogo);
                    TabuleiroJogo.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;

                MudaJogador();
            }

            // #jogadaespecial en passant
            if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                VulneravelEnPassant = peca;
            else
                VulneravelEnPassant = null;
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

            if (!pecaAtual.MovimentoPossivel(destino))
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

        private Peca Rei(Cor cor)
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

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca item in PecasEmJogo(cor))
            {
                bool[,] mat = item.MovimentosPossiveis();

                for (int i = 0; i < TabuleiroJogo.Linhas; i++)
                    for (int j = 0; j < TabuleiroJogo.Colunas; j++)
                        if (mat[i, j])
                        {
                            Posicao posicaoOrigem = item.Posicao;
                            Posicao posicaoDestino = new Posicao(i, j);

                            Peca pecaCapturada = ExecutaMovimento(posicaoOrigem, posicaoDestino);

                            bool testeXeque = EstaEmXeque(cor);

                            DesfazMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);

                            if (!testeXeque)
                                return false;
                        }
            }

            return true;
        }

        public void ColocarNovaPeca(int linha, char coluna, Peca peca)
        {
            TabuleiroJogo.ColocarPeca(peca, new PosicaoXadrez(linha, coluna).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca(1, 'a', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'b', new Cavalo(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'c', new Bispo(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'd', new Dama(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'e', new Rei(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(1, 'f', new Bispo(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'g', new Cavalo(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(1, 'h', new Torre(Cor.Branca, TabuleiroJogo));
            ColocarNovaPeca(2, 'a', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'b', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'c', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'd', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'e', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'f', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'g', new Peao(Cor.Branca, TabuleiroJogo, this));
            ColocarNovaPeca(2, 'h', new Peao(Cor.Branca, TabuleiroJogo, this));

            ColocarNovaPeca(8, 'a',  new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'b', new Cavalo(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'c', new Bispo(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'd', new Dama(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'e', new Rei(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(8, 'f', new Bispo(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'g', new Cavalo(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(8, 'h', new Torre(Cor.Preta, TabuleiroJogo));
            ColocarNovaPeca(7, 'a', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'b', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'c', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'd', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'e', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'f', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'g', new Peao(Cor.Preta, TabuleiroJogo, this));
            ColocarNovaPeca(7, 'h', new Peao(Cor.Preta, TabuleiroJogo, this));
        }
    }
}
