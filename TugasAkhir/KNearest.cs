using System;
using System.Collections.Generic;
using Tugas_Akhir.Model;

namespace Tugas_Akhir
{
    class KNearest
    {
        private byte[,] _dataTest;
        private List<byte[,]> _dataTrain;
        private List<Couple> _chiDistances;
        private readonly int K_CONSTANTA;
        private readonly int NUMBER_OF_FRAGMENT;
        public KNearest(byte[,] dataTest, List<byte[,]> dataTrain, List<string> labelTrain, int K_constanta, int numberOfFragment)
        {
            NUMBER_OF_FRAGMENT = numberOfFragment;
            _dataTest = dataTest;
            K_CONSTANTA = K_constanta;
            if (dataTrain.Count != labelTrain.Count)
            {
                throw new ArgumentException("Number of feature not equal to number of label");
            }
            else
            {
                _chiDistances = new List<Couple>();
                _dataTrain = dataTrain;
                for (int i = 0; i < labelTrain.Count; i++)
                {
                    _chiDistances.Add(new Couple(0, labelTrain[i]));
                }
            }
        }
        private void CalculateDistances()
        {
            for (int i = 0; i < _dataTrain.Count; i++)
            {
                ChiSquareDissimilarity chiObj = new ChiSquareDissimilarity(_dataTest, _dataTrain[i], NUMBER_OF_FRAGMENT);
                
                _chiDistances[i].Distance = chiObj.CalculateDissimilarityValue();
            }
            _chiDistances.Sort((s1, s2) => s1.Distance.CompareTo(s2.Distance));
        }
        public string GetClass()
        {
            CalculateDistances();
            return GetMostNeighbour();
        }
        private string GetMostNeighbour()
        {
            List<Pair> topK = new List<Pair>();
            for(int i=0; i<K_CONSTANTA; i++)
            {
                topK.Add(new Pair(_chiDistances[i].Label));
            }
            for(int i=0; i<K_CONSTANTA; i++)
            {
                for(int j=0; j<topK.Count; j++)
                {
                    if (topK[j].Key == _chiDistances[i].Label)
                    {
                        topK[j].Value++;
                        break;
                    }
                    if (j == K_CONSTANTA - 1)
                    {
                        Pair pairObj = new Pair(_chiDistances[i].Label);
                        topK.Add(pairObj);
                    }
                }
            }
            topK.Sort((s1, s2) => s1.Value.CompareTo(s2.Value));
            return topK[0].Key;
        }
    }
}
