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
                    chiDistances.Add(coupleObj);
                }
            }
        }
        private void calculateDistances()
        {
            int counter = 0;
            foreach (int[] feature in dataTrain)
            {
                ChiSquareDissimilarity chiObj = new ChiSquareDissimilarity(this.dataTest, feature);
                Couple coupleObj = new Couple(chiObj.CalculateDissimilarityValue(), chiDistances[counter].)
                coupleObj.distance = chiObj.CalculateDissimilarityValue();
                coupleObj.label = chiDistances[counter].label;
                chiDistances[counter] = coupleObj;
                counter++;
            }
        }
        private string getClass()
        {
            calculateDistances();
            chiDistances.Sort((s1, s2) => s1.distance.CompareTo(s2.distance));
            string label = getMostNeighbour();
            return label;
        }
        private string getMostNeighbour()
        {
            List<Couple> topK = new List<Couple>();
            for(int i=0; i<this.K_constanta; i++)
            {
                for(int j=0; j<topK.Count; j++)
                {
                    if (topK[j].Key == chiDistances[i].label)
                    {
                        topK[j].Value++;
                    }
                    if (i = this.K_constanta - 1)
                    {
                        
                    }
                }
            }
        }
    }
}
