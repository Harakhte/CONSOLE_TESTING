using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CONSOLE_TESTING
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ////Find Daddy Tile

            int XX = 13117;
            int YY = 7451;
            int DDetail = 14;
            int ParentDetail = 11;
            string xx = TileXYToQuadKey(XX, YY, DDetail);

            Result b = ReturnDad(XX, YY, DDetail, ParentDetail);

            ////Find Kids Tile

            int X = 102;
            int Y = 58;
            int Detail = 7;
            int KidsDetail = 14;
            List<Result> a = ReturnKid(X, Y, Detail, KidsDetail);
            //bool alreadyExists = a.Any(x => x.X == "13117" && x.Y == "7451" && x.Z == "14");
            //ReturnListDad(a, 5);
            List<Result> l = ReturnRemainKid(XX, YY, DDetail, ParentDetail);
            List<Result> m = ReturnRemainKid(XX, YY, DDetail, ParentDetail);
            var mm = m.Min(z => z.Z);
            var mmm = m.Max(z => z.Z);
            //Data caculate
            var random = new Random();
            int resourse = 1000;
            foreach (var item in l)
            {
                item.dataneed = random.Next(100, 300);
            }
            //l.Where(x => x.X == "3278" && x.Y == "1862").Select(y => { y.dataneed = 244; return y; }).ToList();
            //l.Where(x => x.X == "3278" && x.Y == "1863").Select(y => { y.dataneed = 191; return y; }).ToList();
            //l.Where(x => x.X == "3279" && x.Y == "1863").Select(y => { y.dataneed = 199; return y; }).ToList();
            //l.Where(x => x.X == "6558" && x.Y == "3724").Select(y => { y.dataneed = 161; return y; }).ToList();
            //l.Where(x => x.X == "6559" && x.Y == "3725").Select(y => { y.dataneed = 238; return y; }).ToList();
            //l.Where(x => x.X == "6559" && x.Y == "3724").Select(y => { y.dataneed = 256; return y; }).ToList();
            //l.Where(x => x.X == "13116" && x.Y == "7450").Select(y => { y.dataneed = 147; return y; }).ToList();
            //l.Where(x => x.X == "13117" && x.Y == "7450").Select(y => { y.dataneed = 165; return y; }).ToList();
            //l.Where(x => x.X == "13116" && x.Y == "7451").Select(y => { y.dataneed = 206; return y; }).ToList();

            List<Result> gg = ReturnedAllocatedData2nd(l, 14, 11, 1000);
            int summer = gg.Sum(item => item.received);
            Console.WriteLine("done!");
        }

        public static List<Result> ReturnedAllocatedData2nd(List<Result> a, int z, int zoomed, int data)
        {

            List<Result> res = new List<Result>();
            int y = z;
            int split;
            int dataa = data;
            int lapse = z - zoomed;
            var zz = a;
            int sparedata = 0;
            for (int i = 0; i < (z - zoomed); i++)
            {
                sparedata = 0;
                List<Result> needmore = new List<Result>();
                if (i + 1 < (z - zoomed))
                {
                    split = dataa / 4;
                    sparedata += (dataa % 4);
                }
                else
                {
                    split = dataa / 3;
                    sparedata += (dataa % 3);
                }
                dataa = split;
                var g = zz.Min(z => z.Z);
                var f = zz.Where(j => j.Z != g).ToList();
                int sum = f.Sum(item => item.dataneed);
                var ff = zz.Where(j => j.Z == g).ToList();
                zz.RemoveAll(x => x.Z == g);
                if (split >= sum && i + 1 < (z - zoomed))
                {
                    sparedata += (split - sum);
                    foreach (var item in f)
                    {
                        item.dataneed = item.received;
                    }

                    //sorting the rest
                    foreach (var item in ff)
                    {
                        if (split >= item.dataneed)
                        {
                            sparedata += split - item.dataneed;
                            item.received = item.dataneed;
                            res.Add(item);
                        }
                        else
                        {
                            item.received = split;
                            needmore.Add(item);
                        }
                    }
                    Recursion(needmore, sparedata, res);
                    break;
                }
                else if (i + 1 < (z - zoomed))
                {
                    needmore.Add(new Result { dataneed = sum, received = split, X = "6" });

                    foreach (var item in ff)
                    {
                        if (split >= item.dataneed)
                        {
                            sparedata += split - item.dataneed;
                            item.received = item.dataneed;
                            res.Add(item);
                        }
                        else
                        {
                            item.received = split;
                            needmore.Add(item);
                        }
                    }
                    Recursion(needmore, sparedata, res);
                    var dataaz = res.First(x => x.X == "6");
                    dataa = dataaz.received;
                    res.Remove(dataaz);
                }
                else
                {
                    foreach (var item in ff)
                    {
                        if (split >= item.dataneed)
                        {
                            sparedata += split - item.dataneed;
                            item.received = item.dataneed;
                            res.Add(item);
                        }
                        else
                        {
                            item.received = split;
                            needmore.Add(item);
                        }
                    }
                    Recursion(needmore, sparedata, res);
                }
            }
            return res;
        }

        public static List<Result> ReturnedAllocatedData(List<Result> a, int z, int zoomed, int data)
        {

            List<Result> res = new List<Result>();
            int y = z;
            int split;
            int dataa = data;
            int lapse = z - zoomed;
            var zz = a;
            int sparedata = 0;
            for (int i = 0; i < (z - zoomed); i++)
            {
                List<Result> needmore = new List<Result>();
                sparedata = 0;
                if (i + 1 < (z - zoomed))
                {
                    split = dataa / 4;
                    sparedata += (dataa % 4);
                }
                else
                {
                    split = dataa / 3;
                    sparedata += (dataa % 3);
                }
                dataa = split;
                List<Result> f = new List<Result>();
                var g = zz.Min(z => z.Z);
                f = zz.Where(j => j.Z == g).ToList();
                zz.RemoveAll(x => x.Z == g);

                int needmoredata = 0;
                foreach (var item in f)
                {
                    if (split >= item.dataneed)
                    {
                        sparedata += split - item.dataneed;
                        item.received = item.dataneed;
                        res.Add(item);
                    }
                    else
                    {
                        item.received = split;
                        needmoredata += item.dataneed - split;
                        needmore.Add(item);

                    }
                }
                Recursion(needmore, sparedata, res);
                //if (needmore.Any() && sparedata > 0)
                //{
                //    if (needmore.Count() == 1 )
                //    {
                //        needmore[0].received += sparedata;
                //        if (needmore[0].received > needmore[0].dataneed)
                //        {
                //            needmore[0].received = needmore[0].dataneed;
                //        }
                //        res.Add(needmore[0]);
                //    }
                //    else
                //    {
                //        var evenly = sparedata / needmore.Count();
                //        sparedata = 0;
                //        foreach (var item in needmore)
                //        {
                //            item.received += evenly;
                //            if (item.received >= item.dataneed)
                //            {
                //                item.received = item.dataneed;
                //                sparedata += (item.received - item.dataneed);
                //                res.Add(item);
                //                needmore.Remove(item);
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    if (needmore.Any())
                //    {
                //        foreach (var item in needmore)
                //        {
                //            res.Add(item);
                //            needmore.Remove(item);
                //        }
                //    }
                //}

            }
            return res;
        }
        public static void Recursion(List<Result> needmore, int sparedata, List<Result> res)
        {
            if (needmore.Any() && sparedata > 0)
            {
                if (needmore.Count() == 1)
                {
                    needmore[0].received += sparedata;
                    if (needmore[0].received > needmore[0].dataneed)
                    {
                        needmore[0].received = needmore[0].dataneed;
                    }
                    res.Add(needmore[0]);
                }
                else if (needmore.Count() > sparedata)
                {
                    var zneedmore = needmore.OrderBy(x => (x.dataneed - x.received)).Take(sparedata).ToList();
                    var zzneedmore = needmore.Except(zneedmore).ToList();
                    foreach (var item in zneedmore.ToList())
                    {
                        item.received += 1;
                        res.Add(item);
                        zneedmore.Remove(item);
                    }
                    foreach (var item in zzneedmore.ToList())
                    {
                        res.Add(item);
                        zzneedmore.Remove(item);
                    }
                }
                else
                {
                    var evenly = sparedata / needmore.Count();
                    sparedata = 0 + (sparedata % needmore.Count());
                    foreach (var item in needmore.ToList())
                    {
                        item.received += evenly;
                        if (item.received >= item.dataneed)
                        {
                            sparedata += (item.received - item.dataneed);
                            item.received = item.dataneed;
                            res.Add(item);
                            needmore.Remove(item);
                        }
                    }
                    Recursion(needmore, sparedata, res);
                }
            }
            else
            {
                if (needmore.Any())
                {
                    foreach (var item in needmore.ToList())
                    {
                        res.Add(item);
                        needmore.Remove(item);
                    }
                }
            }

        }
        public static IEnumerable<int> DivideEvenly(int numerator, int denominator)
        {
            int rem;
            int div = Math.DivRem(numerator, denominator, out rem);

            for (int i = 0; i < denominator; i++)
            {
                yield return i < rem ? div + 1 : div;
            }
        }
        public static void ReturnListDad(List<Result> g, int ParentDetail)
        {
            List<Result> lstmanydadyresult = new List<Result>();
            foreach (var item in g)
            {
                int x = Int32.Parse(item.X);
                int y = Int32.Parse(item.Y);
                int z = Int32.Parse(item.Z);
                string xx = TileXYToQuadKey(x, y, z);
                string key = xx.Remove(ParentDetail, (z - ParentDetail));
                QuadKeyToTileXY(key, out x, out y, out z);
                item.X += "-->" + x.ToString();
                item.Y += "-->" + y.ToString();
                item.Z += "-->" + z.ToString();
            }
        }

        public static List<Result> ReturnRemainKid(int X, int Y, int Z, int zoomed)
        {
            List<Result> lstresult = new List<Result>();
            List<string> list = new List<string>();
            string xx = TileXYToQuadKey(X, Y, Z);
            for (int i = 0; i < (Z - zoomed); i++)
            {
                int bar = xx[xx.Length - 1] - '0';
                int[] numbers = { 0, 1, 2, 3 };
                numbers = numbers.Where(val => val != bar).ToArray();
                foreach (var item in numbers)
                {
                    list.Add(xx.Remove(xx.Length - 1) + item);
                }
                xx = xx.Remove(xx.Length - 1);
            }

            foreach (var a in list)
            {
                Result res = new Result();
                int Xresult;
                int Yresult;
                int Detailresult;
                QuadKeyToTileXY(a, out Xresult, out Yresult, out Detailresult);
                res.X = Xresult.ToString();
                res.Y = Yresult.ToString();
                res.Z = Detailresult.ToString();
                lstresult.Add(res);
            }
            return lstresult;
        }

        public static List<Result> ReturnKid(int X, int Y, int Detail, int KidsDetail)
        {
            List<Result> lstresult = new List<Result>();
            string xx = TileXYToQuadKey(X, Y, Detail);
            string[] numbers = new[] { xx };
            for (int i = 0; i < (KidsDetail - Detail); i++)
            {
                List<string> list = new List<string>();
                foreach (string item in numbers)
                {
                    list.Add(item + "0");
                    list.Add(item + "1");
                    list.Add(item + "2");
                    list.Add(item + "3");
                }
                String[] str = list.ToArray();
                numbers = str;
            }
            foreach (string zz in numbers)
            {
                Result res = new Result();
                int Xresult;
                int Yresult;
                int Detailresult;
                QuadKeyToTileXY(zz, out Xresult, out Yresult, out Detailresult);
                res.X = Xresult.ToString();
                res.Y = Yresult.ToString();
                res.Z = Detailresult.ToString();
                lstresult.Add(res);
            }

            return lstresult;
        }
        public static Result ReturnDad(int X, int Y, int Detail, int ParentDetail)
        {
            Result res = new Result();
            string xx = TileXYToQuadKey(X, Y, Detail);
            int Xresult;
            int Yresult;
            int Detailresult;
            string key = xx.Remove(ParentDetail, (Detail - ParentDetail));
            QuadKeyToTileXY(key, out Xresult, out Yresult, out Detailresult);
            res.X = Xresult.ToString();
            res.Y = Yresult.ToString();
            res.Z = Detailresult.ToString();
            return res;
        }
        public static void PrintDaddyTile(int X, int Y, int Detail, int ParentDetail)
        {
            string xx = TileXYToQuadKey(X, Y, Detail);
            int Xresult;
            int Yresult;
            int Detailresult;
            string key = xx.Remove(ParentDetail, (Detail - ParentDetail));
            QuadKeyToTileXY(key, out Xresult, out Yresult, out Detailresult);
            Console.WriteLine(Xresult);
            Console.WriteLine(Yresult);
            Console.WriteLine(Detailresult);
        }
        public static void PrintKidTile(int X, int Y, int Detail, int KidsDetail)
        {
            string xx = TileXYToQuadKey(X, Y, Detail);
            string[] numbers = new[] { xx };
            List<int> listResult = new List<int>();
            for (int i = 0; i < (KidsDetail - Detail); i++)
            {
                List<string> list = new List<string>();
                foreach (string item in numbers)
                {
                    list.Add(item + "0");
                    list.Add(item + "1");
                    list.Add(item + "2");
                    list.Add(item + "3");
                }
                String[] str = list.ToArray();
                numbers = str;
            }
            foreach (string zz in numbers)
            {
                int Xresult;
                int Yresult;
                int Detailresult;
                QuadKeyToTileXY(zz, out Xresult, out Yresult, out Detailresult);
                int[] adding = new int[] { Xresult, Yresult, Detailresult };
                listResult.AddRange(adding);
            }
            listResult.ForEach(Console.WriteLine);
        }
        public static void QuadKeyToTileXY(string quadKey, out int tileX, out int tileY, out int levelOfDetail)
        {
            tileX = tileY = 0;
            levelOfDetail = quadKey.Length;
            for (int i = levelOfDetail; i > 0; i--)
            {
                int mask = 1 << (i - 1);
                switch (quadKey[levelOfDetail - i])
                {
                    case '0':
                        break;

                    case '1':
                        tileX |= mask;
                        break;

                    case '2':
                        tileY |= mask;
                        break;

                    case '3':
                        tileX |= mask;
                        tileY |= mask;
                        break;

                    default:
                        throw new ArgumentException("Invalid QuadKey digit sequence.");
                }
            }
        }
        public static string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)
        {
            StringBuilder quadKey = new StringBuilder();
            for (int i = levelOfDetail; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }
                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }
    }
}
