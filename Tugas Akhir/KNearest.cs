using System;
using System.Collections.Generic;
using Tugas_Akhir.Selfmade_DataStructure;

namespace Tugas_Akhir
{
    class KNearest
    {
        byte[,] dataTest;
        List<byte[,]> dataTrain;
        List<Couple> chiDistances;
        int K_constanta;
        public KNearest(byte[,] dataTest, List<byte[,]> dataTrain, List<string> labelTrain, int K_constanta)
        {
            this.dataTest = dataTest;
            this.K_constanta = K_constanta;
            if (dataTrain.Count != labelTrain.Count)
            {
                throw new ArgumentException("Number of feature not equal to number of label");
            } else
            {
                chiDistances = new List<Couple>();
                this.dataTrain = dataTrain;
                for (int i = 0; i < labelTrain.Count; i++)
                {
                    chiDistances.Add(new Couple(0, labelTrain[i]));
                }
            }
        }
        private void calculateDistances()
        {
            for (int i = 0; i < dataTrain.Count; i++)
            {
                ChiSquareDissimilarity chiObj = new ChiSquareDissimilarity(dataTest, dataTrain[i], 5);
                chiDistances[i].Distance = chiObj.CalculateDissimilarityValue();
                System.Diagnostics.Debug.WriteLine(chiDistances[i].Label + " " + chiDistances[i].Distance.ToString());
            }
            chiDistances.Sort((s1, s2) => s1.Distance.CompareTo(s2.Distance));
        }
        public string getClass()
        {
            calculateDistances();
            chiDistances.Sort((s1, s2)=> s1.Distance.CompareTo(s2.Distance));
            return getMostNeighbour();
        }
        private string getMostNeighbour()
        {
            List<Pair> topK = new List<Pair>();
            for(int i=0; i<this.K_constanta; i++)
            {
                topK.Add(new Pair(this.chiDistances[i].Label));
            }
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
                        topK.Add(pairObj);
                    }
                }
            }
            //foreach(Pair x in topK)
            //{
            //    System.Diagnostics.Debug.WriteLine(x.Key + " " + x.Value.ToString());
            //}
            topK.Sort((s1, s2) => s1.Value.CompareTo(s2.Value));
            return topK[0].Key;
        }
    }
}
