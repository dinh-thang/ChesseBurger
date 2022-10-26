﻿using System.Collections.Generic;
using System.Linq;
using ChessBurger.Board;
using ChessBurger.MoveExplorer;

namespace ChessBurger.Pieces
{
    public class King : Piece
    {
        private CastleMove _castleMoveExplorer;
        private KingMove _kingMoveExplorer;

        public King(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            _kingMoveExplorer = new KingMove();
            _castleMoveExplorer = new CastleMove(); 
            
            PossibleMoves = GenerateMoves();
        }

        public override List<Cell> GenerateMoves()
        {
            List<Cell> defaultMoves = _kingMoveExplorer.FindAllPossibleMoves(X, Y);    
            List<Cell> castleMoves = _castleMoveExplorer.FindAllPossibleMoves(X, Y);
            PossibleMoves = Enumerable.Concat(defaultMoves, castleMoves).ToList();

            return PossibleMoves;
        }
    }
}