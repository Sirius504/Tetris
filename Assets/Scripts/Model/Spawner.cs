using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Model.Enumerators;

namespace Tetris.Model
{
    public class Spawner
    {
        private List<TetraminoTypeEnum> tetraminoBag;
        private readonly TetraminoFactory tetraminoFactory;
        private Random random;

        public Spawner(TetraminoFactory tetraminoFactory)
        {
            this.tetraminoFactory = tetraminoFactory;
            random = new Random();

            tetraminoBag = GenerateNewBag();
        }

        private List<TetraminoTypeEnum> GenerateNewBag()
        {
            return Enum.GetValues(typeof(TetraminoTypeEnum))
                .Cast<TetraminoTypeEnum>()
                .ToList();
        }

        public TetraminoTypeEnum Spawn()
        {
            if (tetraminoBag.Count < 1)
                tetraminoBag = GenerateNewBag();
            var result = tetraminoBag[random.Next() % tetraminoBag.Count];
            tetraminoBag.Remove(result);
            return result;
        }
    } 
}
