using ChessBurger.MoveExplorer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessBurger.GameComponents.Pieces
{
    public class PossibleMoveManager
    {
        private List<Cell> _possibleMoves;
        
        public PossibleMoveManager()
        {
            _possibleMoves = new List<Cell>();
        }

        public int PossibleMoveCount
        {
            get { return _possibleMoves.Count; }
        }

        public List<Cell> PossibleMovesClone
        {
            get
            {
                List<Cell> possibleMovesClone = _possibleMoves;
                return possibleMovesClone;
            }
        }

        // return true if the move is in the possible list
        public bool MoveExist(Cell moveToBeCheck)
        {
            foreach (Cell move in _possibleMoves)
            {
                if (move.X == moveToBeCheck.X && move.Y == moveToBeCheck.Y)
                {
                    return true;
                }
            }
            return false;
        }
        
        // reset possible moves
        public void ClearPossibleMoves()
        {
            _possibleMoves.Clear();
        }

        // remove move
        public void RemovePossibleMove(Cell moveToBeRemoved)
        {
            _possibleMoves.Remove(moveToBeRemoved);
        }

        public void GeneratePossibleMoves(int currentPieceX, int currentPieceY, MoveGenerator moveExplorer)
        {
            _possibleMoves = moveExplorer.FindAllPossibleMoves(currentPieceX, currentPieceY);
        }

        // override for pieces require 2 move explorers
        public void GeneratePossibleMoves(int currentPieceX, int currentPieceY, MoveGenerator moveExplorer1, MoveGenerator moveExplorer2)
        {
            List<Cell> moveList1 = moveExplorer1.FindAllPossibleMoves(currentPieceX, currentPieceY);
            List<Cell> moveList2 = moveExplorer2.FindAllPossibleMoves(currentPieceX, currentPieceY);
            _possibleMoves = moveList1.Union(moveList2).ToList();
        }
    }
}
