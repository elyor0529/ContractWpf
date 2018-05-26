using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Core.Extensions
{
    public static class PriceExtensions
    {
        private static bool isMillion = false;  // 3, 4, 5, 8 mln. uchun { 3 mln. => трех млн., 4 mln. => четырех млн.,... }
        private static bool isThousand = false; // 2.000 uchun { 2.000 => две тысячи | !(двa тысячи) }

        public static string Speach(this double number)
        {
            return Speach((int)number);
        }
         
        public static string Speach(this int number)
        {
            var word = "";                      // natija
            var len = number.ToString().Length; // son uzunligi
            var d = Math.Pow(10, (len - 1));    // son 10 lik, 100 lik, 1000 lik,... ekanligini aniqlash uchun
            int n1 = (int)((number) / d);       // XY, XYY, XYYY... => { n1 = X } , sonni 1-xonasidagi raqam
            int n_10 = (int)(n1 * d);           // 200, ..., 900 uchun, { 200 => двести, 300 => триста,..., 900 => девятьсот } 
            int n_div;                          // XY, XXY, XXXY... => { n_div=Y }, { n_div == 1 => миллион,  }

            switch (len)
            {
                case 1:
                    #region 1 >---> 9

                    switch (n1)
                    {
                        case 1:
                            word += "один";
                            break;
                        case 2:
                            word += (isThousand) ?
                                word += "две" :
                                word += "два";
                            break;
                        case 3:
                            word += (isMillion) ?
                                word += "трех" :
                                word += "три";
                            break;
                        case 4:
                            word += (isMillion) ?
                                word += "четырех" :
                                word += "четыре";
                            break;
                        case 5:
                            word += (isMillion) ?
                                word += "пяти" :
                                word += "пять";
                            break;
                        case 6:
                            word += "шесть";
                            break;
                        case 7:
                            word += "семь";
                            break;
                        case 8:
                            word += (isMillion) ?
                                word += "восьми" :
                                word += "восемь";
                            break;
                        case 9:
                            word += "девять";
                            break;
                    }
                    #endregion
                    break;
                case 2:
                    #region 10 >---> 99

                    switch (number)
                    {
                        case 10:
                            word += "десять";
                            n_10 = 10;
                            break;
                        case 11:
                            word += "одиннадцать";
                            n_10 = 11;
                            break;
                        case 12:
                            word += "двенадцать";
                            n_10 = 12;
                            break;
                        case 13:
                            word += "тринадцать";
                            n_10 = 13;
                            break;
                        case 14:
                            word += "четырнадцать";
                            n_10 = 14;
                            break;
                        case 15:
                            word += "пятнадцать";
                            n_10 = 15;
                            break;
                        case 16:
                            word += "шестнадцать";
                            n_10 = 16;
                            break;
                        case 17:
                            word += "семнадцать";
                            n_10 = 17;
                            break;
                        case 18:
                            word += "восемннадцать";
                            n_10 = 18;
                            break;
                        case 19:
                            word += "девятнадцать";
                            n_10 = 19;
                            break;

                    }
                    if (n1 != 1)
                    {
                        switch (n_10)
                        {
                            case 20:
                                word += "двадцать";
                                break;
                            case 30:
                                word += "тридцать";
                                break;
                            case 40:
                                word += "сорок";
                                break;
                            case 50:
                                word += "пятьдесят";
                                break;
                            case 60:
                                word += "шестьдесят";
                                break;
                            case 70:
                                word += "семьдесят";
                                break;
                            case 80:
                                word += "восемьдесят";
                                break;
                            case 90:
                                word += "девяносто";
                                break;
                        }
                    }
                    #endregion
                    break;
                case 3:
                    #region 100 >---> 999

                    switch (n_10)
                    {
                        case 100:
                            word += "сто";
                            break;
                        case 200:
                            word += "двести";
                            break;
                        case 300:
                            word += "триста";
                            break;
                        case 400:
                            word += "четыреста";
                            break;
                        case 500:
                            word += "пятьсот";
                            break;
                        case 600:
                            word += "шестьсот";
                            break;
                        case 700:
                            word += "семьсот";
                            break;
                        case 800:
                            word += "восемьсот";
                            break;
                        case 900:
                            word += "девятьсот";
                            break;
                    }
                    #endregion
                    break;
                case 4:
                    #region 1.000 >---> 9.999

                    isMillion = false;
                    isThousand = true;

                    word += (n1 == 1) ?
                            word += Speach(n1) + " тысяча" : (n1 > 1 && n1 < 5) ?
                            word += Speach(n1) + " тысячи" :
                            word += Speach(n1) + " тысяч";

                    #endregion
                    break;
                case 5:
                    #region 10.000 >---> 99.999

                    isMillion = false;
                    isThousand = false;

                    n_10 = number / 1000;
                    n_div = n_10 % 10;

                    word += (n_10 >= 10 && n_10 < 20) ?
                            word += Speach(n_10) + " тысяч" : (n_10 >= 20 && n_10 <= 51 || n_div == 1) ?
                            word += Speach(n_10) + " тысяча" :
                            word += Speach(n_10) + " тысячи";

                    n_10 = (n_10) * 1000;

                    #endregion
                    break;
                case 6:
                    #region 100.000 >---> 999.999
                    isMillion = false;
                    isThousand = false;

                    n_10 = number / 1000;

                    word += (n_10 == 100) ?
                            word += Speach(n_10) + " тысяч" :
                            word += Speach(n_10) + " тысяча";

                    n_10 = n_10 * 1000;

                    #endregion
                    break;
                case 7:
                    #region 1.000.000 >---> 9.999.999

                    isMillion = true;
                    isThousand = false;

                    word += (n1 == 1) ?
                            word += Speach(n1) + " миллион" : (n1 == 2) ?
                            word += Speach(n1) + " миллиона" :
                            word += Speach(n1) + " миллионов";

                    #endregion
                    break;
                case 8:
                    #region 10.000.000 >---> 99.999.999

                    isMillion = false;
                    isThousand = false;

                    n_10 = number / 1000000;
                    n_div = n_10 % 10;

                    word += (n_div == 1) ?
                            word += Speach(n_10) + " миллион" : (n_div == 2 && n_10 != 12) ?
                            word += Speach(n_10) + " миллиона" :
                            word += Speach(n_10) + " миллионов";

                    n_10 = n_10 * 1000000;

                    #endregion
                    break;
                case 9:
                    #region 100.000.000 >---> 999.999.999

                    isMillion = false;
                    isThousand = false;

                    n_10 = number / 1000000;
                    n_div = (n_10 % 100) % 10;

                    word += (n_div == 1) ?
                            word += Speach(n_10) + " миллион" : (n_div == 2) ?
                            word += Speach(n_10) + " миллиона" :
                            word += Speach(n_10) + " миллионов";

                    n_10 = n_10 * 1000000;

                    #endregion
                    break;
                case 10:
                    #region 1000.000.000 >---> 2.147.483.647

                    isMillion = false;
                    isThousand = false;

                    n_10 = number / 1000000000;
                    n_div = n_10 % 1000000000;

                    word += (n_div == 1) ?
                          word += Speach(n1) + " миллиард" : (n_div == 2) ?
                          word += Speach(n1) + " миллиарда" : word;

                    n_10 = n1 * 1000000000;

                    #endregion
                    break;
            }

            if (number - n_10 > 0)
            {
                return word += " " + Speach(number - n_10);
            }

            return word;
        }

    }

}