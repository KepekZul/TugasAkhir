using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Program
{
    class LocalDirectionalPattern
    {
        int[,,] kirschMask;
        Bitmap originalImage;
        int[] originalMatrix;
        List<List<int>> ldpResult;
        private void init()
        {
            this.kirschMask = new int[8, 3, 3] {   { {-3, -3, 5}, {-3, 0, 5}, {-3, -3, 5} },
                                              { {-3, 5,  5}, {-3, 0, 5}, {-3, -3,-3} },
                                              { { 5,  5, 5}, {-3, 0,-3}, {-3,-3, -3} },
                                              { { 5, 5, -3}, {5, 0, -3}, {-3,-3, -3} },
                                              { { 5,-3, -3}, {5, 0, -3}, { 5,-3, -3} },
                                              { { -3,-3,-3}, {5, 0, -3}, { 5, 5, -3} },
                                              { {-3,-3,-3 }, {-3, 0,-3}, { 5, 5,  5} },
                                              { { -3,-3,-3}, {-3, 0, 5}, {-3, 5,  5} },
                                          };
            this.ldpResult = new List<List<int>>();
        }
        public LocalDirectionalPattern(Bitmap inputImage)
        {
            init();
            this.originalImage = new Bitmap(inputImage);
        }
        public LocalDirectionalPattern(int[] inputMatrix)
        {
            init();
            this.originalMatrix = inputMatrix;
        }
        private string maskConvolute()
        {
            string ldpCode = "";

            return ldpCode;
        }
    }
}
