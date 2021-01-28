using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace SpiderSolitaire
{
    class Program
    {        
        private static void Main()
        {
            int cardColumn = 10;
            List<int>[] changingCards = new List<int>[cardColumn];
            int[,] playingCards;
            //发牌次数
            int numOfCardsIssued = 1;
            int numOfInvalid = 0;
            int movePlace = 1;
            int numOfRemove = 0;
            bool dealOver = false;

            StartLayout();
            playingCards = Initailize(changingCards);

            while (!GameOver(numOfRemove))
            {
                Console.WriteLine("\n\n请输入移动码移动：");

                switch (Console.ReadLine())
                {
                    case "s" or "S":
                    {
                        if (dealOver)
                        {
                            numOfInvalid++;
                            break;
                        }
                        numOfCardsIssued = Deal(numOfCardsIssued, changingCards, ref dealOver, playingCards);
                        Elimiate(ref numOfRemove, changingCards);
                        RefreshPage(ref numOfInvalid, changingCards);
                    }break;
                    case "m" or "M":
                    {
                        if (MoveCards(ref numOfInvalid, changingCards))
                        {
                            Elimiate(movePlace, ref numOfRemove, changingCards);
                            RefreshPage(ref numOfInvalid, changingCards);
                        }
                    }break;
                    default:
                    {
                        numOfInvalid++;
                        Console.WriteLine("\n您的操作不符合要求，请重新输入：");
                    }break;
                }
                if (numOfInvalid >= 2)
                    RefreshPage(ref numOfInvalid, changingCards);
            }
        }

        private static bool GameOver(int numOfRemove)
        {
            if (numOfRemove != 10) return false;
            Console.Clear();
            Console.WriteLine("您赢了！！！");
            Console.WriteLine("biu,biu,biu~");
            return true;
        }

        //刷新界面
        static void RefreshPage(ref int numOfInvalid, List<int>[] changingCards)
        {
            Console.Clear();
            int column, row;

            for (row = 0; row < Longestfor(changingCards); row++)
            {
                for (column = 0; column < changingCards.Length; column++)
                {
                    if (changingCards[column].Count - row <= 0)
                        Console.Write(" \t");
                    else if (row == 0)
                    {
                        Console.Write(changingCards[column][row] + "({0})\t", column + 1);
                    }
                    else
                        Console.Write(changingCards[column][row] + "\t");
                }
                Console.WriteLine();
            }
            numOfInvalid = 0;
            Console.WriteLine("\n\n\n\n");

        }
        static int Longestfor(List<int>[] changingCards)
        {
            int longest, i;
            longest = changingCards[9].Count;
            for (i = 0; i < changingCards.Length - 1; i++)
            {
                if (changingCards[i].Count > longest)
                    longest = changingCards[i].Count;
            }

            return longest;
        }

        static void StartLayout()
        {
            Console.WriteLine("\t欢迎来到 MrFeng 的游戏空间");
            Console.WriteLine("------------------蜘蛛纸牌------------------");
            Console.WriteLine("输入s发牌，输入m移牌");
            Console.WriteLine("移动规则:第一个数指要移动的行数 \n\t 第二个数指移动该列的牌数 \n\t 第三个数指纸牌移动的位置\n");
            Console.WriteLine("\t 输入s发牌\n");
            Console.WriteLine("(注：括号中的数指当前数字的列数)");
            Console.WriteLine("------------------蜘蛛纸牌------------------\n\n");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        static int[,] Initailize(List<int>[] changingCards)
        {
            //扑克牌数组初始化
            int cardRow = 13;
            int cardColumn = 10;
            int[,] playingCards = new int[cardRow, cardColumn];
            int changingRow, changingColumn;
            for (changingRow = 0; changingRow < cardRow; changingRow++)
            {
                for (changingColumn = 0; changingColumn < cardColumn; changingColumn++)
                    playingCards[changingRow, changingColumn] = changingRow + 1;
            }

            //交换
            for (changingRow = 0; changingRow < cardRow; changingRow++)
            {
                for (changingColumn = 0; changingColumn < cardColumn; changingColumn++)
                {
                    Swap(playingCards, changingRow, changingColumn, RandNum(0, 13), RandNum(0, 10));
                }
            }

            //显示第一行
            for (changingColumn = 0; changingColumn < cardColumn; changingColumn++)
            {
                Console.Write(playingCards[0, changingColumn] + "（{0}）\t", changingColumn + 1);
            }

            //初始化栈数组
            for (changingColumn = 0; changingColumn < changingCards.Length; changingColumn++)
            {
                changingCards[changingColumn] = new List<int>
                {
                    playingCards[0, changingColumn]
                };
            }
            return playingCards;
        }

        /// <summary>
        /// 产生正随机数
        /// </summary>
        /// <param name="start">随机开始值</param>
        /// <param name="end">随机结束值</param>
        /// <returns>随机值</returns>
        static int RandNum(int start, int end) => Math.Abs(Guid.NewGuid().GetHashCode() % end + start);

        /// <summary>
        /// 交换数组的两个元素
        /// </summary>
        /// <param name="array">被交换数组</param>
        /// <param name="fRow">第一个元素行</param>
        /// <param name="fColumn">第一个元素列</param>
        /// <param name="sRow">第二个元素行</param>
        /// <param name="sColumn">第二个元素列</param>
        static void Swap(int[,] array, int fRow, int fColumn, int sRow, int sColumn)
        {
            if (fRow == sRow && sRow == sColumn) return;
            int temp;
            temp = array[fRow, fColumn];
            array[fRow, fColumn] = array[sRow, sColumn];
            array[sRow, sColumn] = temp;
        }

        /// <summary>
        /// 移牌
        /// </summary>
        /// <param name="moveColumn">操作列</param>
        /// <param name="numberOfCards">操作纸牌数目</param>
        /// <param name="movePlace">移动位置</param>
        static bool MoveCards(ref int numOfInvalid, List<int>[] changingCards)
        {//变量太多
            int moveColumn, movePlace, numberOfCards;
            //三元运算符
            return (IsLegalMove(ref numOfInvalid, changingCards, out moveColumn, out movePlace, out numberOfCards) &&
                Move(ref numOfInvalid, changingCards, moveColumn, movePlace, numberOfCards)) ?
                true : false;
        }

        private static bool Move(ref int numOfInvalid, List<int>[] changingCards, int moveColumn, int movePlace, int numberOfCards)
        {
            /*
            *判断：
            * 条件一：如果被移动列的移动首数字 小于 移动位置末位数字 一个单位
            * 且 条件二：被移动列的所有数据都是按序相差一排列（从小到大），可以移动
            * 
            *移动算法：
            *从被移动处截止，将被移动的集合元素赋给移动位置的集合
            *删除被移动集合对应元素
            */
            int columnLen = changingCards[moveColumn].Count;
            int placeLen = changingCards[movePlace].Count;
            int row, temp;
            temp = changingCards[moveColumn][columnLen - numberOfCards];
            //条件一
            if (changingCards[movePlace][placeLen - 1] + 1 == temp)
            {
                //条件二
                for (row = 1; row < numberOfCards; row++)
                {
                    if (temp + 1 != changingCards[moveColumn][columnLen - numberOfCards + row])
                    {
                        Console.WriteLine("移动操作不符合游戏规则，已退出移动操作\n");
                        numOfInvalid++;
                        return false;
                    }
                    else
                    {
                        temp = changingCards[moveColumn][columnLen - numberOfCards + row];
                    }
                }
                //移动算法
                for (row = 0; row < numberOfCards; row++)
                {
                    //从上到下添加元素
                    changingCards[movePlace].Add(changingCards[moveColumn][columnLen - numberOfCards + row]);
                }
                changingCards[moveColumn].RemoveRange(columnLen - numberOfCards, numberOfCards);
            }
            else
            {
                Console.WriteLine("移动操作不符合游戏规则，已退出移动操作\n");
                numOfInvalid++;
                return false;
            }
            Console.WriteLine("移牌已结束");
            return true;
        }

        private static bool IsLegalMove(ref int numOfInvalid, List<int>[] changingCards, out int moveColumn, out int movePlace, out int numberOfCards)
        {
            moveColumn = 0;
            movePlace = 0;
            numberOfCards = 0;
            try
            {
                Console.Write("请输入第一个操作数（要移动的列）：");
                moveColumn = int.Parse(Console.ReadLine());
                Console.Write("请输入第二个操作数（要移动的个数）:");
                numberOfCards = int.Parse(Console.ReadLine());
                Console.Write("请输入第三个操作数（移至何列）：");
                movePlace = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("移动码必须是数字！！！");
                Console.WriteLine("移牌已结束");
                return false;
            }

            moveColumn--;
            movePlace--;


            //三个数的合法验证
            if (numberOfCards > changingCards[moveColumn].Count || numberOfCards <= 0)
            {
                Console.WriteLine("移动元素个数不在范围内,已退出移动操作\n");
                numOfInvalid++;
                return false;
            }
            if (moveColumn >= changingCards.Length || moveColumn < 0)
            {
                Console.WriteLine("输入的列不在当前范围内，已退出移动操作\n");
                numOfInvalid++;
                return false;
            }
            if (movePlace < 0 || movePlace >= changingCards.Length)
            {
                Console.WriteLine("移动的位置不在范围内，已退出移动操作\n");
                numOfInvalid++;
                return false;
            }
            if (moveColumn == movePlace)
            {
                Console.WriteLine("移动列和被移动列相同，已退出移动操作\n");
                numOfInvalid++;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="numOfCardsIssued">之前发牌次数</param>
        static int Deal(int numOfCardsIssued, List<int>[] changingCards, ref bool dealOver, int[,] playingCards)
        {
            int changingColumn;
            //牌发完时return短路
            if (numOfCardsIssued >= 13)
            {
                dealOver = true;
                Console.WriteLine("本局所有牌已发完");
                Console.WriteLine("请输入移动码移动：");
                return 13;
            }
            //否则刷新屏幕，进行发牌
            //发牌部分可优化
            for (changingColumn = 0; changingColumn < changingCards.Length; changingColumn++)
            {
                changingCards[changingColumn].Add(playingCards[numOfCardsIssued, changingColumn]);

            }
            Console.WriteLine("发牌已结束");
            return ++numOfCardsIssued;
        }

        /// <summary>
        /// 移牌造成的消除
        /// </summary>
        /// <param name="movePlace">移动到的位置</param>
        /// <param name="numOfRemove">消牌次数</param>
        static void Elimiate(int movePlace, ref int numOfRemove, List<int>[] changingCards)
        {
            /*如果移动位置的最后一个元素的是K——13
             由上向下判断：1,2,3……
             */
            int changingLen = changingCards[movePlace].Count;

            if (changingCards[movePlace][changingLen - 1] == 13 && changingLen >= 12)
            {
                int row;
                for (row = 0; row < changingLen; row++)
                {
                    if (changingCards[movePlace][changingLen - 12 + row] == row)
                        break;
                }
                if (row == changingLen)
                {
                    //移除这13张牌
                    changingCards[movePlace].RemoveRange(changingLen - 13, 13);
                    numOfRemove++;
                }
            }
        }
        /// <summary>
        /// 发牌造成的消除
        /// </summary>
        /// <param name="numOfRemove">消牌次数</param>
        static void Elimiate(ref int numOfRemove, List<int>[] changingCards)
        {
            int tempLen, row;
            for (row = 0; row < changingCards.Length; row++)
            {
                tempLen = changingCards[row].Count - 1;
                if (tempLen <= 0) continue;
                if (changingCards[row][tempLen] == 13 && tempLen >= 12)
                {

                    int column;
                    for (column = 0; column < tempLen; column++)
                    {
                        if (changingCards[row][tempLen - 12 + column] == column)
                            break;
                    }
                    if (column == tempLen)
                    {
                        //移除这13张牌
                        changingCards[row].RemoveRange(tempLen - 12, 13);
                        numOfRemove++;
                    }
                }
            }
        }
    }
}
