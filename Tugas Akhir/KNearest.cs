using System;
using System.Collections.Generic;
using Tugas_Akhir.Selfmade_DataStructure;

namespace Tugas_Akhir
{
    class KNearest
    {
        int[] dataTest;
        List<int[]> dataTrain;
        List<Couple> chiDistances;
        int K_constanta;
        KNearest(int[] dataTest, List<int[]> dataTrain, string[] labelTrain, int K_constanta)
        {
            this.dataTest = dataTest;
            this.K_constanta = K_constanta;
            if (dataTrain.Count != labelTrain.Length)
            {
                throw new ArgumentException("Number of feature not equal to number of label");
            } else
            {
                this.dataTrain = dataTrain;
                for (int i = 0; i < labelTrain.Length; i++)
                {
                    chiDistances.Add(new Couple(0, labelTrain[i]));
                }
            }
        }
        private void calculateDistances()
        {
            int counter = 0;
            foreach (int[] feature in dataTrain)
            {
                ChiSquareDissimilarity chiObj = new ChiSquareDissimilarity(this.dataTest, feature);
                chiDistances[counter].Distance = chiObj.CalculateDissimilarityValue();
                counter++;
            }
            chiDistances.Sort((s1, s2) => s1.Distance.CompareTo(s2.Distance));
        }
        private string getClass()
        {
            calculateDistances();
            chiDistances.Sort((s1, s2) => s1.);
            return getMostNeighbour();
        }
        private string getMostNeighbour()
        {
            List<Pair> topK = new List<Pair>();
            for(int i=0; i<this.K_constanta; i++)
            {
                for(int j=0; j<topK.Count; j++)
                {
                    if (topK[j].Key == chiDistances[i].Label)
                    {
                        topK[j].Value++;
                        break;
                    }
                    if (j == this.K_constanta - 1)
                    {
                        Pair pairObj = new Pair(chiDistances[i].Label);
                    }
                }
            }
            topK.Sort((s1, s2) => s1.Value.CompareTo(s2.Value));
            return topK[0].Key;
        }
    }
}
