using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PincerSearch
{
    class Program
    {
        static int isRemoved = 0;
        static void Main(string[] args)
        {
            int support = 3;
            //// Transaction data assignment and Initialization - start
            int[][] transactionData = new int[15][];


            for (int ix = 0; ix < 15; ix++)
            {
                transactionData[ix] = new int[5];
            }

            transactionData[0][0] = 1;
            transactionData[0][1] = 5;
            transactionData[0][2] = 6;
            transactionData[0][3] = 8;

            transactionData[1][0] = 2;
            transactionData[1][1] = 4;
            transactionData[1][2] = 8;

            transactionData[2][0] = 4;
            transactionData[2][1] = 5;
            transactionData[2][2] = 7;


            transactionData[3][0] = 2;

            transactionData[3][1] = 3;

            transactionData[4][0] = 5;
            transactionData[4][1] = 6;
            transactionData[4][2] = 7;

            transactionData[5][0] = 2;
            transactionData[5][1] = 3;
            transactionData[5][2] = 4;

            transactionData[6][0] = 2;
            transactionData[6][1] = 6;
            transactionData[6][2] = 7;
            transactionData[6][3] = 9;

            transactionData[7][0] = 5;

            transactionData[8][0] = 8;

            transactionData[9][0] = 3;
            transactionData[9][1] = 5;
            transactionData[9][2] = 7;


            transactionData[10][0] = 3;
            transactionData[10][1] = 5;
            transactionData[10][2] = 7;

            transactionData[11][0] = 5;
            transactionData[11][1] = 6;
            transactionData[11][2] = 8;

            transactionData[12][0] = 2;
            transactionData[12][1] = 4;
            transactionData[12][2] = 6;
            transactionData[12][3] = 7;

            transactionData[13][0] = 1;
            transactionData[13][1] = 3;
            transactionData[13][2] = 5;
            transactionData[13][3] = 7;

            transactionData[14][0] = 2;
            transactionData[14][1] = 3;
            transactionData[14][2] = 9;
            Console.Out.WriteLine("Transactions are as follows: ");
            for (int ix = 0; ix < 15; ix++)
            {
                Console.Out.Write("T" + ix + ":  ");
                for (int iy = 0; iy < 5; iy++)
                {
                    Console.Out.Write(transactionData[ix][iy] + " ");
                }

                Console.Out.WriteLine("");
            }

            //// Transaction data assignment and Initialization - end

            Set[] l = new Set[15];

            int k = 1;
            Set[] c = new Set[15];
            Set[] s = new Set[15];
            Set MFCS;
            Set MFS = new Set(0);
            l[0] = new Set(0);

            Element tmpElement;
            List<int> tmpList;
            List<Element> tmpElemList = new List<Element>();

            for (int i = 1; i <= 9; i++)
            {
                tmpList = new List<int>();
                tmpList.Add(i);
                tmpElement = new Element(tmpList);
                tmpElemList.Add(tmpElement);
            }
            c[k] = new Set(tmpElemList);
            s[0] = new Set(0);

            tmpList = new List<int>();
            for (int i = 1; i <= 9; i++)
            {

                tmpList.Add(i);
            }
            tmpElement = new Element(tmpList);
            tmpElemList = new List<Element>();
            tmpElemList.Add(tmpElement);
            MFCS = new Set(tmpElemList);

            tmpList = new List<int>();


            while (c[k].elements.Count != 0)
            {//start of while
                for (int i = 0; i < 15; i++)
                {
                    tmpList = new List<int>();
                    for (int j = 0; j < 5; j++)
                    {
                        if (transactionData[i][j] != 0)
                            tmpList.Add(transactionData[i][j]);

                    }
                    foreach (Element mfcsitem in MFCS.elements)
                    {
                        if (ContainsAllItems(tmpList, mfcsitem.elemList))
                        {
                            mfcsitem.count++;
                        }
                    }


                    foreach (Element item in c[k].elements)
                    {
                        if (ContainsAllItems(tmpList, item.elemList))
                        {
                            item.count++;
                        }
                    }

                }


                foreach (Element item in MFCS.elements)
                {
                    if (item.count >= support)
                    {
                        MFS.elements.Add(item);

                    }

                }

                s[k] = new Set();

                l[k] = new Set();
                foreach (Element item in c[k].elements)
                {
                    if (item.count < support)
                    {

                        s[k].elements.Add(item);
                        s[k].isEmpty = false;
                    }
                    else
                    {
                        l[k].elements.Add(item);

                    }
                }


                if (s[k].isEmpty == false)
                {
                    MFCS = MFCSgen(MFCS, s[k]);
                }
                isRemoved = 0;
                l[k] = mfsprune(l[k], MFS);

                c[k + 1] = new Set();
                c[k + 1] = candidategen(l[k]);
                if (isRemoved == 1)
                {
                    c[k + 1] = recovery(l[k], MFS, c[k + 1], k);

                }





                k++;
                int pass=k-1;
                Console.WriteLine("");
                Console.Out.WriteLine("----------Pass:"+pass+"------------------------------------------");

                Console.Out.Write("MFCS:    ");
                foreach (Element mfcsitem in MFCS.elements)
                {

                    Console.Out.Write("{");
                    foreach (int elemitem in mfcsitem.elemList)
                    {


                        Console.Out.Write(mfcsitem.elemList[0] != elemitem ? ("," + elemitem).ToString() : elemitem.ToString());
                    }

                    Console.Out.Write("}");
                }
                Console.Out.WriteLine("");
                Console.Out.Write("MFS:    ");
                if (MFS.elements.Count == 0)
                {
                    Console.Out.Write("empty");
                }
                else
                {

                    foreach (Element mfcsitem in MFS.elements)
                    {

                        Console.Out.Write("{");
                        foreach (int elemitem in mfcsitem.elemList)
                        {

                            Console.Out.Write(mfcsitem.elemList[0]!=elemitem?(","+elemitem).ToString():elemitem.ToString() );
                        }

                        Console.Out.Write("}");
                    }
                


                }

                Console.Out.WriteLine("");
                Console.Out.Write("C["+k+"]:    ");
               

                    foreach (Element mfcsitem in c[k].elements)
                    {

                        Console.Out.Write("{");
                        foreach (int elemitem in mfcsitem.elemList)
                        {


                            Console.Out.Write(mfcsitem.elemList[0] != elemitem ? ("," + elemitem).ToString() : elemitem.ToString());
                        }

                        Console.Out.Write("}");
                    }

                    Console.Out.WriteLine("");
                    Console.Out.Write("l[{0}]:    ",k-1);


                    foreach (Element mfcsitem in l[k-1].elements)
                    {

                        Console.Out.Write("{");
                        foreach (int elemitem in mfcsitem.elemList)
                        {


                            Console.Out.Write(mfcsitem.elemList[0] != elemitem ? ("," + elemitem).ToString() : elemitem.ToString());
                        }

                        Console.Out.Write("}");
                    }

                    Console.Out.WriteLine("");
                    Console.Out.Write("s[{0}]:    ", k - 1);


                    foreach (Element mfcsitem in s[k - 1].elements)
                    {

                        Console.Out.Write("{");
                        foreach (int elemitem in mfcsitem.elemList)
                        {


                            Console.Out.Write(mfcsitem.elemList[0] != elemitem ? ("," + elemitem).ToString() : elemitem.ToString());
                        }

                        Console.Out.Write("}");
                    }

                
                //end of while
            }


            Console.Out.WriteLine("");
            Console.Out.WriteLine("Final L Set contents:");

            foreach (Set litem in l)
            {

                if (litem != null)
                    foreach (Element item in litem.elements)
                    {

                        Console.Out.Write("{");
                        foreach (int mfcsl in item.elemList)
                        {

                            Console.Out.Write(item.elemList[0] != mfcsl ? ("," + mfcsl).ToString() : mfcsl.ToString());
                        }

                        Console.Out.Write("}");
                    }
            }






            Console.In.Read();
        }

        private static Set mfcsprune(Set set, Set MFCS)
        {



            for (int i = set.elements.Count - 1; i >= 0; i--)
            {

                Element setList = set.elements[i];
                foreach (Element m in MFCS.elements)
                {

                    if (!ContainsAllItems(m.elemList, setList.elemList))
                    {
                        set.elements.Remove(setList);

                    }

                }

            }
            return set;

        }

        private static Set recovery(Set lset, Set MFS, Set cset, int k)
        {
            int flag = 0, index = 0;
            List<int> tempList = new List<int>();

            List<int> tempList2 = new List<int>();
            foreach (Element l in lset.elements)
            {
                flag = 0;
                index = 0;
                foreach (Element m in MFS.elements)
                {
                    for (int i = 0; i < k - 1; i++)
                    {
                        if (!m.elemList.Contains(l.elemList[i]))
                        {
                            flag = 1;

                            break;

                        }
                        else
                        {
                            index = m.elemList.IndexOf(l.elemList[i]);
                        }
                    }

                    if (flag == 0)
                    {
                        tempList = new List<int>();
                        for (int i = 0; i < k; i++)
                        {
                            tempList.Add(l.elemList[i]);
                        }

                        Element newTempElement = new Element();
                        for (int i = index + 1; i < m.count; i++)
                        {
                            tempList2 = tempList;
                            tempList2.Add(m.elemList[i]);
                            newTempElement = new Element(tempList2);
                            cset.elements.Add(newTempElement);
                        }
                    }

                }
            }


            return cset;
        }

        private static Set candidategen(Set set)
        {
            Set c = new Set();
            int count = 0;
            int length1 = 0, length2 = 0;
            Element tmpElement = new Element(); ;
            List<int> tmpList = new List<int>();
            foreach (Element item in set.elements)
            {
                count++;

            }
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    length1 = set.elements[i].elemList.Count;
                    length2 = set.elements[j].elemList.Count;
                    if (length2 == length1)
                    {
                        if (length1 == 1)
                        {
                            tmpList = new List<int>();
                            tmpList.Add(set.elements[i].elemList[0]);
                            tmpList.Add(set.elements[j].elemList[0]);
                            tmpElement = new Element(tmpList);
                            c.elements.Add(tmpElement);

                        }
                        else if (length1 > 0)
                        {
                            int flag = 0;
                            for (int k = 0; k < length1 - 1; k++)
                            {
                                if (set.elements[i].elemList[k] != set.elements[j].elemList[k])
                                {
                                    flag = 1;
                                    break;
                                }


                            }

                            if (!(set.elements[i].elemList[length1 - 1] < set.elements[j].elemList[length1 - 1]))
                            {
                                flag = 1;
                            }
                            if (flag == 0)
                            {
                                tmpList = new List<int>();
                                int k = 0;
                                for (k = 0; k < length1; k++)
                                {

                                    tmpList.Add(set.elements[i].elemList[k]);


                                }
                                tmpList.Add(set.elements[j].elemList[k - 1]);
                                tmpElement = new Element(tmpList);
                                c.elements.Add(tmpElement);
                            }

                        }

                    }

                }

            }

            return c;

        }

        private static Set mfsprune(Set set, Set MFS)
        {
            int count = 0;
            foreach (Element setList in set.elements)
            {
                count++;
            }

            foreach (Element item in MFS.elements)
            {

                for (int i = 1; i <= count; i++)
                {
                    Element setList = set.elements[count - 1];
                    if (ContainsAllItems(item.elemList, setList.elemList))
                    {
                        set.elements.Remove(setList);
                        isRemoved = 1;
                    }

                }

            }
            return set;

        }

        private static Set MFCSgen(Set MFCS, Set set)
        {



            foreach (Element s in set.elements)
            {


                for (int i = MFCS.elements.Count - 1; i >= 0; i--)
                {

                    Element m = MFCS.elements[i];
                    if (ContainsAllItems(m.elemList, s.elemList))
                    {

                        MFCS.elements.Remove(m);
                        Element mback = m;
                        foreach (Element e in set.elements)
                        {
                            foreach (int eint in e.elemList)
                            {

                                if (mback.elemList.Contains(eint))
                                {
                                    mback.elemList.Remove(eint);

                                }
                                int flag = 0;
                                foreach (Element mfcsitem in MFCS.elements)
                                {

                                    if (ContainsAllItems(mfcsitem.elemList, mback.elemList))
                                    {
                                        flag = 1;
                                        break;
                                    }
                                }

                                if (flag == 0)
                                {
                                    MFCS.elements.Add(mback);

                                }


                            }
                        }
                    }

                }
            }

            return MFCS;

        }
        public static bool ContainsAllItems(List<int> a, List<int> b)
        {
            return !b.Except(a).Any();
        }
    }
}
